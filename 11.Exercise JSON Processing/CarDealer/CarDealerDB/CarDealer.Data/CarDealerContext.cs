namespace CarDealer.Data
{
    using Microsoft.EntityFrameworkCore;

    using CarDealer.Models;

    public class CarDealerContext : DbContext
    {
        public CarDealerContext()
        {
        }

        public CarDealerContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<PartCar> PartCars { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<PartCar>().HasKey(pc => new { pc.Car_Id, pc.Part_Id, });

            builder.Entity<PartCar>().HasOne(pc => pc.Part)
                .WithMany(p => p.PartsCar)
                .HasForeignKey(pc => pc.Part_Id);

            builder.Entity<PartCar>().HasOne(pc => pc.Car)
                .WithMany(c => c.PartsCar)
                .HasForeignKey(pc => pc.Car_Id);

            builder.Entity<Sale>().HasOne(s => s.Customer)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.Customer_Id);

            builder.Entity<Sale>().HasOne(s => s.Car)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.Car_Id);
            
            builder.Entity<Part>().HasOne(p => p.Supplier)
                .WithMany(s => s.Parts)
                .HasForeignKey(p => p.Supplier_Id);
        }
    }
}
