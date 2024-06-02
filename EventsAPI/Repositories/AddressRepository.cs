using EventsAPI.Entity.Data.Context;
using EventsAPI.Interface;
using EventsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventsAPI.Repositories;

public class AddressRepository : IAddressRepository
{
    private readonly EventContext _db;

    public AddressRepository(EventContext db)
    {
        _db = db;
    }

    public async Task Add(Address address)
    {
        _db.Add(address);
        await _db.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        Address? address = await Get(id);
        if (address != null)
        {
            _db.Remove(address);
            await _db.SaveChangesAsync();
        }
    }

    public async Task<IList<Address>> Get()
    {
        return await _db.Addresses.OrderBy(a => a.City).ToListAsync();
    }

    public async Task<Address?> Get(int id)
    {
        return await _db.Addresses.FindAsync(id);
    }

    public async Task Update(Address address)
    {
        _db.Update(address);
        await _db.SaveChangesAsync();
    }
}
