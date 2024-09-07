using Bhank.Customer.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bhank.Customer.Api.Infra
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options)
            : base(options) {}

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseNpgsql(
                x => x.MigrationsHistoryTable("__MyMigrationsHistory", "customer_account"));

        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<AddressEntity> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("customer_account");
        }
    }

}   