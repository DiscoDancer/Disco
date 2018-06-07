using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
            ViewBag.SubHeader = "Edit Sound";

            return View(new EditSoundViewModel());
        }

        public IActionResult GetSoundById(int id)
        {
            var sound = _soundsRepository.GetAll().FirstOrDefault(x => x.ID == id);

            if (sound?.Data == null || sound.Data.Length == 0)
            {
                return NotFound();
            }

            var memory = new MemoryStream(sound.Data)
            {
                Position = 0
            };

            var response = File(memory, "application/octet-stream", "file.mp3"); // FileStreamResult
            return response;
        }

        [HttpPost]
        public async Task<IActionResult> EditSound(EditSoundViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.SubHeader = "Edit Sound";

                return View(model);
            }

            var sound = new TimerSound
            {
                Name = model.Name,
                ID = model.ID
            };

            using (var memoryStream = new MemoryStream())
            {
                await model.File.CopyToAsync(memoryStream);
                sound.Data = memoryStream.ToArray();
            }

            _soundsRepository.Save(sound);

            if (TempData != null)
            {
                TempData["message"] = $"Sound {sound.Name} has been uploaded to server!";
            }

            return RedirectToAction("EditSounds");
        }

        [HttpGet]
        public ViewResult CreateSound()
        {
            ViewBag.SubHeader = "Upload New Sound";

            return View("EditSound", new EditSoundViewModel());
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
