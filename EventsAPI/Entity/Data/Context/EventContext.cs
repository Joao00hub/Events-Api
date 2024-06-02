using EventsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventsAPI.Entity.Data.Context;

public class EventContext : DbContext
{
    public EventContext(DbContextOptions<EventContext> options)
       : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("EventDatabase");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Address>().HasData(
             new Address
             {
                 Id = 1,
                 Type = 0,
                 Street = "New Steet",
                 Number = "123",
                 City = "An City",
                 State = "An State",
                 PostalCode = "98520",
             }
       );

        modelBuilder.Entity<Contact>().HasData(
             new Contact
             {
                 Id = 1,
                 Email = "email@email.com",
                 Phone = "+55 31 12345-6789",
                 Type = 0,
                 LocalId = 1,
             }
        );
       
        modelBuilder.Entity<Local>().HasData(
              new Local
              {
                  Id = 1,
                  Name = "Initial Local",
                  AddressId = 1,
                  Latitude = 40.778848,
                  Longitude = -73.968898,
                  Facilities = new List<string> { "Facility1", "Facility2" },
                  Capacity = 1000
              }
        );

        modelBuilder.Entity<Event>().HasData(
            new Event
            {
                Id = 1,
                Name = "Initial Event",
                LocalId = 1,
                Title = "Title Initial Event",
                EventInitDate = DateTimeOffset.Now,
                EventEndDate = DateTimeOffset.Now.AddHours(20),
                ParticipantsLimit = 100
            }
        );
    }

    public DbSet<Event> Events { get; set; }

    public DbSet<Contact> Contacts { get; set; }

    public DbSet<Address> Addresses { get; set; }

    public DbSet<Local> Locals { get; set; }

}
