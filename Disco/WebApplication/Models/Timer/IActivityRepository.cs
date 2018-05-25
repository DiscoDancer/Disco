using System.Linq;

namespace WebApplication.Models.Timer
{
    public interface IActivityRepository
    {
        IQueryable<TimerActivity> TimerActivities { get; }

        void Save(TimerActivity activity);

        TimerActivity Delete(int activityId);
    }
}
