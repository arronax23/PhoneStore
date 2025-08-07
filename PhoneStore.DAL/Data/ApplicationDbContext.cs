using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PhoneStore.DAL.Models;

namespace PhoneStore.DAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PhoneOrder> PhoneOrders { get; set; }
        public DbSet<OrderStatusWorkflow> OrderStatusWorkflows { get; set; }
        public DbSet<OrderStatus> OrderStatusLookup{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder
            //    .Entity<OrderStatusWorkflow>()
            //    .HasOne<OrderStatus>()
            //    .WithMany();


            //modelBuilder
            //    .Entity<Order>()
            //    .HasOne<OrderStatus>()
            //    .WithMany();

            //modelBuilder
            //    .Entity<OrderStatus>()
            //    .HasMany<OrderStatusWorkflow>()
            //    .WithOne()
            //    .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<OrderStatus>()
                .HasData(
                    Enum.GetValues(typeof(OrderStatusId))
                    .Cast<OrderStatusId>()
                    .Select(os => new OrderStatus()
                    {
                        OrderStatusId = os,
                        Status = os.ToString()
                    })
            );

        }
    }
}
