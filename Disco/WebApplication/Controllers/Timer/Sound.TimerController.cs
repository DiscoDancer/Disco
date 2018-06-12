using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models.Timer;
using WebApplication.Models.ViewModels.Timer;

namespace WebApplication.Controllers.Timer
{
    public partial class TimerController : Controller
    {
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
    }
}
