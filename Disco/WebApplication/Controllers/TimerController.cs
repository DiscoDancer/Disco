using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models.Timer;
using WebApplication.Models.ViewModels.Timer;

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
        public ViewResult CreateActivity()
        {
            var model = new CaptionActivity
            {
                Caption = "Create Activity",
                Activity = new TimerActivity()
            };

            return View("EditActivity", model);
        } 


        [HttpGet]
        public ViewResult EditActivity(int activityId)
        {
            var model = new CaptionActivity
            {
                Caption = "Edit Activity",
                Activity = _activityRepository.TimerActivities
                    .FirstOrDefault(x => x.ID == activityId)
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditActivity(TimerActivity activity)
        {
            if (!ModelState.IsValid) return View(activity);

            _activityRepository.Save(activity);
            TempData["message"] = $"Activity {activity.Name} has been saved";
            return RedirectToAction("EditActivities");
        }

        public IActionResult DeleteActivity(int activityId)
        {
            var deletedActivity = _activityRepository.Delete(activityId);

            if (deletedActivity != null)
            {
                TempData["message"] = $"Activity {deletedActivity.Name} was deleted";
            }

            return RedirectToAction("EditActivities");
        }

        #endregion
    }
}
