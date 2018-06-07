using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models.Timer;
using WebApplication.Models.ViewModels.Timer;

namespace WebApplication.Controllers
{
    public class TimerController : Controller
    {
        private readonly IRepository<TimerActivity> _activityRepository;
        private readonly IRepository<TimerSound> _soundsRepository;

        public TimerController(IRepository<TimerActivity> activityRepository,
            IRepository<TimerSound> soundsRepository)
        {
            _activityRepository = activityRepository;
            _soundsRepository = soundsRepository;
        }

        public ViewResult Index()
        {
            var activities = _activityRepository.GetAll();

            return View(activities);
        }

        public ViewResult Settings() => View();

        public ViewResult History() => View();

        #region Timer Sounds

        [HttpGet]
        public ViewResult EditSounds()
        {
            var sounds = _soundsRepository.GetAll();

            return View(sounds);
        }

        [HttpGet]
        public ViewResult EditSound(int soundId)
        {
            var model = new CaptionSound
            {
                Caption = "Edit Sound",
                Sound = _soundsRepository.GetAll()
                    .FirstOrDefault(x => x.ID == soundId)
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditSound(TimerSound sound)
        {
            return RedirectToAction("EditSounds");
        }

        [HttpGet]
        public ViewResult CreateSound()
        {
            return View("EditSound");
        }

        public IActionResult DeleteSound(int soundId)
        {
            return RedirectToAction("EditSounds");
        }

        #endregion


        #region Timer Activities

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

        #endregion
    }
}
