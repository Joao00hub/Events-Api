using EventsAPI.Models;

namespace EventsAPI.Interface;

public interface ILocalRepository
{
    Task<IList<Local>> Get();

    Task<Local?> Get(int id);

    Task Add(Local local);

    Task Update(Local local);

    Task Delete(int id);
}
