using Microsoft.EntityFrameworkCore;
using StudentProjectPortal.Data;
using StudentProjectPortal.Models;

namespace StudentProjectPortal.Repositories
{
    public class ProjectRepository: IProjectRepository
    {

        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task AddAsync (Project project)
        {
            _context.Projects.Add (project);    
            await _context.SaveChangesAsync();


        }


        public async Task<List<Project>> GetBySupervisorAsync(int supervisorId)
        {

            return await _context.Projects
                .Include(p => p.Submissions)
                .Where(p => p.SupervisorId == supervisorId) 
                .ToListAsync();

        }


        public async Task UpdateAsync(Project project) { 
        
        
        _context.Projects .Update (project);    
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id) { 
        
        var project = await _context.Projects.FindAsync (id);
            _context.Projects.Remove (project);
            await _context.SaveChangesAsync();

        
        }

        public async Task<List<Project>> GetProjectsForStudentAsync(string departmentName, int level) { 
        return  await _context.Projects
                .Include(p=>p.Supervisor)
                   .ThenInclude(s=>s.Department)
                .Where(p=> p.Supervisor.Department.Name== departmentName && p.Level==level)
              
                .ToListAsync();

        }



    }
}
