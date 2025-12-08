using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using StudentProjectPortal.Models;
using StudentProjectPortal.ViewModels;
using System.Threading.Tasks;

namespace StudentProjectPortal.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<Users> signInManager;
        private readonly UserManager<Users> userManager;

        //creating constructors for both props
        public AccountController(SignInManager<Users>signInManager ,UserManager<Users>userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

       



        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                Users users = new Users
                {
                    FullName = model.Name,
                    Email = model.Email,
                    UserName = model.Email,
                    ClassId = model.ClassId,
                    Program = model.Program,
                    StudentId = model.StudentId,

                };
                var res = await userManager.CreateAsync(users, model.Password);
                if (res.Succeeded)
                {
                    TempData["SuccessMessage"] = "🎉 Welcome! Your account has been created successfully.";

                    //await userManager.AddToRoleAsync(users , "Student");
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach(var error in res.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
            }
            return View();
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var res = await signInManager.PasswordSignInAsync(
                    model.Email,
                    model.Password,
                    model.RememberMe,
                    lockoutOnFailure: false
                );

                if (res.Succeeded)
                {
                    TempData["SuccessMessage"] = "Great! You are logged in successfully.";

                    return RedirectToAction("Index", "Students");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect Email  or Password");
                    return View(model);
                }
            }

            return View(model);
        }



      

        public IActionResult VerifyEmail()
        {
            return View();
        }


        [HttpPost]
     
        public async Task<IActionResult> VerifyEmail(VerifyEmailViewmodel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                // USER NOT FOUND → ERROR
                if (user == null)
                {
                    ModelState.AddModelError("", "Email not found. Try an existing email.");
                    return View(model);
                }

                // USER FOUND → CONTINUE TO PASSWORD CHANGE
                return RedirectToAction("ChangePassword", "Account", new { username = user.UserName });
            }

            return View(model);
        }


        public IActionResult ChangePassword(string username)
        {
            if(string .IsNullOrEmpty(username))
            {
                return RedirectToAction("VerifyEmail", "Account");
            }
            return View(new ChangePasswordViewModel { Email = username});
        }



     
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid input");
                return View(model);
            }

            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "User not found");
                return View(model);
            }

            // Remove old password
            var remove = await userManager.RemovePasswordAsync(user);

            if (!remove.Succeeded)
            {
                foreach (var error in remove.Errors)
                    ModelState.AddModelError("", error.Description);

                return View(model);
            }

            // Add new password
            var add = await userManager.AddPasswordAsync(user, model.NewPassword);

            if (add.Succeeded)
                return RedirectToAction("Login", "Account");

            foreach (var err in add.Errors)
                ModelState.AddModelError("", err.Description);

            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

    }
}
