using WebApplication.Models.Shelves;

namespace WebApplication.Services.Shelves
{
    internal static class ShelvesService
    {
        public static Shelf WantToReadShelf =>
            CreateShelf(APIService.WantShelfContent);

        public static Shelf CurrentShelf =>
            CreateShelf(APIService.CurrentShelfContent);

        public static Shelf ToReadShelf =>
            CreateShelf(APIService.ToReadShelfContent);

        private static Shelf CreateShelf(string xmlContent) =>
            new Shelf
            {
                Books = XmlService.ParseBooks(XmlService.CreateXmlDocument(xmlContent))
            };
    }
}
