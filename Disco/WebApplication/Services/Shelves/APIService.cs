using RestSharp;
using WebApplication.Models.Shelves;

namespace WebApplication.Services.Shelves
{
    internal static class APIService
    {
        private const string Key = "Mo878oUEd5nAWZaMqqcbg";
        private const string Id = "80865068";

        internal static string GetCurrentShelfContent()
        => GetShelfContent(Shelf.CurrentGoodReadsName);

        internal static string GetReadShelfContent()
        => GetShelfContent(Shelf.ReadGoodReadsName);

        private static string GetShelfContent(string shelf)
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
