using Microsoft.EntityFrameworkCore;
using EBOOK.Models;

namespace EBOOK.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Classroom> Classrooms { get; set; } = default!;
        public DbSet<Student> Students { get; set; } = default!;
        public DbSet<Course> Courses { get; set; } = default!;
        public DbSet<NotebookEntry> NotebookEntries { get; set; } = default!;
        public DbSet<Presence> Presences { get; set; } = default!;
    }
}
