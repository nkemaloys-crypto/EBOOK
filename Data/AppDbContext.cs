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
            base.OnModelCreating(modelBuilder);

            // ---------- 1. Classrooms (Classes) ----------
            modelBuilder.Entity<Classroom>().HasData(
                new Classroom { Id = 1, Name = "Administration", Year = 2025 },
                new Classroom { Id = 2, Name = "Terminales S-2025", Year = 2025 }
            );

            // ---------- 2. Users (Enseignants) ----------
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "Sophie", LastName = "Dubois", Email = "sophie.dubois@ebook.edu" },
                new User { Id = 2, FirstName = "Marc", LastName = "Durand", Email = "marc.durand@ebook.edu" }
            );

            // ---------- 3. Courses (Cours) ----------
            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Title = "Mathématiques Avancées", Description = "Analyse et Algèbre.", ClassroomId = 2, TeacherId = 1 },
                new Course { Id = 2, Title = "Physique-Chimie", Description = "Mécanique quantique et chimie organique.", ClassroomId = 2, TeacherId = 1 },
                new Course { Id = 3, Title = "Informatique", Description = "Algorithmique et programmation C#.", ClassroomId = 2, TeacherId = 1 },
                new Course { Id = 4, Title = "Biologie", Description = "Cellules, ADN et génétique.", ClassroomId = 2, TeacherId = 1 }
            );

            // ---------- 4. Students (Étudiants + Admin) ----------
            modelBuilder.Entity<Student>().HasData(
                // Le Super Utilisateur (Admin)
                new Student 
                { 
                    Id = 1, 
                    FirstName = "Admin", 
                    LastName = "System", 
                    DateOfBirth = new DateTime(2000, 1, 1), 
                    ClassroomId = 1, 
                    IsAdmin = true, 
                    IsDelegate = false 
                },
                // Tes étudiants de test (décalés à partir de l'ID 2)
                new Student { Id = 2, FirstName = "Alice", LastName = "Mang", DateOfBirth = new DateTime(2007, 5, 10), ClassroomId = 2 },
                new Student { Id = 3, FirstName = "Bruno", LastName = "Leukeng", DateOfBirth = new DateTime(2006, 11, 25), ClassroomId = 2 },
                new Student { Id = 4, FirstName = "Chloé", LastName = "Pagou", DateOfBirth = new DateTime(2007, 1, 15), ClassroomId = 2 }
            );
        }
    }
}