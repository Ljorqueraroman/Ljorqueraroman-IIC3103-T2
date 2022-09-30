using Newtonsoft.Json;

namespace AirportsApi.Model
{
    public class Position
    {
        
        public double? Lat { get; set; }
        
        public double? Long { get; set; }

        public bool IsValid()
        {
            return Lat != null && Long != null;
        }

    }
}
