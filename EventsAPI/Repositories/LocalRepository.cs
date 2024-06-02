using EventsAPI.Entity.Data.Context;
using EventsAPI.Interface;
using EventsAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EventsAPI.Repositories;

public class LocalRepository : ILocalRepository
{
    private readonly EventContext _db;

    public LocalRepository(EventContext db)
    {
        _db = db;
    }

    public async Task Add(Local local)
    {
        var addressExists = await _db.Addresses.AnyAsync(a => a.Id == local.AddressId);

        if (!addressExists)
        {
            throw new ArgumentException("The specified LocalId does not exist.");
        }

        _db.Add(local);
        await _db.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        Local? local = await Get(id);
        if (local != null)
        {
            _db.Remove(local);
            await _db.SaveChangesAsync();
        }
    }

    public async Task<IList<Local>> Get()
    {
        return await _db.Locals.OrderBy(l => l.Name).ToListAsync();
    }

    public async Task<Local?> Get(int id)
    {
        return await _db.Locals.FindAsync(id);
    }

    public async Task Update(Local local)
    {
        _db.Update(local);
        await _db.SaveChangesAsync();
    }

    Task<Local?> ILocalRepository.Get(int id)
    {
        throw new NotImplementedException();
    }
}
