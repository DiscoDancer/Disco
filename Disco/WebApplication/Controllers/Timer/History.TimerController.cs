using System;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult AddLog(int activityId)
        {
            throw new NotImplementedException();
        }
    }
}
