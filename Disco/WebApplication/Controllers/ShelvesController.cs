﻿using Microsoft.AspNetCore.Mvc;
using WebApplication.Models.ViewModels.Shelves;
using WebApplication.Services.Shelves;

namespace WebApplication.Controllers
{
    public class ShelvesController : Controller
    {
        public ViewResult Index()
        {
            var shelves = new[]
            {
                new ShelfViewModel("Recently Read", ShelvesService.ToReadShelf),
                new ShelfViewModel("Currently Reading", ShelvesService.CurrentShelf),
                new ShelfViewModel("Want to Read", ShelvesService.WantToReadShelf),
            };

            return View(shelves);
        }
    }
}
