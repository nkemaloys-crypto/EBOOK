

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EBOOK.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int TeacherId { get; set; }

        [Required]
        public User Teacher { get; set; } = null!;

        [Required]
        public int ClassroomId { get; set; }

        [Required]
        public Classroom Classroom { get; set; } = null!;

        [Required]
        public ICollection<NotebookEntry> Entries { get; set; } = new List<NotebookEntry>();
    }
}
