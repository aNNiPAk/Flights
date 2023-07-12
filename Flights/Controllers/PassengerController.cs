using System.Diagnostics;
using Flights.Dtos;
using Flights.ReadModels;
using Microsoft.AspNetCore.Mvc;

namespace Flights.Controllers;

[Route("[controller]")]
[ApiController]
public class PassengerController : ControllerBase
{
    private static readonly IList<NewPassengerDto> Passengers = new List<NewPassengerDto>();

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public IActionResult Register(NewPassengerDto dto)
    {
        Passengers.Add(dto);
        Debug.WriteLine($"Registers Passenger count: {Passengers.Count}");
        return CreatedAtAction(nameof(Find), new { email = dto.Email });
    }

    [HttpGet("{{email}}")]
    public ActionResult<PassengerRm> Find(string email)
    {
        var passenger = Passengers.FirstOrDefault(p => p.Email == email);

        if (passenger == null) return NotFound();

        var rm = new PassengerRm(
            passenger.Email,
            passenger.FirstName,
            passenger.LastName,
            passenger.Gender
        );

        return Ok(rm);
    }
}