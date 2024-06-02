using EventsAPI.Entity.Data.Context;
using EventsAPI.Interface;
using EventsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventsAPI.Repositories;

public class EventRepository : IEventRepository
{
    private readonly EventContext _db;

    public EventRepository(EventContext db)
    {
        _db = db;
    }

    public async Task Add(Event @event)
    {
        var localExists = await _db.Locals.AnyAsync(l => l.Id == @event.LocalId);

        if (!localExists)
        {
            throw new ArgumentException("The specified LocalId does not exist.");
        }

        _db.Add(@event);
        await _db.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        Event? @event = await Get(id);
        if (@event != null)
        {
            _db.Remove(@event);
            await _db.SaveChangesAsync();
        }
    }

    public async Task<IList<Event>> Get()
    {
        return await _db.Events.OrderBy(e => e.Name).ToListAsync();
    }

    public async Task<Event?> Get(int id)
    {
        return await _db.Events.FindAsync(id);
    }

    public async Task Update(Event @event)
    {
        _db.Update(@event);
        await _db.SaveChangesAsync();
    }

    Task<Event?> IEventRepository.Get(int id)
    {
        throw new NotImplementedException();
    }
}

