namespace StudentProjectPortal.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }= string.Empty;
        public string Description { get; set; }= string.Empty;

        public int SupervisorId { get; set; }
        public Supervisor? Supervisor { get; set; }

        public int DepartmentId { get; set; }
        public int Level { get; set; }

        //public int ClassId { get; set; }
        //public Class Class { get; set; }

        public DateTime DateCreated { get; set; }= DateTime.UtcNow;

        public string? FilePath { get; set; }
        public ICollection<Submission> Submissions { get; set; }= new List<Submission>();
    }
}

