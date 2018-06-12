using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Models.Timer
{
    public class EFLogRepository : IRepository<TimerLog>
    {
        private readonly ApplicationDbContext _context;

        public EFLogRepository(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        public IQueryable<TimerLog> GetAll() =>
            _context.TimerLogs
                .Include(x => x.Activity);

        public void Save(TimerLog instance)
        {
            if (instance.ID == 0)
            {
                _context.TimerLogs.Add(instance);
            }
            else
            {
                var dbEntry = _context.TimerLogs
                    .FirstOrDefault(x => x.ID == instance.ID);

                if (dbEntry != null)
                {
                    dbEntry.Date = instance.Date;
                }
            }

            _context.SaveChanges();
        }

        public TimerLog Delete(int id)
        {
            var dbEntry = _context.TimerLogs
                .FirstOrDefault(x => x.ID == id);

            if (dbEntry == null) return null;

            _context.TimerLogs.Remove(dbEntry);
            _context.SaveChanges();

            return dbEntry;
        }
    }
}