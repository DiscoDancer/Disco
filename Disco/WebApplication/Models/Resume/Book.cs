using System;

namespace WebApplication.Models.Resume
{
    [Serializable]
    public class Book
    {
        public string Name { get; set; }
        public string CoverUrl { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
    }
}
