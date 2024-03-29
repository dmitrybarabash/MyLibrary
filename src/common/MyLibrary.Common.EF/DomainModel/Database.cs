﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyLibrary.Common.DomainModel;
using MyLibrary.Common.Options;

namespace MyLibrary.Common.EF.DomainModel;

public class Database : IDatabase
{
    private readonly MyLibraryContext context;

    public Database()
    {
        // Создаем объект контекста EF, указываем ему строку соединения и
        // получаем объект настроек для конструктора объекта контекста EF
        var options =
            new DbContextOptionsBuilder<MyLibraryContext>()
                .UseSqlServer(GlobalOptions.ConnectionString)
                .Options;

        // Создаем контекст EF
        context = new MyLibraryContext(options);
    }

    #region IDisposable implementation

    public void Dispose()
    {
        context.Dispose();
    }

    #endregion

    #region IDatabase implementation

    //
    // Authors management
    //

    public IEnumerable<Author> GetAllAuthors()
    {
        return context.Authors.ToList();
    }

    public Author GetAuthorById(long id)
    {
        return context.Authors.Find(id);
    }

    public long InsertAuthor(Author author)
    {
        context.Authors.Add(author);
        context.SaveChanges();

        // В этом нет необходимости, поскольку EF по после вызова SaveChanges()
        // сам заполняет автоинкрементные поля в добавленных объектах в соответствии
        // с полученными значениями для этих полей при добавлении в БД
        //return Convert.ToInt64(context.Database.SqlQuery<decimal>("SELECT IDENT_CURRENT('Authors')").SingleOrDefault());

        return author.Id;
    }

    public void UpdateAuthor(Author author)
    {
        Author a = context.Authors.Find(author.Id);
        if (a is not null)
        {
            a.Name = author.Name;
            context.Entry(a).State = EntityState.Modified;
            context.SaveChanges();
        }
    }

    public void DeleteAuthor(long id)
    {
        Author author = context.Authors.Find(id);
        if (author is not null)
        {
            context.Authors.Remove(author);
            context.SaveChanges();
        }
    }


    //
    // Presses management
    //

    public IEnumerable<Press> GetAllPresses()
    {
        return context.Presses.ToList();
    }

    public Press GetPressById(long id)
    {
        return context.Presses.Find(id);
    }

    public long InsertPress(Press press)
    {
        context.Presses.Add(press);
        context.SaveChanges();

        // В этом нет необходимости, поскольку EF по после вызова SaveChanges()
        // сам заполняет автоинкрементные поля в добавленных объектах в соответствии
        // с полученными значениями для этих полей при добавлении в БД
        //return Convert.ToInt64(context.Database.SqlQuery<decimal>("SELECT IDENT_CURRENT('Presses')").SingleOrDefault());

        return press.Id;
    }

    public void UpdatePress(Press press)
    {
        Press p = context.Presses.Find(press.Id);
        if (p is not null)
        {
            p.Name = press.Name;
            context.Entry(p).State = EntityState.Modified;
            context.SaveChanges();
        }
    }

    public void DeletePress(long id)
    {
        Press press = context.Presses.Find(id);
        if (press is not null)
        {
            context.Presses.Remove(press);
            context.SaveChanges();
        }
    }


    //
    // Books management
    //

    public IEnumerable<Book> GetAllBooks()
    {
        return context.Books.ToList();
    }

    public Book GetBookById(long id)
    {
        return context.Books.Find(id);
    }

    public long InsertBook(Book book)
    {
        context.Books.Add(book);
        context.SaveChanges();

        // В этом нет необходимости, поскольку EF по после вызова SaveChanges()
        // сам заполняет автоинкрементные поля в добавленных объектах в соответствии
        // с полученными значениями для этих полей при добавлении в БД
        //return Convert.ToInt64(context.Database.SqlQuery<decimal>("SELECT IDENT_CURRENT('Books')").SingleOrDefault());

        return book.Id;
    }

    public void UpdateBook(Book book)
    {
        Book b = context.Books.Find(book.Id);
        if (b is not null)
        {
            b.Name = book.Name;
            b.AuthorFk = book.AuthorFk;
            b.Pages = book.Pages;
            b.Price = book.Price;
            b.PressFk = book.PressFk;
            context.Entry(b).State = EntityState.Modified;
            context.SaveChanges();
        }
    }

    public void DeleteBook(long id)
    {
        Book book = context.Books.Find(id);
        if (book is not null)
        {
            context.Books.Remove(book);
            context.SaveChanges();
        }
    }

    #endregion
}
