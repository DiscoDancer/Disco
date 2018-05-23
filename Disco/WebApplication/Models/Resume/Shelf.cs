using System;
using System.Collections.Generic;

namespace WebApplication.Models.Resume
{
    [Serializable]
    public abstract class Shelf
    {
        public abstract string Name { get; }
        public List<Book> Books { get; set; }
    }
}
