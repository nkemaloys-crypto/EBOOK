namespace eTextBook.Models
{
    public class Presence
    {
        public int Id { get; set; }
        public string StudentName { get; set; } = "";
        public bool IsPresent { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public int CourseId { get; set; }
        public Course? Course { get; set; }
    }
}
