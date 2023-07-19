using System.ComponentModel;

namespace Flights.Dtos;

public record FlightSearchParameters(
    [DefaultValue("Los Angeles")] string? From,
    [DefaultValue("Berlin")] string? Destination,
    [DefaultValue("01/01/2000 00:00:00 AM")]
    DateTime? FromDate,
    [DefaultValue("01/02/2000 00:00:00 AM")]
    DateTime? ToDate,
    [DefaultValue(1)] int? NumberOfPassenger
);