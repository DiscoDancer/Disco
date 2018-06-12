using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models.Timer;

namespace WebApplication.Controllers.Timer
{
    public partial class TimerController : Controller
    {
        public ViewResult History()
            => View(_logsRepository.GetAll());

        public IActionResult DeleteLog(int id)
        {
            var deletedLog = _logsRepository.Delete(id);

            if (deletedLog != null)
            {
                TempData["message"] = $"Activity #{id} was deleted";
            }

            return RedirectToAction("History");
        }

        #region API

        [HttpPost]
        public IActionResult AddLog(int activityId)
        {
            var activity = _activityRepository
                .GetAll()
                .FirstOrDefault(x => x.ID == activityId);

            if (activity == null) return NotFound();

            _logsRepository.Save(new TimerLog
            {
                Activity = activity,
                Date = DateTime.Now
            });

            return Ok();
        }

        #endregion

    }
}
