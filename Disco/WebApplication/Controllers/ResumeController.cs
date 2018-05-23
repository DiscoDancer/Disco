﻿using Microsoft.AspNetCore.Mvc;
using WebApplication.Services.Resume;

namespace WebApplication.Controllers
{
    public class ResumeController: Controller
    {
        public ViewResult Index()
        {
            var shelves = new[]
            {
                ShelvesService.ReadBooks,
                ShelvesService.CurrentShelf
            };

            return View(shelves);
        }
    }
}
