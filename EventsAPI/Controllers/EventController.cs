using EventsAPI.Interface;
using EventsAPI.Models;
using EventsAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EventsAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventController : MainController
{
    private readonly IEventRepository _eventRepository;

    public EventController(ILogger<EventController> logger, IEventRepository repository) : base(logger)
    {
        _eventRepository = repository;
    }

    [HttpGet]
    [MiddlewareLogs("EventController", "GetEventController")]
    public async Task<IActionResult> Get()
    {
        IList<Event> events = await _eventRepository.Get();
        return Ok(events);
    }

    [HttpGet("{id}")]
    [MiddlewareLogs("EventController", "GetEventController")]
    public async Task<IActionResult> Get(int id)
    {
        Event? Event = await _eventRepository.Get(id);
        return Event == null ? NotFound("Event not found") : Ok(Event);
    }

    [HttpPost]
    [MiddlewareLogs("EventController", "PostEventController")]
    public async Task<IActionResult> Post([FromBody] Event Event)
    {
        if (Event == null)
        {
            return BadRequest("Event is null");
        }

        await _eventRepository.Add(Event);

        return CreatedAtAction(nameof(Get), new { id = Event.Id }, Event);
    }

    [HttpPut]
    [MiddlewareLogs("EventController", "PutEventController")]
    public async Task<IActionResult> Put([FromBody] Event Event)
    {
        if (Event == null)
        {
            return BadRequest("Event is null");
        }

        await _eventRepository.Update(Event);

        return CreatedAtAction(nameof(Get), new { id = Event.Id }, Event);
    }

    [HttpDelete("{id}")]
    [MiddlewareLogs("EventController", "DeleteEventController")]
    public async Task<IActionResult> Delete(int id)
    {
        await _eventRepository.Delete(id);
        return Ok();
    }
}
