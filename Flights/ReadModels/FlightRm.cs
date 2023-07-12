namespace Flights.ReadModels;

public record FlightRm(
    Guid Id,
    string Airline,
    TimePlaceRm Arrival,
    TimePlaceRm Departure,
    string Price,
    int RemainingNumberOfSeats
);