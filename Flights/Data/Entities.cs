using Flights.Domain.Entities;

namespace Flights.Data;

public class Entities
{
    public readonly List<Flight> Flights = new();
    public readonly IList<Passenger> Passengers = new List<Passenger>();
}