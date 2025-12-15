using StudentProjectPortal.Models;

namespace StudentProjectPortal.Repositories
{
    public interface ISubmissionRepository
    {

        Task<List<Submission>> GetSubmissionsForStudentAsync(string userId);
        Task<List<Submission>> GetSubmissionForProjectAsync(int projectId);

        Task UpdateGradeAsync(int submission, string grade);
    }
}