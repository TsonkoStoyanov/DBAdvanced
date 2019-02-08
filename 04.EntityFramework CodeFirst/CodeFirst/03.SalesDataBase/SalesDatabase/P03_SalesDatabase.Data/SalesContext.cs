using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Data
{
    public class SalesContext : DbContext
    {
        public SalesContext()
        {
        }

        public SalesContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Store> Stores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.connection);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Name)
                    .HasMaxLength(50)
                    .IsRequired()
                    .IsUnicode();

                entity.Property(p => p.Description)
                    .HasMaxLength(250)
                    .IsRequired(false)
                    .HasDefaultValue("No description");

                entity.HasMany(p => p.Sales)
                    .WithOne(e => e.Product)
                    .HasForeignKey(e => e.ProductId);
            });

            builder.Entity<Customer>(entity =>
            {
                entity.Property(c => c.Name)
                    .HasMaxLength(100)
                    .IsRequired()
                    .IsRequired();

                entity.Property(c => c.Email)
                    .HasMaxLength(80)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasMany(c => c.Sales)
                    .WithOne(e => e.Customer)
                    .HasForeignKey(e => e.CustomerId);
            });

            builder.Entity<Store>(entity =>
            {
                entity.Property(c => c.Name)
                    .HasMaxLength(80)
                    .IsRequired()
                    .IsRequired();


                entity.HasMany(c => c.Sales)
                    .WithOne(e => e.Store)
                    .HasForeignKey(e => e.StoreId);
            });

            builder.Entity<Sale>(entity =>
            {
                entity.Property(s => s.Date)
                    .HasDefaultValueSql("GETDATE()");
            });
        }
    }
}
