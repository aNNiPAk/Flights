using Flights.Domain.Entities;

namespace Flights.Data;

public class Entities
{
    public static readonly IList<Passenger> Passengers = new List<Passenger>();

    private static readonly Random Random = new();

    public static readonly Flight[] Flights =
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
}