using AirportsApi.Model;

namespace AirportsApi.Helpers;

public static class ErrorMessageGenerator
{
    
    // Entities
    public const string EntityAlreadyExists = "{0} with id {1} already exists";
    public const string EntityNotFound = "{0} with id {1} not found";

    public const string AirportHasFlightsMessage = "Airport {0} has flights in progress";

    #region 400 Bad Request

    public static ErrorMessage InvalidFields(List<string> validationErrors)
    {
        return validationErrors.Count > 0 ? new ErrorMessage(validationErrors.First()) : null;
    }
    
    #endregion

    #region 404 Not Found

    public static ErrorMessage AirportNotFound(string id)
    {
        return new ErrorMessage(string.Format(EntityNotFound, "Airport", id));
    }

    public static ErrorMessage FlightNotFound(string id)
    {
        return new ErrorMessage(string.Format(EntityNotFound, "Flight", id));
    }

    #endregion

    #region 409 Conflict

    public static ErrorMessage AirportAlreadyExistsError(string id)
    {
        return new ErrorMessage(string.Format(EntityAlreadyExists, "Airport", id));
    }

    public static ErrorMessage FlightAlreadyExistsError(string id)
    {
        return new ErrorMessage(string.Format(EntityAlreadyExists, "Flight", id));
    }

    public static string GetAirportHasFlightsError(string id)
    {
        return string.Format(AirportHasFlightsMessage, id);
    }

    #endregion
    
}