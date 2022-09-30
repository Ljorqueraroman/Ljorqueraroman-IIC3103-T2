using AirportsApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirportsApi.Contexts
{
    public class AirportsContext : DbContext
    {
        public AirportsContext(DbContextOptions<AirportsContext> options) : base(options)
        {

        }

        public DbSet<Airport> Airports { get; set; }
        public DbSet<Flight> Flights { get; set; }

    }
}
