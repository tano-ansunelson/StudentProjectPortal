using System.ComponentModel.DataAnnotations;

namespace StudentProjectPortal.ViewModels
{
    public class SuperVisorResetPasswordViewModel
    {

        [Required(ErrorMessage ="Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage ="New Password is required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage ="ConfirmPassword is requred")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
}
}
