using MyLibrary.Common.DomainModel;

namespace MyLibrary.WinForms.DLWithLinqToDataSet.DomainModel
{
    public class MyLibraryDomainModel : IMyLibraryDomainModel
    {
        private readonly IDatabase db;

        public MyLibraryDomainModel()
        {
            db = new Database();
            AuthorRepository = new AuthorRepository(db);
            PressRepository = new PressRepository(db);
            BookRepository = new BookRepository(db);
        }

        #region IMyLibraryDomainModel implementation

        public IRepository<Author> AuthorRepository { get; }
        public IRepository<Press> PressRepository { get; }
        public IRepository<Book> BookRepository { get; }

        public void Dispose() =>
            db.Dispose();

        #endregion
    }
}