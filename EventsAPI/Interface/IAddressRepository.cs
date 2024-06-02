using EventsAPI.Models;

namespace EventsAPI.Interface;

public interface IAddressRepository
{
    Task<IList<Address>> Get();

    Task<Address?> Get(int id);

    Task Add(Address address);

    Task Update(Address address);

    Task Delete(int id);
}
