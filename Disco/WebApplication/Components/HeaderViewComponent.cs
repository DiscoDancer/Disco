using Microsoft.AspNetCore.Mvc;
using WebApplication.Models.ViewModels.Header;

namespace WebApplication.Components
{
    public class HeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var model = new HeaderViewModel
            {
                CurrentControllerName = RouteData?.Values["controller"] as string ?? string.Empty
            };

            return View(model);
        }
    }
}