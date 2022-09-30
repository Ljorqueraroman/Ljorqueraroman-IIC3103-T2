using AirportsApi.Entities;
using AirportsApi.Helpers;
using AirportsApi.Model;

namespace AirportsApi.Mapper;

public static class FlightMapper
{
    public static FlightModelSmol ToSmol(Flight entity)
    {
        return new FlightModelSmol
        {
            Id = entity.Id,
            Departure = entity.DepartureId,
            Destination = entity.DestinationId,
        };
    }

    public static FlightModelChonker ToChonker(Flight entity)
    {
        var currentPosition = new Position
        {
            Lat = entity.Latitude,
            Long = entity.Longitude,
        };

        var chonker = new FlightModelChonker
        {
            Id = entity.Id,
            Departure = new AirportModelSmol
            {
                Id = entity.Departure.Id,
                Name = entity.Departure.Name,
            },
            Destination = new AirportModelSmol
            {
                Id = entity.Destination.Id,
                Name = entity.Destination.Name,
            },
            TraveledDistance = entity.TraveledDistance,
            TotalDistance = entity.TotalDistance,
            Bearing = entity.Bearing,
            Position = currentPosition,
        };

        return chonker;
    }

    //TODO: Fill position data on service/controller
    public static Flight ToEntity(FlightModelSmol smol)
    {
        return new Flight
        {
            Id = smol.Id,
            DestinationId = smol.Destination,
            DepartureId = smol.Departure,
        };
    }
}