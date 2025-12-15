using Microsoft.AspNetCore.Identity;

namespace StudentProjectPortal.Data
{
    public class SeedData
    {





        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roles = { "Student", "Supervisor" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }








    }
}
