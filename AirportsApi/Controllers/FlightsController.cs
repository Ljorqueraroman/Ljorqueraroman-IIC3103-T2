using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirportsApi.Contexts;
using AirportsApi.Entities;
using AirportsApi.Helpers;
using AirportsApi.Mapper;
using AirportsApi.Model;

namespace AirportsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly AirportsContext _context;

        public FlightsController(AirportsContext context)
        {
            _context = context;
        }

        // GET: api/Flights
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlightModelSmol>>> GetFlights()
        {
            var flights = await _context.Flights.ToListAsync();

            var flightsSmolModels = flights.Select(FlightMapper.ToSmol).ToList();

            return flightsSmolModels;
        }

        // GET: api/Flights/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FlightModelChonker>> GetFlight(string id)
        {
            var flight = await GetFlightWithAirports(id);

            if (flight == null)
            {
                return NotFound(ErrorMessageGenerator.FlightNotFound(id));
            }
            
            var flightModelChonker = FlightMapper.ToChonker(flight);
            
            return flightModelChonker;
        }

        // POST: api/Flights
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FlightModelSmol>> PostFlight(FlightModelSmol flightSmol)
        {
            if (FlightExists(flightSmol.Id))
            {
                return Conflict(ErrorMessageGenerator.FlightAlreadyExistsError(flightSmol.Id));
            }

            var flight = FlightMapper.ToEntity(flightSmol);

            var fieldErrors = FieldValidator.GetFieldErrors(flightSmol);
            if (fieldErrors.Any())
            {
                return BadRequest(ErrorMessageGenerator.InvalidFields(fieldErrors));
            }

            var departureId = flightSmol.Departure;
            var departure = await _context.Airports.FindAsync(departureId);
            if (departure == null)
            {
                return NotFound(ErrorMessageGenerator.AirportNotFound(departureId));
            }

            var destinationId = flightSmol.Destination;
            var destination = await _context.Airports.FindAsync(destinationId);
            if (destination == null)
            {
                return NotFound(ErrorMessageGenerator.AirportNotFound(destinationId));
            }

            flight.TotalDistance = CoordinatesHelper.CalculateDistanceKms(
                departure.Latitude, departure.Longitude, destination.Latitude, destination.Longitude);
            flight.Bearing = 0;
            flight.TraveledDistance = 0;

            flight.Latitude = departure.Latitude;
            flight.Longitude = departure.Longitude;

            _context.Flights.Add(flight);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFlight", new { id = flight.Id }, flightSmol);
        }

        // DELETE: api/Flights/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(string id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
            {
                return NotFound(ErrorMessageGenerator.FlightNotFound(id));
            }

            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{id}/position")]
        public async Task<ActionResult<FlightModelSmol>> PostFlight(string id, Position position)
        {
            var flight = await GetFlightWithAirports(id);

            if (flight == null)
            {
                return NotFound(ErrorMessageGenerator.FlightNotFound(id));
            }
            
            var fieldErrors = FieldValidator.GetFieldErrors(position);
            if (fieldErrors.Any())
            {
                return BadRequest(ErrorMessageGenerator.InvalidFields(fieldErrors));
            }

            var oldPosition = new Position
            {
                Lat = flight.Latitude,
                Long = flight.Longitude,
            };

            var deltaDistance = CoordinatesHelper.CalculateDistanceKms(oldPosition, position);

            flight.TraveledDistance += deltaDistance;
            flight.Bearing = CoordinatesHelper.CalculateBearing(oldPosition, position);

            flight.Latitude = position.Lat.Value;
            flight.Longitude = position.Long.Value;

            await _context.SaveChangesAsync();

            var flightChonker = FlightMapper.ToChonker(flight);

            return CreatedAtAction("GetFlight", new { id = flight.Id }, flightChonker);
        }

        private async Task<Flight> GetFlightWithAirports(string id)
        {
            return await GetFlightWithAirportsNavigation(id);
            //return await GetFlightWithAirportsBruteForce(id);
        }

        private async Task<Flight> GetFlightWithAirportsNavigation(string id)
        {
            return await _context.Flights
                .Include(f => f.Departure)
                .Include(f => f.Destination)
                .FirstOrDefaultAsync(f => f.Id == id);
        }


        private async Task<Flight> GetFlightWithAirportsBruteForce(string id)
        {
            var flight = await _context.Flights.FirstOrDefaultAsync(f => f.Id == id);

            if (flight == null) return null;

            var departure = await _context.Airports.FirstOrDefaultAsync(a => a.Id == flight.DepartureId);
            var destination = await _context.Airports.FirstOrDefaultAsync(a => a.Id == flight.DestinationId);

            flight.Destination = destination;
            flight.Departure = departure;

            return flight;
        }

        private bool FlightExists(string id)
        {
            return _context.Flights.Any(e => e.Id == id);
        }
    }
}
