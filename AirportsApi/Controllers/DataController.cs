using AirportsApi.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirportsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly AirportsContext _context;

        public DataController(AirportsContext context)
        {
            _context = context;
        }

        // DELETE: api/data
        [HttpDelete]
        public async Task<IActionResult> DeleteData()
        {
            var flights = await _context.Flights.Where(f => true).ToListAsync();
            var airports = await _context.Airports.Where(a => true).ToListAsync();

            _context.Flights.RemoveRange(flights);
            _context.Airports.RemoveRange(airports);

            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
