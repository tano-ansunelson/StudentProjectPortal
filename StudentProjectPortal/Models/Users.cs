using Microsoft.AspNetCore.Identity;
namespace StudentProjectPortal.Models
{
    public class Users:IdentityUser
    {
        public string FullName { get; set; }

        // Student Level/Class
        public string ClassId { get; set; }
        public string Program { get; set; }
        public int StudentId { get; set; }
        public string StaffId { get; set; } 

    }

}

