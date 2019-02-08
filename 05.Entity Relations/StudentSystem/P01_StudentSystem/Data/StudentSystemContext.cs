using System.Reflection.Metadata;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data
{
    public class StudentSystemContext : DbContext
    {
        
        public StudentSystemContext()
        {
        }

        public StudentSystemContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set;}
        public DbSet<Student> Students { get; set;}
        public DbSet<Resource> Resources { get; set;}
        public DbSet<Homework> HomeworkSubmissions { get; set;}
        public DbSet<StudentCourse> StudentCourses { get; set;}
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Student
            modelBuilder.Entity<Student>().Property(s => s.Name)
                .HasMaxLength(100)
                .IsUnicode()
                .IsRequired();

            modelBuilder.Entity<Student>().Property(s => s.PhoneNumber)
                .HasMaxLength(10)
                .IsRequired(false)
                .IsUnicode(false);

            modelBuilder.Entity<Student>().HasMany(x => x.CourseEnrollments).WithOne(x => x.Student)
                .HasForeignKey(x => x.StudentId);

            modelBuilder.Entity<Student>().HasMany(x => x.HomeworkSubmissions).WithOne(x => x.Student)
                .HasForeignKey(x => x.StudentId);


            //Course
            modelBuilder.Entity<Course>().Property(s => s.Name)
                .HasMaxLength(80)
                .IsUnicode()
                .IsRequired();

            modelBuilder.Entity<Course>().Property(s => s.Description)
                .IsRequired(false)
                .IsUnicode();

            modelBuilder.Entity<Course>().HasMany(x => x.StudentsEnrolled).WithOne(x => x.Course)
                .HasForeignKey(x => x.CourseId);

            modelBuilder.Entity<Course>().HasMany(x => x.HomeworkSubmissions).WithOne(x => x.Course)
                .HasForeignKey(x => x.CourseId);

            modelBuilder.Entity<Course>().HasMany(x => x.Resources).WithOne(x => x.Course)
                .HasForeignKey(x => x.CourseId);

            //Resource
            modelBuilder.Entity<Resource>().Property(r => r.Name).HasMaxLength(50).IsUnicode().IsRequired();

            modelBuilder.Entity<Resource>().Property(r => r.Url).IsUnicode(false).IsRequired();

            //Homework

            modelBuilder.Entity<Homework>().Property(h => h.Content).IsUnicode(false).IsRequired();

            //StudentCourse

            modelBuilder.Entity<StudentCourse>().HasKey(sc => new {sc.CourseId, sc.StudentId});
        }
    }
}