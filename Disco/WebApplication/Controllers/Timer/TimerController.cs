using Microsoft.AspNetCore.Mvc;
using WebApplication.Models.Timer;

namespace WebApplication.Controllers.Timer
{
    public partial class TimerController : Controller
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
    }
}
