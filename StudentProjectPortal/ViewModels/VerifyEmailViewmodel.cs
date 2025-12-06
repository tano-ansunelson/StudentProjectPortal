using System.ComponentModel.DataAnnotations;

namespace StudentProjectPortal.ViewModels
{
    public class VerifyEmailViewmodel

    {
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
