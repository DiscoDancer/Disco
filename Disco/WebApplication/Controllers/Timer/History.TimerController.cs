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
            throw new NotImplementedException();
        }

        public IActionResult AddLog(int activityId)
        {
            throw new NotImplementedException();
        }
    }
}
