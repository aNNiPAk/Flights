using Flights.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Flights.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private static IList<NewPassengerDto> _passengers = new List<NewPassengerDto>();
        
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Register(NewPassengerDto dto)
        {
            _passengers.Add(dto);
            System.Diagnostics.Debug.WriteLine($"Registers Passenger count: {_passengers.Count}");
            return Ok();
        }
    }
}
