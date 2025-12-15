

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EBOOK.Models
{
    public class Classroom
    {
        public int Id { get; set; }

        [Required] // La propriété doit toujours avoir une valeur
        public string Name { get; set; } = string.Empty;

        public int Year { get; set; }

        [Required]
        public ICollection<Student> Students { get; set; } = new List<Student>();

        [Required]
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
