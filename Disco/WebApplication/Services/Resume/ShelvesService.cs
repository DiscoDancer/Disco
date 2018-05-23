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
                var xmlDoc = XmlService.CreateXmlDocument(xmlContent);

                return new CurrentShelf
                {
                    Books = XmlService.ParseBooks(xmlDoc)
                };
            }
        }

        public static Shelf ReadShelf
        {
            get
            {
                var xmlContent = APIService.SendRequestForReadShelf();
                var xmlDoc = XmlService.CreateXmlDocument(xmlContent);

                return new ReadShelf
                {
                    Books = XmlService.ParseBooks(xmlDoc)
                };
            }
        }
    }
}
