using System;

namespace MyLibrary.Common.DomainModel
{
    public interface IMyLibraryDomainModel : IDisposable
    {
        IRepository<Author> AuthorRepository { get; }
        IRepository<Press> PressRepository { get; }
        IRepository<Book> BookRepository { get; }
    }
}