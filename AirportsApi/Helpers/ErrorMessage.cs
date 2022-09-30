namespace AirportsApi.Helpers;

public class ErrorMessage
{
    public string Error { get; set; }
    public ErrorMessage(string error)
    {
        Error = error;
    }
    
}
