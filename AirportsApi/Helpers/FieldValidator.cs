using AirportsApi.Entities;
using AirportsApi.Model;

namespace AirportsApi.Helpers;

public static class FieldValidator
{

    public const string MissingParameterMessage = "missing parameter: {0}";
    public const string InsufficientLengthMessage = "{0} must be at least {1} characters long";
    public const string InvalidValueMessage = "{0} must be between {1} and {2}";
    
    public static List<string> GetFieldErrors(AirportModelChonker airport)
    {
        var errorMessages = new List<string>();

        if (airport.Id == null)
        {
            errorMessages.Add(string.Format(MissingParameterMessage, "id"));
        }
        else if (airport.Id.Length < 3)
        {
            errorMessages.Add(string.Format(InsufficientLengthMessage, "id", "3"));
        }

        if (airport.Name == null)
        {
            errorMessages.Add(string.Format(MissingParameterMessage, "name"));
        }
        if (airport.Country == null)
        {
            errorMessages.Add(string.Format(MissingParameterMessage, "country"));
        }
        if (airport.City == null)
        {
            errorMessages.Add(string.Format(MissingParameterMessage, "city"));
        }

        var positionErrors = GetFieldErrors(airport.Position);

        errorMessages = errorMessages.Concat(positionErrors).ToList();

        return errorMessages;
    }

    public static List<string> GetFieldErrors(FlightModelSmol flight)
    {
        var errorMessages = new List<string>();

        if (flight.Id == null)
        {
            errorMessages.Add(string.Format(MissingParameterMessage, "id"));
        }
        else if (flight.Id.Length < 10)
        {
            errorMessages.Add(string.Format(InsufficientLengthMessage, "id", "10"));
        }
        if (flight.Departure == null)
        {
            errorMessages.Add(string.Format(MissingParameterMessage, "departure"));
        }
        if (flight.Destination == null)
        {
            errorMessages.Add(string.Format(MissingParameterMessage, "destination"));
        }

        return errorMessages;
    }

    public static List<string> GetFieldErrors(Position position)
    {
        var errorMessages = new List<string>();
        if (position == null)
        {
            errorMessages.Add(string.Format(MissingParameterMessage, "Position"));
        }
        else
        {
            if (position.Lat == null)
            {
                errorMessages.Add(string.Format(MissingParameterMessage, "Lat"));
            }
            else if (position.Lat is < -90 or > 90)
            {
                errorMessages.Add(string.Format(InvalidValueMessage, "Lat", "-90", "90"));
            }

            if (position.Long == null)
            {
                errorMessages.Add(string.Format(MissingParameterMessage, "Long"));
            }
            else if (position.Long is < -180 or > 180)
            {
                errorMessages.Add(string.Format(InvalidValueMessage, "Long", "-180", "180"));
            }
        }
        
        return errorMessages;
    }

}
