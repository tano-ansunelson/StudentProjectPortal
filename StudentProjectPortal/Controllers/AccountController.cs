using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentProjectPortal.Models;
using StudentProjectPortal.ViewModels;

namespace StudentProjectPortal.Controllers
{
    public class AccountController : Controller

    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
       
        public AccountController(
             UserManager<ApplicationUser> userManager,
             SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated) {

                if (User.IsInRole("Supervisor"))
                    return RedirectToAction("Index", "Supervisor");

                if (User.IsInRole("Student"))
                   return RedirectToAction("Index", "Students");

             }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login (LoginViewModel model)
        {
          if (!ModelState.IsValid)
                return View(model);
          if(string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                ModelState.AddModelError("", "Email and Password are required.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(
                model.Email!,
                model.Password!,
                model.RememberMe,
                 false );

            if (!result.Succeeded) {
                ModelState.AddModelError("", "Invalid login attempt");
                return View();
            
            }
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                await _signInManager.SignOutAsync();
                ModelState.AddModelError("", "Login failed. User not found.");
                return View(model);
            }
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("Student"))
                return RedirectToAction("Index", "Students");
            if (roles.Contains("Supervisor"))
                return RedirectToAction("Index", "Supervisor");

            return RedirectToAction("Index", "Home");
        }



        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        
    }
}
