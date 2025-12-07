using System.ComponentModel.DataAnnotations;

namespace StudentProjectPortal.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        [Display(Name = "Password")]
        public string Password { get; set; }




        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Your Level is required")]
        public string ClassId { get; set; }


        [Required(ErrorMessage ="Program is required")]
        public string Program { get; set; }

        [Required(ErrorMessage =("Student Id required"))]
        public  int StudentId { get; set; }


        
    }



}

