using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using WebApplication.Models.Resume;

namespace WebApplication.Services.Resume
{
    internal static class XmlService
    {
        public static XmlDocument CreateXmlDocument(string content)
        {
            File.WriteAllText("tmp.txt", content);

            var xmlDoc = new XmlDocument();
            xmlDoc.Load("tmp.txt");

            return xmlDoc;
        }

        public static List<Book> ParseBooks(XmlDocument document)
        {
            var list = new List<Book>();

            var reviews = document.GetElementsByTagName("review");

            for (int i = 0; i < reviews.Count; i++)
            {
                list.Add(GetBookFromReviewNode(reviews[i]));
            }

            return list;
        }

        private static Book GetBookFromReviewNode(XmlNode node)
        {
            var model = new Book
            {
                Name = node.SelectSingleNode("book/title").InnerText,
                CoverUrl = node.SelectSingleNode("book/image_url").InnerText
            };

            int.TryParse(node.SelectSingleNode("book/publication_year").InnerText, out var year);
            model.Year = year;

            model.Author = GetAuthorsFromReviewNode(node);

            return model;
        }

        private static string GetAuthorsFromReviewNode(XmlNode node)
        {
            var authors = node.SelectNodes("book/authors/author");
            var authors_list = new List<string>();

            for (var j = 0; j < authors.Count; j++)
            {
                var authorStr = authors[j].SelectSingleNode("name").InnerText;
                if (!string.IsNullOrWhiteSpace(authorStr))
                {
                    authors_list.Add(authorStr);
                }
            }

            return authors_list.Aggregate((x, y) => $"{x};{y}");
        }
    }
}
