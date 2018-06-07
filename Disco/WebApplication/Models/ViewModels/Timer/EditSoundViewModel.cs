using Microsoft.AspNetCore.Http;

namespace WebApplication.Models.ViewModels.Timer
{
    public class EditSoundViewModel
    {
        public int ID { get; set; }
        public IFormFile File { get; set; }
        public string Name { get; set; }
    }
}
