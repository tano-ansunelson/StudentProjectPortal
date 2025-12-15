using Microsoft.EntityFrameworkCore;
using StudentProjectPortal.Data;
using StudentProjectPortal.Models;

namespace StudentProjectPortal.Repositories
{
    public class SubmissionRepository : ISubmissionRepository
    {

        private readonly ApplicationDbContext _context;
        public SubmissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Submission>> GetSubmissionsForStudentAsync(string userId)
        {
            return await _context.Submissions
                .Include(s => s.Project)
                   .ThenInclude(p => p.Supervisor)
                .Include(s => s.Student)
                .Where(s => s.Student.ApplicationUserId==userId)
                .OrderByDescending(s => s.SubmissionDate)
                .ToListAsync();
        }


        public async Task<List<Submission>> GetSubmissionForProjectAsync(int projectId) {
        
        
              return await _context.Submissions
                    .Include(s=>s.Student)
                       .ThenInclude(st=>st.Class)
                    .Include(s=>s.Project)
                       .ThenInclude(p=>p.Supervisor)
                    .Where(s=>s.ProjectId==projectId)
                    .OrderByDescending (s=>s.SubmissionDate)
                    .ToListAsync ();  
        }

       public async  Task UpdateGradeAsync(int submissionId, string grade){
          var submission = await _context.Submissions.FindAsync(submissionId);

            if (submission == null)
                throw new Exception("Submission not found");

        
        submission.Grade = grade;
         await _context.SaveChangesAsync(); 
        }  








    }
}
