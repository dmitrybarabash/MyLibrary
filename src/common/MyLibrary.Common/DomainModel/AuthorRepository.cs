using System.Collections.Generic;

namespace MyLibrary.Common.DomainModel;

public class AuthorRepository : IRepository<Author>
{
    private readonly IDatabase db;

    public AuthorRepository(IDatabase database)
    {
        db = database;
    }

    #region IRepository<Author> implementation

    public IEnumerable<Author> GetAll()
    {
        return db.GetAllAuthors();
    }

    public Author GetById(long id)
    {
        return db.GetAuthorById(id);
    }

    public long Add(Author author)
    {
        return db.InsertAuthor(author);
    }

    public void Update(Author author)
    {
        db.UpdateAuthor(author);
    }

    public void Delete(long id)
    {
        db.DeleteAuthor(id);
    }

    #endregion
}
