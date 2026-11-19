using MyLibrary.Common.DomainModel;

namespace MyLibrary.Common.DLWithLinqToDataSet.DomainModel;

public class MyLibraryDomainModel : IMyLibraryDomainModel
{
    private readonly IDatabase _db;

    public MyLibraryDomainModel()
    {
        _db = new Database();
        AuthorRepository = new AuthorRepository(_db);
        PressRepository = new PressRepository(_db);
        BookRepository = new BookRepository(_db);
    }

    #region IMyLibraryDomainModel implementation

    public IRepository<Author> AuthorRepository { get; }
    public IRepository<Press> PressRepository { get; }
    public IRepository<Book> BookRepository { get; }

    public void Dispose() =>
        _db.Dispose();

    #endregion
}