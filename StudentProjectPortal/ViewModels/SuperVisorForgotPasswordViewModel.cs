using System.ComponentModel.DataAnnotations;

namespace StudentProjectPortal.ViewModels
{
    public class SuperVisorForgotPasswordViewModel
    {

        [Required(ErrorMessage ="Email is required")]
        public string Email { get; set; }
    }
}
