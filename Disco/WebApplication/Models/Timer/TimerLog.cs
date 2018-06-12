using System;

namespace WebApplication.Models.Timer
{
    public class TimerLog
    {
        public int ID { get; set; }
        public TimerActivity Activity { get; set; }
        public DateTime Date { get; set; }
    }
}
