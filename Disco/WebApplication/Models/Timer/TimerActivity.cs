using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.Timer
{
    public class TimerActivity
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter an activity name")]
        public string Name { get; set; }
    }
}
