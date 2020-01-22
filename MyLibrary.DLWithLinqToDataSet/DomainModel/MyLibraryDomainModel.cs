using MyLibrary.Common.DomainModel;

namespace MyLibrary.DLWithLinqToDataSet.DomainModel
{
    public class MyLibraryDomainModel : IMyLibraryDomainModel
    {
        private readonly IDatabase db;
        private readonly IRepository<Author> authorRepository;
        private readonly IRepository<Press> pressRepository;
        private readonly IRepository<Book> bookRepository;

        public MyLibraryDomainModel()
        {
            db = new Database();
            authorRepository = new AuthorRepository(db);
            pressRepository = new PressRepository(db);
            bookRepository = new BookRepository(db);
        }

        #region IMyLibraryDomainModel implementation

        public IRepository<Author> AuthorRepository { get { return authorRepository; } }
        public IRepository<Press> PressRepository { get { return pressRepository; } }
        public IRepository<Book> BookRepository { get { return bookRepository; } }

        public void Dispose()
        {
            db.Dispose();
        }

        #endregion
    }
}