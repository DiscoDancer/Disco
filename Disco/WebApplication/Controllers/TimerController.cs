using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    public class TimerController : Controller
    {
        public ViewResult Index() => View();

        public ViewResult Settings() => View();

        public ViewResult History() => View();

        public ViewResult BrowseActivities() => View();

        public ViewResult EditActivity() => View();
    }
}
