﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task1.BookListService.Models;

namespace Task1.BookListService.Services {
    public class BookListServiceException : Exception {
        public BookListServiceException(string message) : base(message) { }
        public BookListServiceException(string message, Exception ex) : base(message, ex) { }
    }

    public sealed class AddBookException : BookListServiceException {
        public AddBookException(string message) : base(message) { }
    }

    public sealed class RemoveBookException : BookListServiceException {
        public RemoveBookException(string message) : base(message) { }
    }


    public sealed class BookListService {
        private IRepository<Book> Repository { get; }

        /// <summary>Books, which store in service</summary>
        private List<Book> Books { get; }

        public bool IsEmpty => Books?.Count == 0;

        public BookListService(IRepository<Book> repository) {
            Repository = repository;
            try {
                Books = Repository.Load();
            } catch(Exception ex) {
                throw new BookListServiceException("Incorrect realization of repository", ex);
            }
            
        }

        #region Public methods

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            foreach (var book in Books) {
                sb.Append($"{book}\n");
            }
            return sb.ToString();
        }

        /// <summary>Add book to service
        /// <remarks>If the book already in service thrown <see cref="AddBookException"/></remarks>
        /// </summary>
        /// <param name="book">Book to add</param>
        public void AddBook(Book book) {
            if (book == null)
                throw new ArgumentNullException(nameof(book));
            if (Books.Contains(book)) {
                throw new AddBookException($"BookListService already contains {book}");
            }
            Books.Add(book);
        }
        /// <summary>Add books to service
        /// <remarks>If the any book from <see cref="books"/> already in service thrown <see cref="AddBookException"/> and no book is added</remarks>
        /// </summary>
        /// <param name="books">Books to add</param>
        public void AddBooks(IEnumerable<Book> books) {
            if (books == null)
                throw new ArgumentNullException(nameof(books));
            var enumerable = books as IList<Book> ?? books.ToList();
            foreach (var book in enumerable.Where(book => Books.Contains(book))) {
                throw new AddBookException($"BookListService already contains {book}");
            }
            Books.AddRange(enumerable);
        }
        /// <summary>Remove book from service
        /// <remarks>If the book isn't in service thrown <see cref="RemoveBookException"/></remarks>
        /// </summary>
        /// <param name="book">Book for removal</param>
        public void RemoveBook(Book book) {
            if (book == null)
                throw new ArgumentNullException(nameof(book));
            if (!Books.Remove(book)) 
                throw new RemoveBookException($"BookListService doesn't contain {book}");
        }

        /// <summary>Store books to to repository</summary>
        public void Store() {
            Repository.Store(Books);   
        }

        /// <summary>Sorts the elements in ascending order according to a key</summary>
        /// <param name="keySelector">A function to extract a key from an element</param>
        /// <returns>A <see cref="List{Book}"/> whose elements are sorted according to a key</returns>
        public List<Book> Sort(Func<Book, object> keySelector) => Books.OrderBy(keySelector).ToList();
        /// <summary>Retrieves all the element that match the conditions by the specified predicate</summary>
        /// <param name="match">The <see cref="Predicate{Book}"/> delegate that defines the conditions of the elements to search for</param>
        /// <returns>A <see cref="List{Book}"/> containing all the elements that match the conditions defined by the specified predicate, if found; otherwise, an empty <see cref="List{Book}"/></returns>
        public List<Book> Find(Predicate<Book> match) => Books.FindAll(match).ToList();

        #endregion
    }
}
