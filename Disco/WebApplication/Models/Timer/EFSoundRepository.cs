using System.Linq;

namespace WebApplication.Models.Timer
{
    public class EFSoundRepository : IRepository<TimerSound>
    {
        private readonly ApplicationDbContext _context;

        public EFSoundRepository(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        public IQueryable<TimerSound> GetAll()
        {
            return _context.TimerSounds;
        }

        public void Save(TimerSound instance)
        {
            if (instance.ID == 0)
            {
                _context.TimerSounds.Add(instance);
            }
            else
            {
                var dbEntry = _context.TimerSounds
                    .FirstOrDefault(x => x.ID == instance.ID);

                if (dbEntry != null)
                {
                    dbEntry.Name = instance.Name;
                    dbEntry.Data = instance.Data;
                }
            }

            _context.SaveChanges();
        }

        public TimerSound Delete(int id)
        {
            var dbEntry = _context.TimerSounds
                .FirstOrDefault(x => x.ID == id);

            if (dbEntry == null) return null;

            _context.TimerSounds.Remove(dbEntry);
            _context.SaveChanges();

            return dbEntry;
        }
    }
}
