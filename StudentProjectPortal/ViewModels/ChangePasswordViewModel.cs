using System.ComponentModel.DataAnnotations;

namespace StudentProjectPortal.ViewModels
{
    public class ChangePasswordViewModel
    {
          
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Current Password is required")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 8 and 20 characters")]
        [Compare("ConfirmNewPassword", ErrorMessage = "password does not match")]
        public string NewPassword { get; set; }


        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]

        public string ConfirmNewPassword { get; set; }
    }
}
    

