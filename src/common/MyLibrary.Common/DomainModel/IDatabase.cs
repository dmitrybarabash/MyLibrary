using System;
using System.Collections.Generic;

namespace MyLibrary.Common.DomainModel;

public interface IDatabase : IDisposable
{
    IEnumerable<Author> GetAllAuthors();
    Author GetAuthorById(long id);
    long InsertAuthor(Author author);
    void UpdateAuthor(Author author);
    void DeleteAuthor(long id);

    IEnumerable<Press> GetAllPresses();
    Press GetPressById(long id);
    long InsertPress(Press press);
    void UpdatePress(Press press);
    void DeletePress(long id);

    IEnumerable<Book> GetAllBooks();
    Book GetBookById(long id);
    long InsertBook(Book book);
    void UpdateBook(Book book);
    void DeleteBook(long id);
}
