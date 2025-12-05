namespace eTextBook.Models
{
    public class NotebookEntry
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Content { get; set; } = "";

        // Relation
        public int CourseId { get; set; }
        public Course? Course { get; set; }
    }
}
