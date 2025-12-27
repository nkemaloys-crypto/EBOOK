using System;
using System.Collections.Generic;

namespace EBOOK.Models
{
    public class TakeAttendanceViewModel
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = string.Empty;
        public string ClassroomName { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
        public List<StudentAttendanceItem> Students { get; set; } = new List<StudentAttendanceItem>();
    }

    public class StudentAttendanceItem
    {
        public int StudentId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public bool IsPresent { get; set; } = true;
        public string? Remarks { get; set; }
    }
}