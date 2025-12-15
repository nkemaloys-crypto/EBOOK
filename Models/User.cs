
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EBOOK.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<NotebookEntry> NotebookEntries { get; set; } = new List<NotebookEntry>();
    }
}


