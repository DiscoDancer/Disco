using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Components
{
    public class HeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}