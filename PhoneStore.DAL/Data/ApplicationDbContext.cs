using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhoneStore.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace PhoneStore.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Phone> Phones { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PhoneOrder> PhoneOrders { get; set; }
        public DbSet<OrderStatusWorkflow> OrderStatusWorkflows { get; set; }
    }
}
