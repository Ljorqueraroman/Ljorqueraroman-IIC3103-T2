using AirportsApi.Entities;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportsApi.Model;

public class FlightModelChonker
{

    public string Id { get; set; }
    
    public AirportModelSmol Departure { get; set; }
    
    public AirportModelSmol Destination { get; set; }

    [JsonProperty("total_distance")]
    public double TotalDistance { get; set; }

    [JsonProperty("total_distance")]
    public double TraveledDistance { get; set; }

    public double Bearing { get; set; }

    public Position Position { get; set; }

}