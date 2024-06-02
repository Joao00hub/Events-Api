using EventsAPI.Interface;
using EventsAPI.Models;
using EventsAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EventsAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocalController : MainController
{
    private readonly ILocalRepository _localRepository;

    public LocalController(ILogger<LocalController> logger, ILocalRepository repository) : base(logger)
    {
        _localRepository = repository;
    }

    [HttpGet]
    [MiddlewareLogs("LocalController", "GetLocalController")]
    public async Task<IActionResult> Get()
    {
        IList<Local> locals = await _localRepository.Get();
        return Ok(locals);
    }

    [HttpGet("{id}")]
    [MiddlewareLogs("LocalController", "GetLocalController")]
    public async Task<IActionResult> Get(int id)
    {
        Local? local = await _localRepository.Get(id);
        return local == null ? NotFound("Local not found") : Ok(local);
    }

    [HttpPost]
    [MiddlewareLogs("LocalController", "PostLocalController")]
    public async Task<IActionResult> Post([FromBody] Local local)
    {
        if (local == null)
        {
            return BadRequest("local is null");
        }

        await _localRepository.Add(local);

        return CreatedAtAction(nameof(Get), new { id = local.Id }, local);
    }

    [HttpPut]
    [MiddlewareLogs("LocalController", "PutLocalController")]
    public async Task<IActionResult> Put([FromBody] Local local)
    {
        if (local == null)
        {
            return BadRequest("Local is null");
        }

        await _localRepository.Update(local);

        return CreatedAtAction(nameof(Get), new { id = local.Id }, local);
    }

    [HttpDelete("{id}")]
    [MiddlewareLogs("LocalController", "DeleteLocalController")]
    public async Task<IActionResult> Delete(int id)
    {
        await _localRepository.Delete(id);
        return Ok();
    }
}
