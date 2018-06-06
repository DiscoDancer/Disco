using Microsoft.AspNetCore.Mvc;

namespace UnitTests.Services
{
    public static class MVCHelper
    {
        public static T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }
    }
}
