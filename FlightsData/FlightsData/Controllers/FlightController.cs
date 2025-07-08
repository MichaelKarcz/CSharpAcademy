using FlightsData.Models;
using FlightsData.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightsData.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet]
        public ActionResult<List<Flight>> GetAllFlights()
        {
            return Ok(_flightService.GetAllFlights());
        }

        [HttpGet("{id}")]
        public ActionResult<Flight> GetFlightById(int id)
        {
            var result = _flightService.GetFlightById(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<Flight> CreateFlight(Flight flight)
        {
            return Ok(_flightService.CreateFlight(flight));
        }

        [HttpPut("{id}")]
        public ActionResult<Flight> UpdateFlight(int id, Flight updatedFlight)
        {
            var result = _flightService.UpdateFlight(id, updatedFlight);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult<string> DeleteFlight(int id)
        {
            var result = _flightService.DeleteFlight(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
