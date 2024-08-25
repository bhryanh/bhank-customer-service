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
                x => x.MigrationsHistoryTable("__MyMigrationsHistory", "customer"));

        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<AddressEntity> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("customer");

            modelBuilder.Entity<CustomerEntity>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<CustomerEntity>()
                .Property(p => p.LastName)
                .IsRequired(false);

            modelBuilder.Entity<CustomerEntity>()
                .HasOne(c => c.Address)
                .WithOne(a => a.Customer)
                .HasForeignKey<CustomerEntity>(c => c.AddressId)
                .OnDelete(DeleteBehavior.Restrict);    

            modelBuilder.Entity<AddressEntity>()
                .HasKey(c => c.Id);
      
        }
    }

}   