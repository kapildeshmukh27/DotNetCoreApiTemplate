using DotNetCoreApiTemplate.DTOs;
using DotNetCoreApiTemplate.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreApiTemplate.Data
{
    public class ApplicationDbContext : DbContext   
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Example tables
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CustomerOrderDto> CustomerOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API configurations here
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<CustomerOrderDto>().HasNoKey().ToView(null);
        }
    }
}
