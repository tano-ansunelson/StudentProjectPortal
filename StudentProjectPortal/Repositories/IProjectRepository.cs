using StudentProjectPortal.Models;

namespace StudentProjectPortal.Repositories
{
    public interface IProjectRepository
    {
        Task AddAsync (Project project);
        Task<List<Project>> GetBySupervisorAsync(int supervisorId);

        Task UpdateAsync(Project project);
        Task DeleteAsync(int id);
        Task<List<Project>> GetProjectsForStudentAsync(string departmentName, int level);
    }
}