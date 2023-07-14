using Flights.Data;
using Flights.Domain.Errors;
using Flights.Dtos;
using Flights.ReadModels;
using Microsoft.AspNetCore.Mvc;

namespace Flights.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightController : ControllerBase
{
    private readonly Entities _entities;
    private readonly ILogger<FlightController> _logger;

    public FlightController(ILogger<FlightController> logger, Entities entities)
    {
        _logger = logger;
        _entities = entities;
    }

    [ProducesResponseType(typeof(IEnumerable<FlightRm>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public IEnumerable<FlightRm> Search()
    {
        return _entities.Flights.Select(x =>
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
        var flight = _entities.Flights.SingleOrDefault(x => x.Id == id);

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

        var flight = _entities.Flights.SingleOrDefault(x => x.Id == dto.FlightId);

        if (flight == null) return NotFound();

        var error = flight.MakeBooking(dto.PassengerEmail, dto.NumberOfSeats);
        if (error is OverbookError)
            return Conflict(new { message = "The number of requested seats exceeds the number of remaining seats" });

        _entities.SaveChanges();

        return CreatedAtAction(nameof(Find), new { id = dto.FlightId });
    }
}