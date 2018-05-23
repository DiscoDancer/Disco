using RestSharp;
using WebApplication.Models.Resume;

namespace WebApplication.Services.Resume
{
    internal static class APIService
    {
        private const string Key = "Mo878oUEd5nAWZaMqqcbg";
        private const string Id = "80865068";

        public static string SendRequestForCurrentShelf()
        => SendRequestForShelf(CurrentShelf.GoodReadsName);

        public static string SendRequestForReadShelf()
        => SendRequestForShelf(ReadShelf.GoodReadsName);

        private static string SendRequestForShelf(string shelf)
        {
            var client = new RestClient("https://www.goodreads.com");
            var request = new RestRequest("/review/list?v=2 ", Method.GET);
            request.AddParameter("key", Key);
            request.AddParameter("id", Id);
            request.AddParameter("shelf", shelf);

            var response = client.Execute(request);
            var content = response.Content;

            return content;
        }
    }
}
