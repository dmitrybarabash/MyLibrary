#nullable disable

using System.Collections.Generic;

namespace MyLibrary.Common.DomainModel
{
    public class BookRepository : IRepository<Book>
    {
        private readonly IDatabase db;

        public BookRepository(IDatabase database)
        {
            db = database;
        }

        #region IRepository<Book> implementation

        public IEnumerable<Book> GetAll()
        {
            return db.GetAllBooks();
        }

        public Book GetById(long id)
        {
            return db.GetBookById(id);
        }

        public long Add(Book book)
        {
            return db.InsertBook(book);
        }

        public void Update(Book book)
        {
            db.UpdateBook(book);
        }

        public void Delete(long id)
        {
            db.DeleteBook(id);
        }

        #endregion
    }
}