using EventsAPI.Entity.Data.Context;
using EventsAPI.Interface;
using EventsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventsAPI.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly EventContext _db;

    public ContactRepository(EventContext db)
    {
        _db = db;
    }

    public async Task Add(Contact contact)
    {
        _db.Add(contact);
        await _db.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        Contact? contact = await Get(id);
        if (contact != null)
        {
            _db.Remove(contact);
            await _db.SaveChangesAsync();
        }
    }

    public async Task<IList<Contact>> Get()
    {
        return await _db.Contacts.OrderBy(c => c.Email).ToListAsync();
    }

    public async Task<Contact?> Get(int id)
    {
        return await _db.Contacts.FindAsync(id);
    }

    public async Task Update(Contact contact)
    {
        _db.Update(contact);
        await _db.SaveChangesAsync();
    }
}
