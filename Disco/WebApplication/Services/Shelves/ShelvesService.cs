using WebApplication.Models.Shelves;

namespace WebApplication.Services.Shelves
{
    internal static class ShelvesService
    {
        private const string Key = "Mo878oUEd5nAWZaMqqcbg";

        public static Shelf CurrentShelf
        {
            get
            {
                var xmlContent = APIService.GetCurrentShelfContent();
                var xmlDoc = XmlService.CreateXmlDocument(xmlContent);

                return new Shelf
                {
                    Books = XmlService.ParseBooks(xmlDoc)
                };
            }
        }

        public static Shelf ReadShelf
        {
            get
            {
                var xmlContent = APIService.GetReadShelfContent();
                var xmlDoc = XmlService.CreateXmlDocument(xmlContent);

                return new Shelf
                {
                    Books = XmlService.ParseBooks(xmlDoc)
                };
            }
        }
    }
}
