namespace StudentProjectPortal.Models
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; }= string.Empty;

        public ICollection<Supervisor> Supervisors { get; set; }= new List<Supervisor>();


    }
}
