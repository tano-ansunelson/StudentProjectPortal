namespace StudentProjectPortal.Models
{
    public class Supervisor
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }

        public string? ApplicationUserId { get; set; }

        public ApplicationUser? ApplicationUser { get; set; }

        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        public string? ProfileImagePath { get; set; }

        public ICollection<Project> Projects { get; set; }=new List<Project>();



    }
}
