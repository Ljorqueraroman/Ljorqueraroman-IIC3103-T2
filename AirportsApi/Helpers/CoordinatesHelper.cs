using System.Diagnostics.CodeAnalysis;
using AirportsApi.Model;

namespace AirportsApi.Helpers
{
    public static class CoordinatesHelper
    {

        private static double ToRadians(double angle)
        {
            return angle * Math.PI / 180;
        }

        private static double ToDegrees(double radians)
        {
            return radians * 180 / Math.PI;
        }

        #region Distance

        private const double EarthRadiusKms = 6371;
        private const double EarthRadiusMiles = 3956;

        [SuppressMessage("ReSharper", "PossibleInvalidOperationException")]
        public static double CalculateDistanceKms(Position position1, Position position2)
        {
            if (!position1.IsValid() || !position2.IsValid())
                return -1;

            return CalculateDistanceKms(
                position1.Lat.Value, position1.Long.Value,
                position2.Lat.Value, position2.Long.Value);
        }

        public static double CalculateDistanceKms(double latitude1, double longitude1, double latitude2, double longitude2)
        {
            var radiansDistance = CalculateDistanceRadians(latitude1, longitude1, latitude2, longitude2);

            // calculate the result
            return radiansDistance * EarthRadiusKms;

        }

        // Obtained from https://www.geeksforgeeks.org/program-distance-two-points-earth/
        private static double CalculateDistanceRadians(double latitude1, double longitude1, double latitude2, double longitude2)
        {
            var lat1 = ToRadians(latitude1);
            var lat2 = ToRadians(latitude2);
            var lon1 = ToRadians(longitude1);
            var lon2 = ToRadians(longitude2);

            // Haversine formula
            var dlon = lon2 - lon1;
            var dlat = lat2 - lat1;

            var a = Math.Pow(Math.Sin(dlat / 2), 2) +
                    Math.Cos(lat1) * Math.Cos(lat2) *
                    Math.Pow(Math.Sin(dlon / 2), 2);

            var c = 2 * Math.Asin(Math.Sqrt(a));

            return c;
        }

        #endregion

        #region Bearing

        [SuppressMessage("ReSharper", "PossibleInvalidOperationException")]
        public static double CalculateBearing(Position position1, Position position2)
        {
            if (!position1.IsValid() || !position2.IsValid())
                return -1;

            return AngleFromCoordinate(
                position1.Lat.Value, position1.Long.Value,
                position2.Lat.Value, position2.Long.Value);
        }

        // Obtained from https://stackoverflow.com/questions/3932502/calculate-angle-between-two-latitude-longitude-points
        private static double AngleFromCoordinate(double latitude1, double longitude1, double latitude2, double longitude2)
        {

            var lat1 = ToRadians(latitude1);
            var lat2 = ToRadians(latitude2);
            var lon1 = ToRadians(longitude1);
            var lon2 = ToRadians(longitude2);

            var dLon = (lon2 - lon1);

            var y = Math.Sin(dLon) * Math.Cos(lat2);
            var x = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1)
                * Math.Cos(lat2) * Math.Cos(dLon);

            var brng = Math.Atan2(y, x);

            brng = ToDegrees(brng);
            brng = (brng + 360) % 360;
            //brng = 360 - brng; // count degrees counter-clockwise - remove to make clockwise

            return brng;
        }

        #endregion

    }
}
