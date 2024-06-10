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
    public async Task<IActionResult> Get()
    {
        try
        {
            IList<Address> addresses = await _addressRepository.Get();
            return Ok(addresses);
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
       
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        Address address = await _addressRepository.Get(id);
        return address == null ? NotFound("Adress not found") : Ok(address);
    }

    [HttpPost]
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
    public async Task<IActionResult> Delete(int id)
    {
        await _addressRepository.Delete(id);
        return Ok();
    }
 }
