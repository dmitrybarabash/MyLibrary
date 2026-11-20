using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyLibrary.Common.DomainModel;
using MyLibrary.Common.Options;

namespace MyLibrary.Common.EF.DomainModel;

public class Database : IDatabase
{
    private readonly MyLibraryContext _context;

    public Database()
    {
        // Создаем объект контекста EF, указываем ему строку соединения и
        // получаем объект настроек для конструктора объекта контекста EF
        var options =
            new DbContextOptionsBuilder<MyLibraryContext>()
                .UseSqlServer(GlobalOptions.ConnectionString)
                .Options;

        // Создаем контекст EF
        _context = new MyLibraryContext(options);
    }

    #region IDisposable implementation

    public void Dispose()
    {
        _context.Dispose();
    }

    #endregion

    #region IDatabase implementation

    //
    // Authors management
    //

    public IEnumerable<Author> GetAllAuthors()
    {
        return _context.Authors.ToList();
    }

    public Author GetAuthorById(long id)
    {
        return _context.Authors.Find(id);
    }

    public long InsertAuthor(Author author)
    {
        _context.Authors.Add(author);
        _context.SaveChanges();

        // В этом нет необходимости, поскольку EF по после вызова SaveChanges()
        // сам заполняет автоинкрементные поля в добавленных объектах в соответствии
        // с полученными значениями для этих полей при добавлении в БД
        //return Convert.ToInt64(_context.Database.SqlQuery<decimal>("SELECT IDENT_CURRENT('Authors')").SingleOrDefault());

        return author.Id;
    }

    public void UpdateAuthor(Author author)
    {
        Author a = _context.Authors.Find(author.Id);
        if (a is not null)
        {
            a.Name = author.Name;
            _context.Entry(a).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }

    public void DeleteAuthor(long id)
    {
        Author author = _context.Authors.Find(id);
        if (author is not null)
        {
            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }


    //
    // Presses management
    //

    public IEnumerable<Press> GetAllPresses()
    {
        return _context.Presses.ToList();
    }

    public Press GetPressById(long id)
    {
        return _context.Presses.Find(id);
    }

    public long InsertPress(Press press)
    {
        _context.Presses.Add(press);
        _context.SaveChanges();

        // В этом нет необходимости, поскольку EF по после вызова SaveChanges()
        // сам заполняет автоинкрементные поля в добавленных объектах в соответствии
        // с полученными значениями для этих полей при добавлении в БД
        //return Convert.ToInt64(_context.Database.SqlQuery<decimal>("SELECT IDENT_CURRENT('Presses')").SingleOrDefault());

        return press.Id;
    }

    public void UpdatePress(Press press)
    {
        Press p = _context.Presses.Find(press.Id);
        if (p is not null)
        {
            p.Name = press.Name;
            _context.Entry(p).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }

    public void DeletePress(long id)
    {
        Press press = _context.Presses.Find(id);
        if (press is not null)
        {
            _context.Presses.Remove(press);
            _context.SaveChanges();
        }
    }


    //
    // Books management
    //

    public IEnumerable<Book> GetAllBooks()
    {
        return _context.Books.ToList();
    }

    public Book GetBookById(long id)
    {
        return _context.Books.Find(id);
    }

    public long InsertBook(Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();

        // В этом нет необходимости, поскольку EF по после вызова SaveChanges()
        // сам заполняет автоинкрементные поля в добавленных объектах в соответствии
        // с полученными значениями для этих полей при добавлении в БД
        //return Convert.ToInt64(_context.Database.SqlQuery<decimal>("SELECT IDENT_CURRENT('Books')").SingleOrDefault());

        return book.Id;
    }

    public void UpdateBook(Book book)
    {
        Book b = _context.Books.Find(book.Id);
        if (b is not null)
        {
            b.Name = book.Name;
            b.AuthorFk = book.AuthorFk;
            b.Pages = book.Pages;
            b.Price = book.Price;
            b.PressFk = book.PressFk;
            _context.Entry(b).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }

    public void DeleteBook(long id)
    {
        Book book = _context.Books.Find(id);
        if (book is not null)
        {
            _context.Books.Remove(book);
            _context.SaveChanges();
        }
    }

    #endregion
}
