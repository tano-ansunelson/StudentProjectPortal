using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;

using StudentProjectPortal.Data;
using StudentProjectPortal.Models;
using StudentProjectPortal.Repositories;
using StudentProjectPortal.ViewModels;

namespace StudentProjectPortal.Controllers
{


    public class SupervisorController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IProjectRepository _projectRepo;
        private readonly ISupervisorRepository _supervisorRepo;
        private readonly ISubmissionRepository _submissionRepo;

        public SupervisorController(
             UserManager<ApplicationUser> userManager,
             SignInManager<ApplicationUser> signInManager,
             ApplicationDbContext context,
             IProjectRepository projecRepo,
             ISupervisorRepository supervisorRepo,
             ISubmissionRepository submissionRepo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _projectRepo = projecRepo;
            _supervisorRepo = supervisorRepo;
            _submissionRepo = submissionRepo;
        }
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> Index() {
            var user = await _userManager.GetUserAsync(User);

            if (user == null) {
                return RedirectToAction("Login", "Account");
            }
            var userId = user.Id;

            var supervisor = _context.Supervisors
                .FirstOrDefault(s => s.ApplicationUserId == userId);

            if (supervisor == null) {

                return RedirectToAction("Registration");
            }

            var projects = await _projectRepo.GetBySupervisorAsync(supervisor.Id);
            ViewBag.Classes = _context.Classes.ToList();


            return View(projects);
        }
        [HttpGet]
        public IActionResult Registration()
        {
            var vm = new SupervisorRegisterViewModel
            {
                Departments = _context.Departments.ToList()
            };
            return View("~/Views/Account/SupervisorRegister.cshtml", vm);
        }

        //supervisor registration
        [HttpPost]

        public async Task<IActionResult> Registration(SupervisorRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Departments = _context.Departments.ToList();
                return View("~/Views/Account/SupervisorRegister.cshtml", model);
            }
            if (!model.Email.EndsWith("@knust.edu.gh"))
            {
                ModelState.AddModelError("Email", "Supervisor email must end with @knust.edu.gh");
                model.Departments = _context.Departments.ToList();
                return View("~/Views/Account/SupervisorRegister.cshtml", model);
            }
            //Identity user
            var user = new ApplicationUser
            { UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                DepartmentId = model.DepartmentId,
                Role = "Supervisor"
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);


                model.Departments = _context.Departments.ToList();
                return View("~/Views/Account/SupervisorRegister.cshtml", model);

            }
            //Roles
            await _userManager.AddToRoleAsync(user, "Supervisor");

            //insert to supervisor table 
            var supervisor = new Supervisor
            {
                ApplicationUserId = user.Id,
                DepartmentId = model.DepartmentId,
                FullName = model.FullName,
                Email = model.Email,



            };
            _context.Supervisors.Add(supervisor);
            await _context.SaveChangesAsync();

            //login
            await _signInManager.SignInAsync(user, false);
            // redirect to supervisor dashboard
            return RedirectToAction("Index", "Supervisor");
        }

        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> Profile()
        {
            var userId = _userManager.GetUserId(User);
            var supervisor = await _supervisorRepo.GetByUserIdAsync(userId);


            if (supervisor == null)
            {
                return RedirectToAction("Index");
            }
            return View(supervisor);
        }

        // editing profile
        [HttpGet]
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> EditProfile()
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var supervisor = await _supervisorRepo.GetByUserIdAsync(userId);
            if (supervisor == null)
            {
                return RedirectToAction("Index");
            }

            var vm = new SupervisorEditProfileViewModel
            {
                Id = supervisor.Id,
                FullName = supervisor.FullName,
                Email = supervisor.Email,
                DepartmentId = supervisor.DepartmentId,
                Departments = _context.Departments.ToList()


            };
            return View(vm);

        }

        [HttpPost]
        [Authorize(Roles = "Supervisor")]

        public async Task<IActionResult> EditProfile(SupervisorEditProfileViewModel model)
        {
            if (!ModelState.IsValid) {

                model.Departments = _context.Departments.ToList();
                return View(model);

            }

            var supervisor = await _context.Supervisors.FindAsync(model.Id);
            if (supervisor == null) {
                return NotFound();
            }
            supervisor.FullName = model.FullName;
            supervisor.Email = model.Email;
            supervisor.DepartmentId = model.DepartmentId;


            if (model.ProfileImage != null) {

                var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/profiles");
                Directory.CreateDirectory(uploads);


                var fileName = Guid.NewGuid() + Path.GetExtension(model.ProfileImage.FileName);
                var filePath = Path.Combine(uploads, fileName);


                using var stream = new FileStream(filePath, FileMode.Create);
                await model.ProfileImage.CopyToAsync(stream);

                supervisor.ProfileImagePath = "/uploads/profiles/" + fileName;



            }
            await _supervisorRepo.UpdateAsync(supervisor);
            return RedirectToAction("Profile");
        }

        [Authorize(Roles="Supervisor")]
        public IActionResult ChangePassword()=> View();


        [Authorize(Roles ="Supervisor")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword)
        {
            var user = await _userManager.GetUserAsync(User);

            if(user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var result = await _userManager.ChangePasswordAsync(

                user,
                currentPassword,
                newPassword
                );
            if(!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            return RedirectToAction("Profile");

        }

        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> ViewSubmissions(int id)
        { 
            var submissions = await _submissionRepo.GetSubmissionForProjectAsync(id);
            if(submissions == null||submissions.Count == 0)
            {

                return View(new List<Submission>());
            }
        
        
        return View(submissions);
        
        }

        [HttpPost]
        [Authorize(Roles = "Supervisor")]

        public async Task<IActionResult> GradeSubmission(int SubmissionId, string grade)
        {
            if (string.IsNullOrEmpty(grade))
            {
                TempData["Error"] = "Please select a grade";
                return RedirectToAction("ViewSubmissions", new { id = SubmissionId });
            }

            await _submissionRepo.UpdateGradeAsync(SubmissionId, grade);
            TempData["Success"] = "Grade updated successfully";

            return Redirect(Request.Headers["Referer"].ToString());
        }





    }
}
