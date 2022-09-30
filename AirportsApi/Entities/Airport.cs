using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportsApi.Entities
{
    public class Airport
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [MinLength(3)]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }

        public ICollection<Flight> Flights;

    }
}
