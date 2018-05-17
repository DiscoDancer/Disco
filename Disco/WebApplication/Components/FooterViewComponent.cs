using System;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models.ViewModels.Footer;

namespace WebApplication.Components
{
    public class FooterViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var model = new FooterViewModel
            {
                CurrentYear = DateTime.Now.Year,
                CompanyName = "Disco"
            };

            return View(model);
        }
    }
}