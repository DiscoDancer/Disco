using System.Linq;
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


        #region Timer Activities

        [HttpGet]
        public ViewResult EditActivities()
        {
            var activities = _activityRepository.TimerActivities;

            return View(activities);
        }

        [HttpGet]
        public ViewResult CreateActivity() => View("EditActivity", new TimerActivity());


        [HttpGet]
        public ViewResult EditActivity(int activityId)
        {
            return View(_activityRepository.TimerActivities
                .FirstOrDefault(x => x.ID == activityId));
        }

        [HttpPost]
        public IActionResult EditActivity(TimerActivity activity)
        {
            if (!ModelState.IsValid) return View(activity);

            _activityRepository.Save(activity);
            return RedirectToAction("EditActivities");
        }

        public IActionResult DeleteActivity(int activityId)
        {
            var deletedActivity = _activityRepository.Delete(activityId);

            return RedirectToAction("EditActivities");
        }

        #endregion
    }
}
