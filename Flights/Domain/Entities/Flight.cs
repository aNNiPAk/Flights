using Flights.Domain.Errors;

namespace Flights.Domain.Entities;

public class Flight
{
    public IList<Booking> Bookings = new List<Booking>();

    public Flight()
    {
    }

    public Flight(Guid id,
        string airline,
        TimePlace arrival,
        TimePlace departure,
        string price,
        int remainingNumberOfSeats)
    {
        Id = id;
        Airline = airline;
        Arrival = arrival;
        Departure = departure;
        Price = price;
        RemainingNumberOfSeats = remainingNumberOfSeats;
    }

    public Guid Id { get; init; }
    public string Airline { get; init; }
    public TimePlace Arrival { get; init; }
    public TimePlace Departure { get; init; }
    public string Price { get; init; }
    public int RemainingNumberOfSeats { get; set; }

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

    public object? CancelBooking(string passangerEmail, byte numberOfSeats)
    {
        var booking = Bookings.FirstOrDefault(b =>
            numberOfSeats == b.NumberOfSeats && string.Equals(passangerEmail, b.PassengerEmail,
                StringComparison.CurrentCultureIgnoreCase));

        if (booking == null) return new NotFoundError();

        Bookings.Remove(booking);
        RemainingNumberOfSeats += numberOfSeats;

        return null;
    }
}