using AutoMappingObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace AutoMappingObjects
{
    public class AutoMappingObjectsContext : DbContext
    {
        protected AutoMappingObjectsContext()
        {
        }

        public AutoMappingObjectsContext(DbContextOptions options) : base(options)
        {
        }


        private DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
