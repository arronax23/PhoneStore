using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace PhoneShop.UI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("init main");
                var builder = CreateHostBuilder(args).Build();
                using(var serviceScope  = builder.Services.CreateScope())
                {
                    var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    var doesAdminRoleExists = await roleManager.RoleExistsAsync("Admin");
                    var doesCustomerRoleExists = await roleManager.RoleExistsAsync("Customer");

                    if (!doesAdminRoleExists)
                        await roleManager.CreateAsync(new IdentityRole() { Name = "Admin"});

                    if (!doesCustomerRoleExists)
                        await roleManager.CreateAsync(new IdentityRole() { Name = "Customer" });

                }
                builder.Run();
            }
            catch (Exception exception)
            {
                //NLog: catch setup errors
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                })
                .UseNLog();  // NLog: Setup NLog for Dependency injection
    }
}
