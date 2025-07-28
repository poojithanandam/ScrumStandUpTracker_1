using Microsoft.EntityFrameworkCore;
using ScrumStandUpTracker_1.Data;
using ScrumStandUpTracker_1.Models;

namespace ScrumStandUpTracker_1.Repositories
{
    public class DeveloperRepository : IDeveloperRepository
    {
        private readonly MyContext _myContext;
        public DeveloperRepository(MyContext myContext)
        {
            _myContext = myContext;
        }
        public async Task<Developer> GetDeveloper(string username)
        {
            return await _myContext.Developers.FirstOrDefaultAsync(d => d.Username == username);
        }
        public async Task AddDeveloper(Developer developer)
        {
            _myContext.Developers.Add(developer);
            await _myContext.SaveChangesAsync();
        }
    }
}
