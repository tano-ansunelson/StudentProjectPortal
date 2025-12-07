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


        
    }



}


//if will be adding these field later when i wake 

//using System.ComponentModel.DataAnnotations;

//namespace StudentProjectPortal.ViewModels
//{
//    public class RegisterViewModel
//    {
//        [Required(ErrorMessage = "Name is required")]
//        public string Name { get; set; }

//        [Required(ErrorMessage = "Email is required")]
//        [EmailAddress(ErrorMessage = "Invalid email address")]
//        public string Email { get; set; }

//        [Required(ErrorMessage = "Password is required")]
//        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 characters")]
//        [DataType(DataType.Password)]
//        [Compare("ConfirmPassword", ErrorMessage = "Passwords do not match")]
//        [Display(Name = "Password")]
//        public string Password { get; set; }

//        [Required(ErrorMessage = "Confirm Password is required")]
//        [DataType(DataType.Password)]
//        [Display(Name = "Confirm Password")]
//        public string ConfirmPassword { get; set; }

//        [Required(ErrorMessage = "Your Level is required")]
//        public string ClassId { get; set; }

//        // New fields with validation

//        [Required(ErrorMessage = "Program is required")]
//        public string Program { get; set; }  // e.g., Chemistry

//        [Required(ErrorMessage = "Year is required")]
//        [RegularExpression(@"^\d{4}$", ErrorMessage = "Year must be a 4-digit number")] // e.g., 2025
//        public string Year { get; set; }     // e.g., Senior

  

//        [Required(ErrorMessage = "Student ID is required")]
//        [StringLength(15, MinimumLength = 5, ErrorMessage = "Student ID must be between 5 and 15 characters")]
//        public string StudentId { get; set; }
//    }
//}
