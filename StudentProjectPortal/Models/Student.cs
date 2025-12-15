namespace StudentProjectPortal.Models
{
    public class Student
    {
        public int Id { get; set; } 
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? ApplicationUserId { get; set; }

        public ApplicationUser? ApplicationUser { get; set; }    

        public int ClassId { get; set; }
        public Class? Class { get; set; }

        public int Level { get; set; }

        public ICollection<Submission> Submissions { get; set; }=new List<Submission>();


    }
}
