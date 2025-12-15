using EBOOK.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace EBOOK.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<NotebookEntry> NotebookEntries { get; set; }
        public DbSet<Presence> Presences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ---------- Classrooms ----------
            modelBuilder.Entity<Classroom>().HasData(
                new Classroom { Id = 1, Name = "Terminales S-2025", Year = 2025 }
            );

            // ---------- Users (Teachers) ----------
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "Sophie", LastName = "Dubois", Email = "sophie.dubois@ebook.edu" },
                new User { Id = 2, FirstName = "Marc", LastName = "Durand", Email = "marc.durand@ebook.edu" }
            );

            // ---------- Courses ----------
            modelBuilder.Entity<Course>().HasData(
                  new Course { Id = 1, Title = "Mathématiques Avancées", Description = "Analyse et Algèbre.", ClassroomId = 1, TeacherId = 1 },
                  new Course { Id = 2, Title = "Physique-Chimie", Description = "Mécanique quantique et chimie organique.", ClassroomId = 1, TeacherId = 1 },
                  new Course { Id = 3, Title = "Informatique", Description = "Algorithmique et programmation C#.", ClassroomId = 1, TeacherId = 1 },
                  new Course { Id = 4, Title = "Biologie", Description = "Cellules, ADN et génétique.", ClassroomId = 1, TeacherId = 1 }
            );


            // ---------- Students ----------
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, FirstName = "Alice", LastName = "Mang", DateOfBirth = new DateTime(2007, 5, 10), ClassroomId = 1 },
                new Student { Id = 2, FirstName = "Bruno", LastName = "Leukeng", DateOfBirth = new DateTime(2006, 11, 25), ClassroomId = 1 },
                new Student { Id = 3, FirstName = "Chloé", LastName = "Pagou", DateOfBirth = new DateTime(2007, 1, 15), ClassroomId = 1 }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
