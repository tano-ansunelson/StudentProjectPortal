using Microsoft.AspNetCore.Identity;

namespace StudentProjectPortal.Models
{
    public class ApplicationUser : IdentityUser
    {
        
        public string FullName { get; set; }= string.Empty;

        //student fields
        public int? ClassId { get; set; }
        public Class? Class { get; set; }

        //supervisor fields
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }

        //Role(student or supervisor) 
        public string? Role { get; set; }    
       
      
       



    }
}
