using Flights.Data;
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
        var bookings = _entities.Flights
            .SelectMany(f => f.Bookings
                .Where(b => b.PassengerEmail == email)
                .Select(_ =>
                    new BookingRm(
                        f.Id,
                        f.Airline,
                        f.Price,
                        new TimePlaceRm(f.Arrival.Place, f.Arrival.Time),
                        new TimePlaceRm(f.Departure.Place, f.Departure.Time),
                        f.RemainingNumberOfSeats,
                        email
                    )));

        return Ok(bookings);
    }
}