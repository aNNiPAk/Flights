namespace Flights.ReadModels;

public record BookingRm(
    Guid FlightId,
    string Airline,
    string Price,
    TimePlaceRm Arrival,
    TimePlaceRm Departure,
    int RemainingNumberOfSeats,
    string PassengerEmail
);