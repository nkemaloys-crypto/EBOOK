using System.ComponentModel.DataAnnotations;

namespace EBOOK.Models
{
    public class Presence
    {
        public int Id { get; set; }

        // AJOUTE CES TROIS LIGNES :
        public DateTime Date { get; set; } = DateTime.Now;
        public bool IsPresent { get; set; }
        public string? Remarks { get; set; }

        // Liens avec les autres tables
        public int StudentId { get; set; }
        public Student? Student { get; set; }

        public int CourseId { get; set; }
        public Course? Course { get; set; }
    }
}