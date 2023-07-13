using Flights.Domain.Entities;
using Flights.Dtos;
using Flights.ReadModels;
using Microsoft.AspNetCore.Mvc;

namespace Flights.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightController : ControllerBase
{
    private static readonly Random Random = new();

    private static readonly Flight[] Flights =
    {
        new(Guid.NewGuid(),
            "American Airlines",
            new TimePlace("Istanbul", DateTime.Now.AddHours(Random.Next(4, 10))),
            new TimePlace("Los Angeles", DateTime.Now.AddHours(Random.Next(1, 3))),
            Random.Next(90, 5000).ToString(),
            Random.Next(1, 853)),
        new(Guid.NewGuid(),
            "Deutsche BA",
            new TimePlace("Schiphol", DateTime.Now.AddHours(Random.Next(4, 15))),
            new TimePlace("Munchen", DateTime.Now.AddHours(Random.Next(1, 10))),
            Random.Next(90, 5000).ToString(),
            Random.Next(1, 853)),
        new(Guid.NewGuid(),
            "British Airways",
            new TimePlace("Vizzola-Ticino", DateTime.Now.AddHours(Random.Next(4, 18))),
            new TimePlace("London, England", DateTime.Now.AddHours(Random.Next(1, 15))),
            Random.Next(90, 5000).ToString(),
            Random.Next(1, 853)),
        new(Guid.NewGuid(),
            "Basiq Air",
            new TimePlace("Glasgow, Scotland", DateTime.Now.AddHours(Random.Next(4, 21))),
            new TimePlace("Amsterdam", DateTime.Now.AddHours(Random.Next(1, 21))),
            Random.Next(90, 5000).ToString(),
            Random.Next(1, 853)),
        new(Guid.NewGuid(),
            "BB Heliag",
            new TimePlace("Baku", DateTime.Now.AddHours(Random.Next(4, 25))),
            new TimePlace("Zurich", DateTime.Now.AddHours(Random.Next(1, 23))),
            Random.Next(90, 5000).ToString(),
            Random.Next(1, 853))
    };

    private readonly ILogger<FlightController> _logger;


    public FlightController(ILogger<FlightController> logger)
    {
        _logger = logger;
    }

    [ProducesResponseType(typeof(IEnumerable<FlightRm>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public IEnumerable<FlightRm> Search()
    {
        return Flights.Select(x =>
            new FlightRm(
                x.Id,
                x.Airline,
                new TimePlaceRm(x.Arrival.Place, x.Arrival.Time),
                new TimePlaceRm(x.Departure.Place, x.Departure.Time),
                x.Price,
                x.RemainingNumberOfSeats
            ));
    }

    [ProducesResponseType(typeof(FlightRm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet("{id:guid}")]
    public ActionResult<FlightRm> Find(Guid id)
    {
        var flight = Flights.SingleOrDefault(x => x.Id == id);

        if (flight == null) return NotFound();

        var flightRm = new FlightRm(
            flight.Id,
            flight.Airline,
            new TimePlaceRm(flight.Arrival.Place, flight.Arrival.Time),
            new TimePlaceRm(flight.Departure.Place, flight.Departure.Time),
            flight.Price,
            flight.RemainingNumberOfSeats
        );

        return Ok(flightRm);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult Book(BookDto dto)
    {
        _logger.LogInformation("Booking a new flight {dto}", dto);

        var flight = Flights.SingleOrDefault(x => x.Id == dto.FlightId);

        if (flight == null) return NotFound();

        flight.Bookings.Add(new Booking(dto.FlightId, dto.PassengerEmail, dto.NumberOfSeats));
        return CreatedAtAction(nameof(Find), new { id = dto.FlightId });
    }
}