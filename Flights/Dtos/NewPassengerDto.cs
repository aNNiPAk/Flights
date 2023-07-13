using System.ComponentModel.DataAnnotations;

namespace Flights.Dtos;

public record NewPassengerDto(
    [Required]
    [EmailAddress]
    [StringLength(100, MinimumLength = 3)]
    string Email,
    [Required]
    [StringLength(35, MinimumLength = 2)]
    string FirstName,
    [Required]
    [StringLength(35, MinimumLength = 2)]
    string LastName,
    [Required] bool Gender);