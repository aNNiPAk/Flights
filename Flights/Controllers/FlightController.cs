﻿using Flights.ReadModels;
using Microsoft.AspNetCore.Mvc;

namespace Flights.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly ILogger<FlightController> _logger;
        private Random _random = new();

        public FlightController(ILogger<FlightController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<FlightRm> Get() => new List<FlightRm>
            {
                new (Guid.NewGuid(),
                    "American Airlines",
                    new TimePlaceRm("Istanbul", DateTime.Now.AddHours(_random.Next(4, 10))),
                    new TimePlaceRm("Los Angeles", DateTime.Now.AddHours(_random.Next(1, 3))),
                    _random.Next(90, 5000).ToString(),
                    _random.Next(1, 853)),
                new ( Guid.NewGuid(),
                    "Deutsche BA",
                    new TimePlaceRm("Schiphol",DateTime.Now.AddHours(_random.Next(4, 15))),
                    new TimePlaceRm("Munchen",DateTime.Now.AddHours(_random.Next(1, 10))),
                    _random.Next(90, 5000) .ToString(),
                    _random.Next(1, 853)),
                new ( Guid.NewGuid(),
                    "British Airways",
                    new TimePlaceRm("Vizzola-Ticino",DateTime.Now.AddHours(_random.Next(4, 18))),
                    new TimePlaceRm("London, England",DateTime.Now.AddHours(_random.Next(1, 15))),
                    _random.Next(90, 5000).ToString(),
                    _random.Next(1, 853)),
                new ( Guid.NewGuid(),
                    "Basiq Air",
                    new TimePlaceRm("Glasgow, Scotland",DateTime.Now.AddHours(_random.Next(4, 21))),
                    new TimePlaceRm("Amsterdam",DateTime.Now.AddHours(_random.Next(1, 21))),
                    _random.Next(90, 5000).ToString(),
                    _random.Next(1, 853)),
                new (Guid.NewGuid(), 
                    "BB Heliag",
                    new TimePlaceRm("Baku",DateTime.Now.AddHours(_random.Next(4, 25))),
                    new TimePlaceRm("Zurich",DateTime.Now.AddHours(_random.Next(1, 23))),
                    _random.Next(90, 5000).ToString(),
                    _random.Next(1, 853))
            };
    }
}