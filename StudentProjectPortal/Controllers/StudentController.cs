using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentProjectPortal.Data;
using StudentProjectPortal.Models;
using StudentProjectPortal.ViewModels;

namespace StudentProjectPortal.Controllers
{
    public class StudentsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;


        public StudentsController(
             UserManager<ApplicationUser> userManager,
             SignInManager<ApplicationUser> signInManager,
             ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        [Authorize(Roles ="Student")]
        public async  Task<IActionResult> Index() { 

            var user = await _userManager.GetUserAsync(User);
            if(user==null)
            {
                return RedirectToAction("Login", "Account");
            }

            var student =_context.Students
                .Include(s=>s.Class)
                .FirstOrDefault(s=>s.ApplicationUserId== user.Id);

            if(student== null)
                return RedirectToAction("Register");


            //match class name with department name

            int level = student.Level;
            string deptName= student.Class.Name;
            //get project

            var projects = await _context.Projects
                 .Include(p =>p.Supervisor)
                    .ThenInclude(s => s.Department)
                    .Where( p=> p.Supervisor.Department.Name== deptName && p.Level== level)
                    .ToListAsync();

            //get submission
            var submisssion= await _context.Submissions
                .Where(s=>s.StudentId== student.Id)
                .Include(s=>s.Project)
                .ToListAsync();
            var vm = new StudentDashboardViewModel
            {
                AvailableProject = projects,
                MySubmissions = submisssion
            };  

            return View(vm);  
        } 
        // Controller actions go here   
        [HttpGet]
        public IActionResult Register()
        {
            var vm = new StudentRegisterViewModel
            {
                classes = _context.Classes.ToList()
            };
            return View("~/Views/Account/StudentRegister.cshtml", vm);

        }


        //student registration
        [HttpPost]

        public async Task<IActionResult> Register(StudentRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.classes = _context.Classes.ToList();
                return View("~/Views/Account/StudentRegister.cshtml",model);
            }
            if (!model.Email.EndsWith("@st.knust.edu.gh"))
            {
                ModelState.AddModelError("Email", "Student email must end with @st.knust.edu.gh");
                model.classes = _context.Classes.ToList();
                return View("~/Views/Account/StudentRegister.cshtml",model);

            }
            // identity user
            var user = new ApplicationUser {
                UserName = model.Email,
                Email = model.Email,
                FullName=model.FullName, 
                ClassId=model.ClassId, 
                Role="Student" 
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {

                foreach (var err in result.Errors)
                    ModelState.AddModelError("", err.Description);
                
                

                model.classes = _context.Classes.ToList();
                return View("~/Views/Account/StudentRegister.cshtml", model);


            }
            await _userManager.AddToRoleAsync(user, "Student");
             

            //insert to student table
            var student = new Student
            {

                ApplicationUserId = user.Id,
                ClassId = model.ClassId,
                FullName = model.FullName,
                Email = model.Email,
                Level=model.Level,
            };
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            //login
            await _signInManager.SignInAsync(user, false);
            // redirect to student dashboard
            return RedirectToAction("Index","Students");
          


        }


        [HttpPost]
        [Authorize(Roles ="Student")]
        public async Task<IActionResult> SubmitProject(int ProjectId, IFormFile UploadFile) {
            var user = await _userManager.GetUserAsync(User);
            if(user == null)
            {
                return RedirectToAction("Login", "Account");
            }   

            var student = _context.Students
                .FirstOrDefault(s => s.ApplicationUserId == user.Id);
            if(student==null) 
                return Unauthorized();

            var existing = _context.Submissions.FirstOrDefault(s=>s.StudentId== student.Id && s.ProjectId== ProjectId);
            if(existing != null)
            {
                ModelState.AddModelError("", "You have already submitted this project.");
                return RedirectToAction("Index");
            }

            if(UploadFile == null || UploadFile.Length == 0)
            {
                TempData["Error"]= "Please select a file to upload.";
                return RedirectToAction("Index");
            }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/submissions");
            Directory.CreateDirectory(uploadsFolder);

            var fileName = Guid.NewGuid() + Path.GetExtension(UploadFile.FileName);
            var filePath= Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await UploadFile.CopyToAsync(stream);
            }

            var submission = new Submission
            {
                StudentId = student.Id,
                ProjectId = ProjectId,
                FilePath = "/uploads/submissions/" + fileName,
                SubmissionDate = DateTime.UtcNow
            };
            _context.Submissions.Add(submission);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Project submitted successfully.";
            return RedirectToAction("Index");
        }




        public async Task<IActionResult> MySubmissions() { 
        
        var userId = _userManager.GetUserId(User);

            var submissions= await _context.Submissions
                .Include(s=>s.Project)
                .Include(s=>s.Student)
                .Where(s=>s.Student.ApplicationUserId== userId)
                .OrderByDescending (s=>s.SubmissionDate)
                .ToListAsync();


            return View(submissions);    

        }









    }
}