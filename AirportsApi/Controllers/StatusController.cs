using AirportsApi.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace AirportsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly AirportsContext _context;

        public StatusController(AirportsContext context)
        {
            _context = context;
        }

        // GET: api/Status
        [HttpGet]
        public async Task<IActionResult> GetStatus()
        {
            return NoContent();
        }

    }
}
