using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Task1.BookListService.Models;
using Task1.BookListService.Services;
using static System.Console;

namespace Task1.BookListService.ConsoleTests {
    class Program {
        static void Main(string[] args) {
            LoggingService.Instance.Log("\n\nProgram version " + Assembly.GetEntryAssembly().GetName().Version + " started. OS: " + Environment.OSVersion);
            try {
                TestBookListService();
            } catch(Exception ex) {
                LoggingService.Instance.Log(ex, "Error when working with book service");
            }

        }
        
        static void TestBookListService() {
            Services.BookListService service = new Services.BookListService();

            if(service.IsEmpty) {
                LoggingService.Instance.Log("Service is empty. Try to load from repositoty");
                service.Load(new BookRepository("book"));
                if(service.IsEmpty) {
                    LoggingService.Instance.Log("Service is empty. Try to add some books");
                    service.AddBooks(new List<Book> {
                        new Book("J. K. Rowling", "Harry Potter and the Prisoner of Azkaban", "Махаон", 528, 184100),
                        new Book("J. K. Rowling", "Harry Potter and the Philosopher's Stone", "МАХАОН", 432, 172100),
                        new Book("Stephen King", "The Green Mile", "ACT", 384, 106100),
                        new Book("Margaret Mitchell", "Gone with the Wind", "Эксмо", 1280, 173100),
                        new Book("Arthur Conan Doyle", "A Study in Scarlet", "ACT", 304, 123700),
                        new Book("George Martin", "A Storm of Swords", "ACT", 960, 127300),
                        new Book("Борис Васильев", "А зори здесь тихие...", "Мартин", 432, 102200),
                        new Book("Alexandre Dumas", "Le comte de Monte Cristo", "ACT", 1216, 166700),
                        new Book("Harper Lee", "To Kill a Mockingbird", "ACT", 416, 90100)
                    });
                }
                LoggingService.Instance.Log("Store books to repository 'b'");
                service.Store(new BookRepository("b"));
                
            }
            List<Book> find = service.Find(book => book.PublishOrganization.ToLowerInvariant() == "махаон");
            LoggingService.Instance.Log("\tFind by publish organisation Махаон\n");

            LoggingService.Instance.Log(string.Join("\n", find.Select(book => book.ToString()).ToArray()));

            List<Book> sort = service.Sort(book => book.Price);
            LoggingService.Instance.Log("\n\n\tSort by price\n");
            LoggingService.Instance.Log(string.Join("\n", sort.Select(book => book.ToString()).ToArray()));

            sort = service.Sort(book => book.PagesNumber);
            LoggingService.Instance.Log("\n\n\tSort by number of pages\n");
            LoggingService.Instance.Log(string.Join("\n", sort.Select(book => book.ToString()).ToArray()));

            LoggingService.Instance.Log("\n\n\ttest exceptions\n");
            try {
                service.AddBook(new Book("Margaret Mitchell", "Gone with the Wind", "Эксмо", 1280, 173100));
            } catch (BookListServiceException ex) {
                LoggingService.Instance.Log(ex, "Exception when adding book");
            }
            try {
                service.RemoveBook(new Book("Margaret Mitchell", "Gone with the Wind", "ACT", 1280, 173100));
            } catch (BookListServiceException ex) {
                LoggingService.Instance.Log(ex, "Exception when removing book");
            }

            LoggingService.Instance.Log("\n\n\tTest remove: 'Gone with the Wind' and '...the Prisoner of Azkaban'");
            service.RemoveBook(new Book("Margaret Mitchell", "Gone with the Wind", "Эксмо", 1280, 173100));
            service.RemoveBook(new Book("J. K. Rowling", "Harry Potter and the Prisoner of Azkaban", "Махаон", 528, 184100));

            LoggingService.Instance.Log(service.ToString());

            LoggingService.Instance.Log("\n\n\tTest add book: 'Gone with the Wind' and '...the Prisoner of Azkaban' and 'Godfather'");
            service.AddBooks(new List<Book> {
                new Book("Margaret Mitchell", "Gone with the Wind", "Эксмо", 1280, 173100),
                new Book("J. K. Rowling", "Harry Potter and the Prisoner of Azkaban", "Махаон", 528, 184100),
                new Book("Mario Puzo", "Godfather", "Айрис-пресс", 288, 256300)
            });

            LoggingService.Instance.Log(service.ToString());

            service.RemoveBook(new Book("Mario Puzo", "Godfather", "Айрис-пресс", 288, 256300));
            LoggingService.Instance.Log("Store books to repository 'book'");
            service.Store(new BookRepository("book"));
            ReadLine();
        }
    }
}
