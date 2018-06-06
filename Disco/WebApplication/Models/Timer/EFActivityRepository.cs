using System.Linq;

namespace WebApplication.Models.Timer
{
    public class EFActivityRepository: IRepository<TimerActivity>
    {
        private readonly ApplicationDbContext _context;

        public EFActivityRepository(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        public IQueryable<TimerActivity> GetAll () => 
            _context.TimerActivities;

        public void Save(TimerActivity activity)
        {
            if (activity.ID == 0)
            {
                _context.TimerActivities.Add(activity);
            }
            else
            {
                var dbEntry = _context.TimerActivities
                    .FirstOrDefault(x => x.ID == activity.ID);

                if (dbEntry != null)
                {
                    dbEntry.Name = activity.Name;
                }
            }

            _context.SaveChanges();
        }

        public TimerActivity Delete(int activityId)
        {
            var dbEntry = _context.TimerActivities
                .FirstOrDefault(x => x.ID == activityId);

            if (dbEntry == null) return null;

            _context.TimerActivities.Remove(dbEntry);
            _context.SaveChanges();

            return dbEntry;
        }
    }
}
