using StudentProjectPortal.Models;
using System.ComponentModel.DataAnnotations;

namespace StudentProjectPortal.ViewModels
{
    public class SupervisorRegisterViewModel
    {
        [Required(ErrorMessage ="Full name is required")]
        [Display(Name ="Full Name")]
        public string FullName { get; set; }= string.Empty;

        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage ="Invalid email address")]
        public string Email { get; set; }= string.Empty;
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage ="Password must be at least 8 characters")]
        public string Password { get; set; }= string.Empty;

        [Required(ErrorMessage ="Please select a department")]
        [Display(Name ="Department")]
        public int DepartmentId { get; set; }
        public List<Department> Departments { get; set; } = new();




    }
}
