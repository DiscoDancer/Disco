using System.Collections.Generic;

namespace WebApplication.Models.Shelves
{
    internal class Shelf
    {
        internal const string CurrentGoodReadsName = "currently-reading";
        internal const string ReadGoodReadsName = "read";

        internal List<Book> Books { get; set; }
    }
}
