using Flights.Data;
using Flights.Domain.Errors;
using Flights.Dtos;
using Flights.ReadModels;
using Microsoft.AspNetCore.Mvc;

namespace Flights.Controllers;

[Route("[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly Entities _entities;

    public BookingController(Entities entities)
    {
        _entities = entities;
    }

    [HttpGet("{email}")]
    [ProducesResponseType(typeof(IEnumerable<BookingRm>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IEnumerable<BookingRm>> List(string email)
    {
        var bookings = _entities.Flights.ToList()
            .SelectMany(f => f.Bookings
                .Where(b => b.PassengerEmail == email)
                .Select(b =>
                    new BookingRm(
                        f.Id,
                        f.Airline,
                        f.Price,
                        new TimePlaceRm(f.Arrival.Place, f.Arrival.Time),
                        new TimePlaceRm(f.Departure.Place, f.Departure.Time),
                        b.NumberOfSeats,
                        email
                    )));

        return Ok(bookings);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Cancel(BookDto dto)
    {
        var flight = _entities.Flights.Find(dto.FlightId);

        var error = flight?.CancelBooking(dto.PassengerEmail, dto.NumberOfSeats);

        switch (error)
        {
            case null:
                _entities.SaveChanges();
                return NoContent();
            case NotFoundError:
                return NotFound();
            default:
                throw new Exception(
                    $"The error of type: {error.GetType().Name} occurred while cancelling the booking made by {dto.PassengerEmail}");
        }
    }
}