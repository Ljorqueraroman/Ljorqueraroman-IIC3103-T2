using AirportsApi.Entities;
using AirportsApi.Model;

namespace AirportsApi.Helpers;

public static class AirportMapper
{

    public static AirportModelSmol ToSmol(Airport entity)
    {
        return new AirportModelSmol
        {
            Id = entity.Id,
            Name = entity.Name,
        };
    }

    public static AirportModelChonker ToChonker(Airport entity)
    {
        return new AirportModelChonker
        {
            Id = entity.Id,
            Name = entity.Name,
            Country = entity.Country,
            City = entity.City,
            Position = new Position
            {
                Lat = entity.Latitude,
                Long = entity.Longitude,
            }

        };
    }

    public static Airport ToEntity(AirportModelChonker chonker)
    {
        return new Airport
        {
            Id = chonker.Id,
            Name = chonker.Name,
            Country = chonker.Country,
            City = chonker.City,
            Latitude = chonker.Position.Lat.Value,
            Longitude = chonker.Position.Long.Value,
        };
    }

}