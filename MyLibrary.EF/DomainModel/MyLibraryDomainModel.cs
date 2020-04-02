using MyLibrary.Common.DomainModel;

namespace MyLibrary.EF.DomainModel
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

        public IRepository<Author> AuthorRepository { get => authorRepository; }
        public IRepository<Press> PressRepository { get => pressRepository; }
        public IRepository<Book> BookRepository { get => bookRepository; }

        public void Dispose()
        {
            db.Dispose();
        }

        #endregion
    }
}