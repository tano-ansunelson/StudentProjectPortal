using StudentProjectPortal.Models;

namespace StudentProjectPortal.Repositories
{
    public interface ISupervisorRepository
    {
        Task<Supervisor?> GetByUserIdAsync(string userId);
        Task UpdateAsync(Supervisor supervisor);


    }
}