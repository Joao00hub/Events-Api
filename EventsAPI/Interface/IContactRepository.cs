using EventsAPI.Models;

namespace EventsAPI.Interface;

public interface IContactRepository
{
    Task<IList<Contact>> Get();

    Task<Contact?> Get(int id);

    Task Add(Contact contact);

    Task Update(Contact contact);

    Task Delete(int id);
}
