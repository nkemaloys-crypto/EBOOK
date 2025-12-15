using System.ComponentModel.DataAnnotations;

namespace EBOOK.Models
{
    public class Presence
    {
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public Student Student { get; set; } = null!;

        [Required]
        public int CourseId { get; set; }

        [Required]
        public Course Course { get; set; } = null!;

        [Required]
        public bool Status { get; set; }
    }
}
