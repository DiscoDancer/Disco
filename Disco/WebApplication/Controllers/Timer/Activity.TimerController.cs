using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models.Timer;
using WebApplication.Models.ViewModels.Timer;

namespace WebApplication.Controllers.Timer
{
    public partial class TimerController : Controller
    {
        [HttpGet]
        public ViewResult EditActivities()
        {
            var activities = _activityRepository.GetAll();

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
                Activity = _activityRepository.GetAll()
                    .FirstOrDefault(x => x.ID == activityId)
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditActivity(TimerActivity activity)
        {
            var model = new CaptionActivity
            {
                Caption = "Edit Activity",
                Activity = activity
            };

            if (!ModelState.IsValid) return View(model);

            _activityRepository.Save(activity);

            if (TempData != null)
            {
                TempData["message"] = $"Activity {activity.Name} has been saved";
            }

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
    }
}
