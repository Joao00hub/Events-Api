using EventsAPI.Interface;
using EventsAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventsAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddressController : MainController
{
    private readonly IAddressRepository _addressRepository;

    public AddressController(ILogger<AddressController> logger, IAddressRepository repository) : base(logger)
    {
        _addressRepository = repository;
    }

    [HttpGet]
    [MiddlewareLogs("AddressController", "GetAddressController")]
    public async Task<IActionResult> Get()
    {
        IList<Address> addresses = await _addressRepository.Get();
        return Ok(addresses);
    }

    [HttpGet("{id}")]
    [MiddlewareLogs("AddressController", "GetAddressController")]
    public async Task<IActionResult> Get(int id)
    {
        Address address = await _addressRepository.Get(id);
        return address == null ? NotFound("Adress not found") : Ok(address);
    }

    [HttpPost]
    [MiddlewareLogs("AddressController", "PostAddressController")]
    public async Task<IActionResult> Post([FromBody] Address address)
    {
        if (address == null)
        {
            return BadRequest("Address is null");
        }

        await _addressRepository.Add(address);

        return CreatedAtAction(nameof(Get), new { id = address.Id }, address);
    }

    [HttpPut]
    [MiddlewareLogs("AddressController", "PutAddressController")]
    public async Task<IActionResult> Put([FromBody] Address address)
    {
        if (address == null)
        {
            return BadRequest("Address is null");
        }

        await _addressRepository.Update(address);

        return CreatedAtAction(nameof(Get), new { id = address.Id }, address);
    }

    [HttpDelete("{id}")]
    [MiddlewareLogs("AddressController", "DeleteAddressController")]
    public async Task<IActionResult> Delete(int id)
    {
        await _addressRepository.Delete(id);
        return Ok();
    }
 }
