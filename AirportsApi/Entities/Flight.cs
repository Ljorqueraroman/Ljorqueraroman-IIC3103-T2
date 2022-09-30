using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportsApi.Entities
{
    public class Flight
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [MinLength(10)]
        public string Id { get; set; }

        [ForeignKey("Departure")]
        [Required]
        public string DepartureId { get; set; }
        public Airport Departure { get; set; }

        [ForeignKey("Destination")]
        [Required]
        public string DestinationId { get; set; }
        public Airport Destination { get; set; }


        public double TotalDistance { get; set; }
        public double TraveledDistance { get; set; }
        public double Bearing { get; set; }


        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
}
