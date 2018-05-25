using Microsoft.AspNetCore.Mvc;
using WebApplication.Models.Timer;

namespace WebApplication.Controllers
{
    public class TimerController : Controller
    {
        private readonly IActivityRepository _activityRepository;

        public TimerController(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public ViewResult Index()
        {
            var activities = _activityRepository.TimerActivities;

            return View(activities);
        }

        public ViewResult Settings() => View();

        public ViewResult History() => View();

        public ViewResult EditActivities() => View();

        public ViewResult EditActivity() => View();
    }
}
