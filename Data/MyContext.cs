using Microsoft.EntityFrameworkCore;
using ScrumStandUpTracker_1.Models;

namespace ScrumStandUpTracker_1.Data
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<StatusForm> StatusForms { get; set; }
    }
}
