using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dolgozqat241202
{
    internal class Book
    {
        private static Random random = new Random();
        public long ISBN { get; private set; }
        public List<Author> Authors { get; private set; }
        public string Title { get; private set; }
        public int Year { get; private set; }
        public string Language { get; private set; }
        public int Stock { get; set; }
        public int Price { get; private set; }
        public Book(long isbn, List<Author> authors, string title, int year, string language, int stock, int price)
        {
            ISBN = isbn;
            Authors = authors ?? throw new ArgumentNullException(nameof(authors));
            Title = title;
            Year = year;
            Language = ValidateLanguage(language);
            Stock = stock >= 0 ? stock : throw new ArgumentException("Készlet nem lehet negatív.");
            Price = ValidatePrice(price);
        }

        public Book(string title, string singleAuthorName)
        {
            ISBN = GenerateRandomISBN();
            Authors = new List<Author> { new Author(singleAuthorName) };
            Title = title;
            Year = 2024;
            Language = "magyar";
            Stock = 0;
            Price = 4500;
        }

        private int ValidatePrice(int price)
        {
            return price >= 1000 && price <= 10000 && price % 100 == 0 ? price : throw new ArgumentException("Az ár nem megfelelő.");
        }

        private string ValidateLanguage(string language)
        {
            string[] acceptedLanguages = { "angol", "németh", "magyar" };
            if (!acceptedLanguages.Contains(language))
            {
                throw new ArgumentException("Nyelv csak angol, német, vagy magyar lehet.");
            }
            return language;
        }

        private long GenerateRandomISBN()
        {
            return random.Next(1000000000, 10000000000);
        }

        public override string ToString()
        {
            string authorLabel = Authors.Count == 1 ? "szerző:" : "szerzők:";
            string stockStatus = Stock > 0 ? $"{Stock} db" : "beszerzés alatt";
            return $"{Title} ({ISBN}) - {authorLabel} {string.Join(", ", Authors)} - {stockStatus} - {Price} Ft";
        }
    }
}
