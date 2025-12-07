using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using StudentProjectPortal.Data;
using StudentProjectPortal.Models;
using StudentProjectPortal.ViewModels;
using System.Linq;

namespace StudentProjectPortal.Controllers
{
    public class SupervisorController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<SuperVisorUser> _passwordHasher;

        public SupervisorController(AppDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<SuperVisorUser>();
        }

        public IActionResult Index()
        {
            return View();
        }

        // ========================
        // Registration
        // ========================
        [HttpGet]
        public IActionResult SuperVisorRegister()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SuperVisorRegister(SuperVisorRegisterModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Prevent duplicate StaffId
            if (_context.SuperVisors.Any(s => s.StaffId == model.StaffId))
            {
                ModelState.AddModelError("StaffId", "Staff ID already exists.");
                return View(model);
            }

            // Check password confirmation
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
                return View(model);
            }

            var supervisor = new SuperVisorUser
            {
                FullName = model.Name,
                Email = model.Email,
                StaffId = model.StaffId,
                Department = model.Department,
            };

            supervisor.Password = _passwordHasher.HashPassword(supervisor, model.Password);

            _context.SuperVisors.Add(supervisor);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "🎉 Account created successfully.";
            return RedirectToAction("SuperVisorLogin");
        }



        // ========================
        // Login
        // ========================
        [HttpGet]
        public IActionResult SuperVisorLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SuperVisorLogin(SuperVisorLoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var supervisor = _context.SuperVisors.FirstOrDefault(s => s.StaffId == model.StaffId);

            if (supervisor == null)
            {
                ModelState.AddModelError("", "Invalid Staff ID or password.");
                return View(model);
            }

            var result = _passwordHasher.VerifyHashedPassword(supervisor, supervisor.Password, model.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("", "Invalid Staff ID or password.");
                return View(model);
            }

            // Set session
            HttpContext.Session.SetInt32("SupervisorId", supervisor.Id);
            HttpContext.Session.SetString("SupervisorStaffId", supervisor.StaffId);

            TempData["SuccessMessage"] = "Successfully logged in.";

            return RedirectToAction("Index", "Supervisor");
        }




        // ========================
        // Forgot Password
        // ========================
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(SuperVisorForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var supervisor = _context.SuperVisors.FirstOrDefault(x => x.Email == model.Email);

            if (supervisor == null)
            {
                ModelState.AddModelError("", "Email not found.");
                return View(model);
            }

            // Redirect to Reset Screen
            return RedirectToAction("ResetPassword", new { email = supervisor.Email });
        }



        // Reset Password (GET)
     
        [HttpGet]
        public IActionResult ResetPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("ForgotPassword");

            return View(new SuperVisorResetPasswordViewModel { Email = email });
        }



        // ========================
        // Reset Password (POST)
        // ========================
        [HttpPost]
        public IActionResult ResetPassword(SuperVisorResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var supervisor = _context.SuperVisors.FirstOrDefault(x => x.Email == model.Email);

            if (supervisor == null)
            {
                ModelState.AddModelError("", "Supervisor not found.");
                return View(model);
            }

            supervisor.Password = _passwordHasher.HashPassword(supervisor, model.NewPassword);

            _context.SuperVisors.Update(supervisor);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Password updated successfully.";
            return RedirectToAction("SuperVisorLogin");
        }




        // ========================
        // Logout
        // ========================
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("SuperVisorLogin", "SuperVisor");
        }

      
    }
}
