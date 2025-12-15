using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentProjectPortal.Data;
using StudentProjectPortal.Models;
using StudentProjectPortal.Repositories;

namespace StudentProjectPortal.Controllers
{

    [Authorize(Roles ="Supervisor")]
    public class ProjectsController: Controller
    {
        private readonly IProjectRepository _projectRepo;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProjectsController(
            
            IProjectRepository ProjectRepo,
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManage)
        {


            _projectRepo = ProjectRepo;
            _context = context;
            _userManager = userManage;
        }

        [Authorize(Roles ="Supervisor")]
        [HttpPost]
        public async Task<IActionResult> Create(Project project, IFormFile? ProjectFile) { 
        
          var user =await _userManager.GetUserAsync(User);
            if (user == null) { 
            await HttpContext.SignOutAsync();
                return RedirectToAction("Login", "Account");

            }

            var superviosr = _context.Supervisors
                .FirstOrDefault(s=>s.ApplicationUserId==user.Id);
            if (superviosr == null)
            {
               return Unauthorized();
            }

            project.SupervisorId = superviosr.Id;
            //validate class
            project.DepartmentId = superviosr.DepartmentId;


            //file path

            if(ProjectFile != null && ProjectFile.Length > 0)
            {

                var uploadsFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/uploads/projects"
                    );

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);


                var uniqueFileName = Guid.NewGuid() + "_" + ProjectFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ProjectFile.CopyToAsync(stream);
                }

                project.FilePath = "/uploads/projects/" + uniqueFileName;

            }


        
          await _projectRepo.AddAsync(project);


            return RedirectToAction("Index", "Supervisor");
        }
        [Authorize(Roles ="Supervisor")]
        [HttpPost]
        public async Task<IActionResult> Edit(Project model, IFormFile? ProjectFile)
        {
            var project = _context.Projects.FirstOrDefault(p => p.Id == model.Id);

            if (project == null)
                return NotFound();

            project.Title = model.Title;
            project.Description = model.Description;
            project.Level = model.Level;
            project.DateCreated = DateTime.Now;

            // FILE REPLACEMENT
            if (ProjectFile != null && ProjectFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/uploads/projects"
                );

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid() + "_" + ProjectFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ProjectFile.CopyToAsync(stream);
                }

                project.FilePath = "/uploads/projects/" + uniqueFileName;
            }

            _context.Projects.Update(project);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Supervisor");
        }


        [Authorize(Roles ="Supervisor")]
        public async Task<IActionResult> Delete(int id) {

            var project = _context.Projects.FirstOrDefault(p => p.Id == id);

            if (project == null) 
                return NotFound();

            if(project != null)
            {
                _context.Projects.Remove(project);
            }
        
         await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Supervisor");
        
        }


    }
}
