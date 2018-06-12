using Microsoft.EntityFrameworkCore;
using WebApplication.Models.Timer;

namespace WebApplication.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TimerActivity> TimerActivities { get; set; }
        public DbSet<TimerSound> TimerSounds { get; set; }
        public DbSet<TimerLog> TimerLogs { get; set; }
    }
}
