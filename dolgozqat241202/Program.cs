using dolgozqat241202;
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static Random random = new Random();
    static List<Book> books = new List<Book>();
    static int totalRevenue = 0;
    static int outOfStockCount = 0;
    static int initialStock;

    static void Main(string[] args)
    {
        InitializeBooks();
        initialStock = books.Sum(b => b.Stock);

        for (int i = 0; i < 100; i++)
        {
            SimulatePurchase();
        }

        Console.WriteLine($"Teljes bevétel: {totalRevenue} Ft");
        Console.WriteLine($"Kifogyott könyvek száma a nagykerből: {outOfStockCount}");
        Console.WriteLine($"Kezdeti készlet: {initialStock} db");
        Console.WriteLine($"Jelenlegi készlet: {books.Sum(b => b.Stock)} db");
        Console.WriteLine($"Készletváltozás: {books.Sum(b => b.Stock) - initialStock} db");
    }

    static void InitializeBooks()
    {
        for (int i = 0; i < 15; i++)
        {
            string language = RandomLanguage();
            string title = GenerateTitle(language);
            List<Author> authors = GenerateAuthors();
            long isbn;
            do
            {
                isbn = new Random().Next(1000000000, 10000000000);
            } while (books.Any(b => b.ISBN == isbn));

            int year = random.Next(2007, DateTime.Now.Year + 1);
            int stock = random.Next(100) < 30 ? 0 : random.Next(5, 11);
            int price = random.Next(10, 101) * 100;

            Book book = new Book(isbn, authors, title, year, language, stock, price);
            books.Add(book);
        }
    }

    static void SimulatePurchase()
    {
        int bookIndex = random.Next(books.Count);
        Book selectedBook = books[bookIndex];

        if (selectedBook.Stock > 0)
        {
            selectedBook.Stock--;
            totalRevenue += selectedBook.Price;
        }
        else
        {
            if (random.Next(100) < 50)
            {
                selectedBook.Stock += random.Next(1, 11);
            }
            else
            {
                outOfStockCount++;
                books.RemoveAt(bookIndex);
            }
        }
    }

    static string RandomLanguage()
    {
        return random.Next(100) < 80 ? "magyar" : "angol";
    }

    static string GenerateTitle(string language)
    {
        string[] titles;
        if (language == "magyar")
        {
            titles = new[] { "A titok", "Az elfeledett város", "A kaland kezdete", "Sztárok az égen", "Az utolsó esély" };
        }
        else
        {
            titles = new[] { "The Secret", "The Forgotten City", "The Beginning of the Adventure", "Stars in the Sky", "The Last Chance" };
        }
        return titles[random.Next(titles.Length)];
    }

    static List<Author> GenerateAuthors()
    {
        int authorCount = random.Next(100) < 70 ? 1 : random.Next(100) < 50 ? 2 : 3;
        List<Author> authors = new List<Author>();
        for (int j = 0; j < authorCount; j++)
        {
            authors.Add(new Author(GenerateRandomName()));
        }
        return authors;
    }

    static string GenerateRandomName()
    {
        string[] firstNames = { "John Smith", "Jane Doe", "Alice Johnson", "Charlie Brown", "Diana Ross" };
        return firstNames[random.Next(firstNames.Length)];
    }
}
