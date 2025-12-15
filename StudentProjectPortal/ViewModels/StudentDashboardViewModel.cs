using StudentProjectPortal.Models;

namespace StudentProjectPortal.ViewModels
{
    public class StudentDashboardViewModel
    {
        public List<Project> AvailableProject { get; set; } = new();
        public List<Submission> MySubmissions { get; set; } = new();
    }
}
