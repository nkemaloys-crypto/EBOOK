

using System;
using System.ComponentModel.DataAnnotations;

namespace EBOOK.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        [Required]
        public int ClassroomId { get; set; }

        [Required]
        public Classroom Classroom { get; set; } = null!;
    }
}
