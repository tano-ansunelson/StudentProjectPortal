using Microsoft.AspNetCore.Identity;
namespace StudentProjectPortal.Models
{
    public class Users:IdentityUser
    {
        public string FullName { get; set; }

        // Student Level/Class
        public string ClassId { get; set; }

    }

}

