using EventsAPI.Entity.Data.Context;

namespace EventsAPI.Repositories;

public class BaseRepository
{
    protected readonly EventContext _db;

    public BaseRepository(EventContext db)
    {
        _db = db;
    }
}
