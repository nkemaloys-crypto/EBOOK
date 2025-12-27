

using System;
using System.ComponentModel.DataAnnotations;

namespace EBOOK.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        
        // C'est cette ligne qui corrige les erreurs dans les vues Student
        public bool IsDelegate { get; set; } = false; 
        public bool IsAdmin { get; set; } = false; // Par défaut, personne n'est admin

        public int ClassroomId { get; set; }
        public Classroom? Classroom { get; set; }
    }
}
