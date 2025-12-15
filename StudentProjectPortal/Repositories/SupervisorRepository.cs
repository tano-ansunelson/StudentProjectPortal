using Microsoft.EntityFrameworkCore;
using StudentProjectPortal.Data;
using StudentProjectPortal.Models;

namespace StudentProjectPortal.Repositories
{
    public class SupervisorRepository: ISupervisorRepository
    {

        private readonly ApplicationDbContext _context;

        public SupervisorRepository(ApplicationDbContext context)
        {
            _context = context;
        }   


        public async Task<Supervisor?> GetByUserIdAsync(string userId)
        {
            return await _context.Supervisors
                .Include(s => s.Department)
                .FirstOrDefaultAsync(s => s.ApplicationUserId == userId);
        }


        public async Task UpdateAsync(Supervisor supervisor)
        {
            _context.Supervisors.Update(supervisor);
            await _context.SaveChangesAsync();
        }



    }
}
