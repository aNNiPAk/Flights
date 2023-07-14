using Flights.Domain.Errors;

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

    internal object? MakeBooking(string passengerEmail, byte numberOfSeats)
    {
        if (RemainingNumberOfSeats < numberOfSeats)
            return new OverbookError();

        Bookings.Add(
            new Booking(
                passengerEmail,
                numberOfSeats
            )
        );
        RemainingNumberOfSeats -= numberOfSeats;

        return null;
    }
}