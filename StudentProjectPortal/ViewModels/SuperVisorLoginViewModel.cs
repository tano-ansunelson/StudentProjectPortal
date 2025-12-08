using System.ComponentModel.DataAnnotations;

namespace StudentProjectPortal.ViewModels
{
    public class SuperVisorLoginViewModel
    {

        [Required(ErrorMessage = "StaffId is required")]
        public string StaffId { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
