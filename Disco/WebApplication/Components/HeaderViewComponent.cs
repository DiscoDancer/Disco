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
                Links = new[]
                {
                    new HeaderLink
                    {
                        Text = "Google",
                        Url = "https://www.google.com/"
                    },
                    new HeaderLink
                    {
                        Text = "Yandex",
                        Url = "https://yandex.ru/"
                    }
                }
            };
            
            return View(model);
        }
    }
}