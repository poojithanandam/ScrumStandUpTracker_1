using ScrumStandUpTracker_1.DTOs;

namespace ScrumStandUpTracker_1.Repositories
{
    public interface IStatusRepository
    {
        Task<IEnumerable<StatusFormDTO>> GetAllStatus();
        Task<IEnumerable<StatusFormDTO>> GetStatusByDate(DateTime date);
        Task<IEnumerable<StatusFormDTO>> GetStatusByDeveloperName(string name);
        Task AddStatus(StatusFormDTO statusdto);
        Task UpdateStatus(StatusFormDTO statusdto);
        Task DeleteStatus(int id);
    }
}
 