using EventsAPI.Models;

namespace EventsAPI.Interface;

public interface IEventRepository
{
    Task<IList<Event>> Get();

    Task<Event?> Get(int id);

    Task Add(Event usuario);

    Task Update(Event usuario);

    Task Delete(int id);
}
