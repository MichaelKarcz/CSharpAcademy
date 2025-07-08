using FlightsData.Data;
using FlightsData.Models;

namespace FlightsData.Services
{
    public interface IFlightService
    {
        public List<Flight> GetAllFlights();
        public Flight? GetFlightById(int id);
        public Flight CreateFlight(Flight flight);
        public Flight UpdateFlight(int id, Flight updatedFlight);
        public string? DeleteFlight(int id);
    }

    public class FlightService : IFlightService
    {
        private readonly FlightsDbContext _dbContext;

        public FlightService(FlightsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Flight CreateFlight(Flight flight)
        {
            var savedFlight = _dbContext.Flights.Add(flight);
            _dbContext.SaveChanges();
            return savedFlight.Entity;
        }

        public string? DeleteFlight(int id)
        {
            Flight savedFlight = _dbContext.Flights.Find(id);

            if (savedFlight == null)
            {
                return null;
            }

            _dbContext.Flights.Remove(savedFlight);
            _dbContext.SaveChanges();

            return $"Successfully deleted flight with id: {id}";
        }

        public List<Flight> GetAllFlights()
        {
            return _dbContext.Flights.ToList();
        }

        public Flight? GetFlightById(int id)
        {
            Flight savedFlight = _dbContext.Flights.Find(id);
            return savedFlight == null ? null : savedFlight;
        }

        public Flight UpdateFlight(int id, Flight flight)
        {
            Flight savedFlight = _dbContext.Flights.Find(id);

            if (savedFlight == null)
            {
                return null;
            }

            _dbContext.Entry(savedFlight).CurrentValues.SetValues(flight);
            _dbContext.SaveChanges();

            return savedFlight;
        }
    }
}
