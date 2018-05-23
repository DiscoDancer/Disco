using WebApplication.Models.Resume;

namespace WebApplication.Services.Resume
{
    internal static class ShelvesService
    {
        private const string Key = "Mo878oUEd5nAWZaMqqcbg";

        public static Shelf CurrentShelf
        {
            get
            {
                var xmlContent = APIService.SendRequestForCurrentShelf();
                var xmlDoc = XmlService.CreateXmlDocumentFromString(xmlContent);

                return new CurrentShelf
                {
                    Books = XmlService.ParseBooksFromXmlDocument(xmlDoc)
                };
            }
        }

        public static Shelf ReadBooks 
        {
            get
            {
                var xmlContent = APIService.SendRequestForReadShelf();
                var xmlDoc = XmlService.CreateXmlDocumentFromString(xmlContent);

                return new ReadShelf
                {
                    Books = XmlService.ParseBooksFromXmlDocument(xmlDoc)
                };
            }
        }
    }
}
