using System;
using System.Collections.Generic;

namespace Task1.BookListService.Services {
    public interface IRepository<T> where T : IEquatable<T> {
        /// <summary>Load all items from repository</summary>
        /// <returns>All items from repository</returns>
        List<T> Load();
        /// <summary>Clear repository and add books to repository</summary>
        void Store(IEnumerable<T> books);
    }
}
