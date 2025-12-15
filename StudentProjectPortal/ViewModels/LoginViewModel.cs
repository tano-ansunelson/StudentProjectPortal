using System.ComponentModel.DataAnnotations;

namespace StudentProjectPortal.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage ="Error is required")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }= string.Empty;
        public bool RememberMe { get; set; }



    }
}
