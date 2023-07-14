namespace Flights.Domain.Entities;

public record Flight(
    Guid Id,
    string Airline,
    TimePlace Arrival,
    TimePlace Departure,
    string Price,
    int RemainingNumberOfSeats
)
{
    public readonly IList<Booking> Bookings = new List<Booking>();
    public int RemainingNumberOfSeats { get; set; } = RemainingNumberOfSeats;
}