using System.ComponentModel.DataAnnotations;

namespace AirportsApi.Model;

public class AirportModelChonker
{
    [MinLength(3)]
    public string Id { get; set; }

    public string Name { get; set; }
    public string Country { get; set; }
    public string City { get; set; }

    public Position Position { get; set; }


}