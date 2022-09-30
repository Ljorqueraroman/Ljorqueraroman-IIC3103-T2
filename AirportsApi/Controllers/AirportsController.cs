using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirportsApi.Contexts;
using AirportsApi.Helpers;
using AirportsApi.Model;

namespace AirportsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportsController : ControllerBase
    {
        private readonly AirportsContext _context;

        public AirportsController(AirportsContext context)
        {
            _context = context;
        }

        // GET: api/Airports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AirportModelSmol>>> GetAirports()
        {
            var airports = await _context.Airports.ToListAsync();

            var airportSmolModels = airports.Select(AirportMapper.ToSmol).ToList();

            return airportSmolModels;
        }

        // GET: api/Airports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AirportModelChonker>> GetAirport(string id)
        {
            var airport = await _context.Airports.FindAsync(id);

            if (airport == null)
            {
                return NotFound(ErrorMessageGenerator.AirportNotFound(id));
            }
            
            var airportModelChonker = AirportMapper.ToChonker(airport);
            return airportModelChonker;
        }
        
        // POST: api/Airports
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AirportModelChonker>> PostAirport(AirportModelChonker airportChonker)
        {
            if (AirportExists(airportChonker.Id))
            {
                return Conflict(ErrorMessageGenerator.AirportAlreadyExistsError(airportChonker.Id));
            }

            var airport = AirportMapper.ToEntity(airportChonker);

            var errors = FieldValidator.GetFieldErrors(airportChonker);
            if (errors.Count > 0)
            {
                return BadRequest(ErrorMessageGenerator.InvalidFields(errors));
            }

            _context.Airports.Add(airport);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAirport", new { id = airport.Id }, airportChonker);
        }

        // PATCH: api/Airports
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAirport(string id, string name)
        {
            var airport = await _context.Airports.FindAsync(id);

            //TODO: NAME NOT A STRING ERROR

            if (airport == null)
            {
                return NotFound(ErrorMessageGenerator.AirportNotFound(id));
            }
            
            airport.Name = name;
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // DELETE: api/Airports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAirport(string id)
        {
            var airport = await _context.Airports
                //.Include(x => x.Flights) // TODO: FIX TO OPTIMIZE
                .FirstOrDefaultAsync(x => x.Id == id);

            if (airport == null)
            {
                return NotFound(ErrorMessageGenerator.AirportNotFound(id));
            }

            //var flights = airport.Flights;
            var flights = _context.Flights
                .Where(x => x.DestinationId.Equals(id) || x.DepartureId.Equals(id))
                .ToList();
            
            if (flights.Count > 0)
            {
                return Conflict(ErrorMessageGenerator.GetAirportHasFlightsError(id));
            }
            
            _context.Airports.Remove(airport);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AirportExists(string id)
        {
            return _context.Airports.Find(id) != null;
        }
        
    }
}
