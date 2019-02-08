using System;
using CarDealer.Models;
using Microsoft.EntityFrameworkCore;

namespace CarDealer.Data
{
    public class CarDealerContext : DbContext
    {
        public CarDealerContext()
        {
        }

        public CarDealerContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<PartCar> PartCars { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Sale>()
                .HasOne(s => s.Car)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.CarId);

            builder.Entity<Sale>()
                .HasOne(s => s.Customer)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.CustomerId);

            builder.Entity<PartCar>()
                .HasKey(e => new { e.PartId, e.CarId });

            builder.Entity<PartCar>()
                .HasOne(pc => pc.Part)
                .WithMany(p => p.PartCars)
                .HasForeignKey(pc => pc.PartId);

            builder.Entity<PartCar>()
                .HasOne(pc => pc.Car)
                .WithMany(p => p.PartCars)
                .HasForeignKey(c => c.CarId);

        }
    }
}

