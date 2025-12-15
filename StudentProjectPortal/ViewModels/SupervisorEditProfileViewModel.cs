using StudentProjectPortal.Models;
using System.ComponentModel.DataAnnotations;
using StudentProjectPortal.ViewModels;

namespace StudentProjectPortal.ViewModels
{
    public class SupervisorEditProfileViewModel
    { 
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }= string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; }= string.Empty;

        public int DepartmentId { get; set; }
        public List<Department> Departments { get; set; } = new List<Department>();

        public IFormFile? ProfileImage { get; set; }








    }
}
