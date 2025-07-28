using ScrumStandUpTracker_1.DTOs;
using ScrumStandUpTracker_1.Repositories;

namespace ScrumStandUpTracker_1.Service
{
    public class StatusFormService
    {
        private readonly IStatusRepository _repository;
        public StatusFormService(IStatusRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<StatusFormDTO>> GetAllStatus()
        {
            return await _repository.GetAllStatus();
        }
        public async Task<IEnumerable<StatusFormDTO>> GetStatusByDate(DateTime date)
        {
            return await _repository.GetStatusByDate(date);
        }
        public async Task<IEnumerable<StatusFormDTO>> GetStatusByDeveloperName(string name)
        {
            return await _repository.GetStatusByDeveloperName(name);
        }
        public async Task AddStatus(StatusFormDTO statusdto)
        {
            await _repository.AddStatus(statusdto);
        }
        public async Task UpdateStatus(StatusFormDTO statusdto)
        {
            await _repository.UpdateStatus(statusdto);
        }
        public async Task DeleteStatus(int id)
        {
            await _repository.DeleteStatus(id);
        }
    }
}
