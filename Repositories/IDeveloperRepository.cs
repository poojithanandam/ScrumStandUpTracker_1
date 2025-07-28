using ScrumStandUpTracker_1.Models;

namespace ScrumStandUpTracker_1.Repositories
{
    public interface IDeveloperRepository
    {
        Task<Developer> GetDeveloper(string username);
        Task AddDeveloper(Developer developer);
    }
}
