using Flights.ReadModels;
using Microsoft.AspNetCore.Mvc;

namespace Flights.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly ILogger<FlightController> _logger;

        private static readonly Random Random = new();

        private static readonly FlightRm[] Flights = {
            new (Guid.NewGuid(),
                "American Airlines",
                new TimePlaceRm("Istanbul", DateTime.Now.AddHours(Random.Next(4, 10))),
                new TimePlaceRm("Los Angeles", DateTime.Now.AddHours(Random.Next(1, 3))),
                Random.Next(90, 5000).ToString(),
                Random.Next(1, 853)),
            new (Guid.NewGuid(),
                "Deutsche BA",
                new TimePlaceRm("Schiphol", DateTime.Now.AddHours(Random.Next(4, 15))),
                new TimePlaceRm("Munchen", DateTime.Now.AddHours(Random.Next(1, 10))),
                Random.Next(90, 5000) .ToString(),
                Random.Next(1, 853)),
            new (Guid.NewGuid(),
                "British Airways",
                new TimePlaceRm("Vizzola-Ticino", DateTime.Now.AddHours(Random.Next(4, 18))),
                new TimePlaceRm("London, England", DateTime.Now.AddHours(Random.Next(1, 15))),
                Random.Next(90, 5000).ToString(),
                Random.Next(1, 853)),
            new (Guid.NewGuid(),
                "Basiq Air",
                new TimePlaceRm("Glasgow, Scotland", DateTime.Now.AddHours(Random.Next(4, 21))),
                new TimePlaceRm("Amsterdam", DateTime.Now.AddHours(Random.Next(1, 21))),
                Random.Next(90, 5000).ToString(),
                Random.Next(1, 853)),
            new (Guid.NewGuid(),
                "BB Heliag",
                new TimePlaceRm("Baku", DateTime.Now.AddHours(Random.Next(4, 25))),
                new TimePlaceRm("Zurich", DateTime.Now.AddHours(Random.Next(1, 23))),
                Random.Next(90, 5000).ToString(),
                Random.Next(1, 853))
        };


        public FlightController(ILogger<FlightController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<FlightRm> Search() => Flights;

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:guid}")]
        public ActionResult<FlightRm> Find(Guid id)
        {
            var flight = Flights.SingleOrDefault(x => x.Id == id);

            return flight == null ? NotFound() : Ok(flight);
        }
    }
}