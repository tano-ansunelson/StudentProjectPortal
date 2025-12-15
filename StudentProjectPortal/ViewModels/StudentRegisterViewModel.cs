using StudentProjectPortal.Models;
using System.ComponentModel.DataAnnotations;

namespace StudentProjectPortal.ViewModels
{
    public class StudentRegisterViewModel
    {
        [Required(ErrorMessage ="Full name is required")]
        [Display(Name ="Full Name")]
        public string FullName { get; set; }= string.Empty;
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage ="Invalid email address")]
        public string Email { get; set; }= string.Empty;
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100,MinimumLength =8, ErrorMessage ="Password must be at least 8 characters")]
        public string Password { get; set; }= string.Empty;

        [Required(ErrorMessage ="Please select a class")]
        [Display(Name ="Class")]
        public int ClassId { get; set; }
        [Required(ErrorMessage ="Please choose a level")]
        [Display(Name ="Level")]
        public int Level { get; set; }

        public List<Class>? classes { get; set; }




}
}
