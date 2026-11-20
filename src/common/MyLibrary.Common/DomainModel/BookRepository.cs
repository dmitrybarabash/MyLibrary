using System.Collections.Generic;

namespace MyLibrary.Common.DomainModel;

public class BookRepository : IRepository<Book>
{
    private readonly IDatabase _db;

    public BookRepository(IDatabase database)
    {
        _db = database;
    }

    #region IRepository<Book> implementation

    public IEnumerable<Book> GetAll()
    {
        return _db.GetAllBooks();
    }

    public Book GetById(long id)
    {
        return _db.GetBookById(id);
    }

    public long Add(Book book)
    {
        return _db.InsertBook(book);
    }

    public void Update(Book book)
    {
        _db.UpdateBook(book);
    }

    public void Delete(long id)
    {
        _db.DeleteBook(id);
    }

    #endregion
}
