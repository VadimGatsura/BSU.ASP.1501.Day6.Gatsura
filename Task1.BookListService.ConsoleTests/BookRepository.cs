using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Task1.BookListService.Models;
using Task1.BookListService.Services;

namespace Task1.BookListService.ConsoleTests {
    public class BookRepository : IRepository<Book> {

        private string FilePath { get; }

        public BookRepository(string filePath) {
            FilePath = filePath;
        }

        #region Public Methods
        public List<Book> Load() {
            FileMode mode = !File.Exists(FilePath) ? FileMode.Create : FileMode.Open;
            List<Book> books = new List<Book>();
            using (BinaryReader reader = new BinaryReader(new FileStream(FilePath, mode))) {
                while (reader.PeekChar() > -1) {
                    string author = reader.ReadString();
                    string title = reader.ReadString();
                    string publishOrganization = reader.ReadString();
                    int pagesNumber = reader.ReadInt32();
                    double price = reader.ReadDouble();
                    books.Add(new Book(author, title, publishOrganization, pagesNumber, price));
                }
            }
            return books;
        }

        public void Store(IEnumerable<Book> books) {
            if (books == null)
                throw new ArgumentNullException(nameof(books));

            Create(books, FileMode.Create);
        }
        #endregion

        #region Private Methods
        private void WriteBook(Book item, BinaryWriter writer) {
            writer.Write(item.Author);
            writer.Write(item.Title);
            writer.Write(item.PublishOrganization);
            writer.Write(item.PagesNumber);
            writer.Write(item.Price);
        }

        private void Create(IEnumerable<Book> items, FileMode mode) {
            using (BinaryWriter writer = new BinaryWriter(new FileStream(FilePath, mode, FileAccess.Write))) {
                foreach(var item in items.Where(item => item != null)) {
                    WriteBook(item, writer);
                }
            }
        }
        #endregion 
    }
}
