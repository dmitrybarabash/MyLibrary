using System.Collections.Generic;

namespace MyLibrary.Common.DomainModel;

public class PressRepository : IRepository<Press>
{
    private readonly IDatabase db;

    public PressRepository(IDatabase database)
    {
        db = database;
    }

    #region IRepository<Press> implementation

    public IEnumerable<Press> GetAll()
    {
        return db.GetAllPresses();
    }

    public Press GetById(long id)
    {
        return db.GetPressById(id);
    }

    public long Add(Press press)
    {
        return db.InsertPress(press);
    }

    public void Update(Press press)
    {
        db.UpdatePress(press);
    }

    public void Delete(long id)
    {
        db.DeletePress(id);
    }

    #endregion
}
