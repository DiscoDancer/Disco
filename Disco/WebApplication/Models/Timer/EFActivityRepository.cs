using System.Linq;

namespace WebApplication.Models.Timer
{
    public class EFActivityRepository: IActivityRepository
    {
        private readonly ApplicationDbContext _context;

        public EFActivityRepository(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        public IQueryable<TimerActivity> TimerActivities => _context.TimerActivities;
    }
}
