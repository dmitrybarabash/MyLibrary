using System.Collections.Generic;

namespace MyLibrary.Common.DomainModel;

public class PressRepository : IRepository<Press>
{
    private readonly IDatabase _db;

    public PressRepository(IDatabase database)
    {
        _db = database;
    }

    #region IRepository<Press> implementation

    public IEnumerable<Press> GetAll()
    {
        return _db.GetAllPresses();
    }

    public Press GetById(long id)
    {
        return _db.GetPressById(id);
    }

    public long Add(Press press)
    {
        return _db.InsertPress(press);
    }

    public void Update(Press press)
    {
        _db.UpdatePress(press);
    }

    public void Delete(long id)
    {
        _db.DeletePress(id);
    }

    #endregion
}
