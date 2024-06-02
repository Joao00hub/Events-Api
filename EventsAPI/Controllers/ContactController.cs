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
    [MiddlewareLogs("ContactController", "GetContactController")]
    public async Task<IActionResult> Get()
    {
        IList<Contact> events = await _contactRepository.Get();
        return Ok(events);
    }

    [HttpGet("{id}")]
    [MiddlewareLogs("ContactController", "GetContactController")]
    public async Task<IActionResult> Get(int id)
    {
        Contact Contact = await _contactRepository.Get(id);
        return Contact == null ? NotFound("Contact not found") : Ok(Contact);
    }

    [HttpPost]
    [MiddlewareLogs("ContactController", "PostContactController")]
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
    [MiddlewareLogs("ContactController", "PutContactController")]
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
    [MiddlewareLogs("ContactController", "DeleteContactController")]
    public async Task<IActionResult> Delete(int id)
    {
        await _contactRepository.Delete(id);
        return Ok();
    }
}
