using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data
{
    public class HospitalContext : DbContext
    {
        public HospitalContext()
        {
        }

        public HospitalContext(DbContextOptions options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Diagnose> Diagnoses { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<PatientMedicament> PatientsMedicaments { get; set; }
        public DbSet<Visitation> Visitations { get; set; }
        public DbSet<Doctor> Doctors { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.connection);
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PatientMedicament>()
                .HasKey(x => new { x.MedicamentId, x.PatientId });



            modelBuilder.Entity<Patient>()
                .HasKey(x => x.PatientId);

            modelBuilder.Entity<Patient>().Property(x => x.FirstName)
                .HasMaxLength(50)
                .IsUnicode();

            modelBuilder.Entity<Patient>().Property(x => x.LastName)
                .HasMaxLength(50)
                .IsUnicode();

            modelBuilder.Entity<Patient>().Property(x => x.Address)
                .HasMaxLength(250)
                .IsUnicode();

            modelBuilder.Entity<Patient>().Property(x => x.Email)
                .HasMaxLength(80)
                .IsUnicode(false);

            modelBuilder.Entity<Patient>().HasMany(x => x.Prescriptions)
                .WithOne(x => x.Patient)
                .HasForeignKey(x => x.PatientId);

            modelBuilder.Entity<Patient>().HasMany(x => x.Visitations)
                .WithOne(x => x.Patient)
                .HasForeignKey(x => x.PatientId);

            modelBuilder.Entity<Patient>().HasMany(x => x.Diagnoses)
                .WithOne(x => x.Patient)
                .HasForeignKey(x => x.PatientId);



            
            modelBuilder.Entity<Visitation>()
                .HasKey(x => x.VisitationId);

            modelBuilder.Entity<Visitation>()
                .Property(x => x.Comments)
                .HasMaxLength(250)
                .IsUnicode();

            
            modelBuilder.Entity<Diagnose>()
                .HasKey(x => x.DiagnoseId);

            modelBuilder.Entity<Diagnose>()
                .Property(x => x.Name)
                .HasMaxLength(50)
                .IsUnicode();

            modelBuilder.Entity<Diagnose>()
                .Property(x => x.Comments)
                .HasMaxLength(250)
                .IsUnicode();


            modelBuilder.Entity<Medicament>()
                .HasKey(x => x.MedicamentId);

            modelBuilder.Entity<Medicament>().Property(x => x.Name)
                .HasMaxLength(50)
                .IsUnicode();

            modelBuilder.Entity<Medicament>()
                .HasMany(x => x.Prescriptions) 
                .WithOne(x => x.Medicament)
                .HasForeignKey(x => x.MedicamentId);


            modelBuilder.Entity<Doctor>()
                .HasKey(x => x.DoctorId);


            modelBuilder.Entity<Doctor>().Property(x => x.Name)
                .HasMaxLength(100)
                .IsUnicode();

            modelBuilder.Entity<Doctor>().Property(x => x.Specialty)
                .HasMaxLength(100)
                .IsUnicode();


            modelBuilder.Entity<Doctor>()
                .HasMany(x => x.Visitations)
                .WithOne(x => x.Doctor)
                .HasForeignKey(x => x.DoctorId);
        }
    }
}
