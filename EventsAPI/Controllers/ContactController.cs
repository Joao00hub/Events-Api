using EventsAPI.Interface;
using EventsAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventsAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController : MainController
{
    private readonly IContactRepository _contactRepository;

    public ContactController(ILogger<ContactController> logger, IContactRepository repository) : base(logger)
    {
        _contactRepository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        IList<Contact> events = await _contactRepository.Get();
        return Ok(events);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        Contact Contact = await _contactRepository.Get(id);
        return Contact == null ? NotFound("Contact not found") : Ok(Contact);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Contact Contact)
    {
        if (Contact == null)
        {
            return BadRequest("Contact is null");
        }

        await _contactRepository.Add(Contact);

        return CreatedAtAction(nameof(Get), new { id = Contact.Id }, Contact);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Contact Contact)
    {
        if (Contact == null)
        {
            return BadRequest("Contact is null");
        }

        await _contactRepository.Update(Contact);

        return CreatedAtAction(nameof(Get), new { id = Contact.Id }, Contact);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _contactRepository.Delete(id);
        return Ok();
    }
}
