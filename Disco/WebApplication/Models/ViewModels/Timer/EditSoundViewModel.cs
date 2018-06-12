using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace WebApplication.Models.ViewModels.Timer
{
    public class EditSoundViewModel
    {
        public int ID { get; set; }
        public IFormFile File { get; set; }
        [Required(ErrorMessage = "Please enter a sound name")]
        public string Name { get; set; }
    }
}
