using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScrumStandUpTracker_1.Data;
using ScrumStandUpTracker_1.DTOs;
using ScrumStandUpTracker_1.Models;

namespace ScrumStandUpTracker_1.Repositories
{
    public class StatusFormRepository : IStatusRepository
    {
        private readonly MyContext _myContext;
        private readonly IMapper _mapper;
        public StatusFormRepository(MyContext context, IMapper mapper)
        {
            _myContext = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<StatusFormDTO>> GetAllStatus()
        {
            var statuses = await _myContext.StatusForms.Include(s => s.developer).ToListAsync();
            return statuses.Select(s => _mapper.Map<StatusFormDTO>(s));
        }
        public async Task<IEnumerable<StatusFormDTO>> GetStatusByDate(DateTime date)
        {
            var startDate = date.Date;
            var endDate = startDate.AddDays(1);
            var statuses = await _myContext.StatusForms.Include(s => s.developer)
                .Where(s => s.SubmissionDate>=startDate && s.SubmissionDate<=endDate).ToListAsync(); 
            return statuses.Select(s => _mapper.Map<StatusFormDTO>(s));
        }
        public async Task<IEnumerable<StatusFormDTO>> GetStatusByDeveloperName(string name)
        {
            var statuses = await _myContext.StatusForms.Include(s => s.developer)
                .Where(s => s.DeveloperName.ToLower()==name.ToLower()).ToListAsync();
            return statuses.Select(s => _mapper.Map<StatusFormDTO>(s));
        }
        public async Task AddStatus(StatusFormDTO statusdto)
        {
            var status = _mapper.Map<StatusForm>(statusdto);
            if (status.SubmissionDate == default)
            {
                status.SubmissionDate = DateTime.UtcNow;
            }
            _myContext.StatusForms.Add(status);
            await _myContext.SaveChangesAsync();
        }
        public async Task UpdateStatus(StatusFormDTO statusdto)
        {
            var status = await _myContext.StatusForms.FindAsync(statusdto.StatusFormId);
            if (status == null)
            {
                throw new KeyNotFoundException($"StatusForm with {statusdto.StatusFormId} is not found");
            }
            status.DeveloperName = statusdto.DeveloperName;
            status.TaskDetails = statusdto.TaskDetails;
            status.YesterdayTask = statusdto.YesterdayTask;
            status.TodayTask = statusdto.TodayTask;
            status.Blockers = statusdto.Blockers;
            status.SubmissionDate = statusdto.SubmissionDate;
            status.DeveloperId = statusdto.DeveloperId;
            await _myContext.SaveChangesAsync();
        }
        public async Task DeleteStatus(int id)
        {
            var status = await _myContext.StatusForms.FindAsync(id);
            if (status == null)
            {
                throw new KeyNotFoundException($"status form with {id} not found");
            }
            _myContext.StatusForms.Remove(status);
            await _myContext.SaveChangesAsync();
        }
    }
}
