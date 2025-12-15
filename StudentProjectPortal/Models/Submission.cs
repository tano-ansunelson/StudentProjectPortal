namespace StudentProjectPortal.Models
{
    public class Submission
    {
        public int Id { get; set; }



        public int StudentId { get; set; }
        public Student? Student { get; set; }

        public int ProjectId { get; set; }
        public Project? Project { get; set; }
        public string? FilePath { get; set; }
        public string? Grade { get; set; }
        
        public DateTime SubmissionDate { get; set; } = DateTime.UtcNow;

    }
}
