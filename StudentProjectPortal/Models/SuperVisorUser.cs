using System.ComponentModel.DataAnnotations;

namespace StudentProjectPortal.Models
{
    public class SuperVisorUser
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
   

        public string Department { get; set; }

       
        public string StaffId { get; set; }

    }
}

