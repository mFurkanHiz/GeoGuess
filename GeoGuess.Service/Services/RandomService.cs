using GeoGuess.Model.ViewModels;
using GeoGuess.Service.Interfaces;

namespace GeoGuess.Service.Services;

public class RandomService : IRandomService
{
    public Pin GetRandomLocation()
    {

        Random random = new();

        double latitude = random.NextDouble() * 180 - 90;  // Between -90 and 90 degrees
        double longitude = random.NextDouble() * 360 - 180; // Between -180 and 180 degrees

        Pin pin = new()
        {
            Latitude = latitude,
            Longitude = longitude,
        };

        return pin;
    }

}
