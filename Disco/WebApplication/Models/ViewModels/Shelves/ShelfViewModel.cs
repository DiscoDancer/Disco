using System.Collections.Generic;
using System.Linq;
using WebApplication.Models.Shelves;

namespace WebApplication.Models.ViewModels.Shelves
{
    public class ShelfViewModel
    {
        public ShelfViewModel() { }

        internal ShelfViewModel(string name, Shelf shelf)
        {
            Books = shelf.Books
                .Select(x => new BookViewModel(x))
                .ToList();

            Name = name;
        }

        public string Name { get; set; }

        public List<BookViewModel> Books { get; set; }
    }
}
