namespace GeoGuess.Service.Interfaces;

public interface IDistanceService
{
    double CalculateDistance(double lat1, double lon1, double lat2, double lon2);
    double CalculateRadianDistance(double lat1, double lon1, double lat2, double lon2);
    double DegreesToRadians(double degrees);
    int CalculateScore(double distanceKm);
}
