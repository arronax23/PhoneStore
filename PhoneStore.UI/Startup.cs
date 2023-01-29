using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhoneStore.DAL.Data;
using PhoneStore.DAL.Models;
using System;
using PhoneStore.BLL.Interfaces;
using PhoneStore.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using PhoneStore.UI.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace PhoneStore.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = opt =>
                {
                    opt.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };
                options.Events.OnRedirectToAccessDenied = opt =>
                {
                    opt.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };
            });

            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.Cookie.Name = "PhoneStore.Identity.Cookie";
            //    options.Events = new CookieAuthenticationEvents()
            //    {
            //        OnRedirectToLogin = (ctx) =>
            //        {
            //            if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
            //            {
            //                ctx.Response.StatusCode = 401;
            //            }

            //            return Task.CompletedTask;
            //        },
            //        OnRedirectToAccessDenied = (ctx) =>
            //        {
            //            if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
            //            {
            //                ctx.Response.StatusCode = 403;
            //            }

            //            return Task.CompletedTask;
            //        }
            //    };
            //    //options.Cookie.Name = "Identity.Cookie";
            //    //options.LoginPath = "/Login";
            //});

            //services.AddIdentityServer()
            //    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //services.AddAuthentication("some")
            //    .AddCookie("some",config => {
            //        config.AccessDeniedPath = "/login";
            //    });



            //.AddCookie("PhoneShopCookie", config => 
            //{
            //    config.Cookie = new Microsoft.AspNetCore.Http.CookieBuilder() { Name = "PhoneShopCookie", Expiration = TimeSpan.FromHours(2) };
            //});
            //.AddIdentityServerJwt();


            //services.AddAuthorization(config => 
            //{
            //    config.AddPolicy("Admin", conf =>
            //    {
            //        conf.RequireRole("Admin");
            //    });
            //    config.AddPolicy("Customer", conf =>
            //    {
            //        conf.RequireRole("Customer");
            //    });
            //});

            services.AddAuthorization();
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Admin", config =>
            //    {
            //        config.RequireClaim("Role", "Admin");
            //    });
            //});

            services.AddControllersWithViews();
            services.AddRazorPages();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });


           services.AddSwaggerGen(config => 
           {
               var security = new OpenApiSecurityRequirement()
               {
                   {new OpenApiSecurityScheme(){
                       Reference = new OpenApiReference()
                       {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                       }
                    }
                   , new List<string>()}
               };

               config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
               {
                   Description = "JWT authorization header using bearer scheme",
                   Name = "Authorization",
                   In = ParameterLocation.Header,
                   Type = SecuritySchemeType.ApiKey
               });

               config.AddSecurityRequirement(security);
           });
           //services.AddSwaggerGen(c => 
           // {
           //     c.SwaggerDoc("v1", new OpenApiInfo()
           //     {
           //         Description = "Desc"
           //     }) ;
           // });

            services.AddScoped<IPhonesService, PhonesService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IOrdersService, OrdersService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            //app.UseIdentityServer();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(options => 
            { 
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "PhoneStore API");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
