namespace GeoGuess.Service.Interfaces;

public interface IStreetViewService
{
    Task<string> GetStreetView(double latitude, double longitude);
    Task<string> GetCountryCodeByCoordinates(double latitude, double longitude);
}
