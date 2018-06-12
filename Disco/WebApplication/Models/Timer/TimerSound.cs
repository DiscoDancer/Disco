using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.Timer
{
    public class TimerSound
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Please enter a sound name")]
        public string Name { get; set; }
        public byte[] Data { get; set; }
    }
}
