using GeoGuess.Service.Interfaces;

namespace GeoGuess.Service.Services;

public class DistanceService : IDistanceService
{
    // The radius of the Earth in km
    private const double EarthRadiusKm = 6371.0;

    // The method that calculates the distance of 2 coordinates by Haversine formula
    public double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        // Dereceyi radiana çevir
        double dLat = DegreesToRadians(lat2 - lat1);
        double dLon = DegreesToRadians(lon2 - lon1);

        // Latitude ve Longitude'ları radian olarak hesapla
        lat1 = DegreesToRadians(lat1);
        lat2 = DegreesToRadians(lat2);

        // Haversine formülü
        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                   Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        // Mesafe hesaplama
        return EarthRadiusKm * c;
    }

    public double CalculateRadianDistance(double lat1, double lon1, double lat2, double lon2)
    {
        // Dereceyi radiana çevir
        double dLat = DegreesToRadians(lat2 - lat1);
        double dLon = DegreesToRadians(lon2 - lon1);

        // Latitude ve Longitude'ları radian olarak hesapla
        lat1 = DegreesToRadians(lat1);
        lat2 = DegreesToRadians(lat2);

        // Haversine formülü
        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                   Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return c;
    }

    // Convert degree to radians
    public double DegreesToRadians(double degrees)
    {
        return degrees * Math.PI / 180.0;
    }

    public int CalculateScore(double distanceKm)
    {

        // y ~= 5000*e^(-0.0008*x)

        //double initialValue = 4999.6;
        //double exponent = -8E-04 * x;

        double initialValue = 5000;
        //double exponent = -0.0008 * distanceKm;
        double exponent = -0.00075124 * distanceKm;
        double exponential = Math.Exp(exponent);

        double preResult = initialValue * exponential;
        int result = (int)Math.Round(preResult);

        bool resultCondition = result >= 0 && result <= 5000;

        if (!resultCondition) return -1;

        return result;
    }

}
