using System.Collections.Generic;

namespace MyLibrary.Common.DomainModel;

public class AuthorRepository : IRepository<Author>
{
    private readonly IDatabase _db;

    public AuthorRepository(IDatabase database)
    {
        _db = database;
    }

    #region IRepository<Author> implementation

    public IEnumerable<Author> GetAll()
    {
        return _db.GetAllAuthors();
    }

    public Author GetById(long id)
    {
        return _db.GetAuthorById(id);
    }

    public long Add(Author author)
    {
        return _db.InsertAuthor(author);
    }

    public void Update(Author author)
    {
        _db.UpdateAuthor(author);
    }

    public void Delete(long id)
    {
        _db.DeleteAuthor(id);
    }

    #endregion
}
