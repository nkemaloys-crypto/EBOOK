
using System.ComponentModel.DataAnnotations;

namespace EBOOK.Models
{
    public class NotebookEntry
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        [Required]
        public int CourseId { get; set; }

        [Required]
        public Course Course { get; set; } = null!;

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public User Author { get; set; } = null!;
    }
}
