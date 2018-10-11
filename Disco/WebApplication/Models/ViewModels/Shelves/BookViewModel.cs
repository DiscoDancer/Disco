using WebApplication.Models.Shelves;

namespace WebApplication.Models.ViewModels.Shelves
{
    public class BookViewModel
    {
        public BookViewModel() { }

        internal BookViewModel(Book book)
        {
            Name = book.Name;
            CoverUrl = book.CoverUrl;
            Author = book.Author;
            Year = book.Year;
        }

        public string Name { get; set; }
        public string CoverUrl { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
    }
}
