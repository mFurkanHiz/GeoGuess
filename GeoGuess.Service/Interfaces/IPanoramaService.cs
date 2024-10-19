using GeoGuess.Model.Entities;
using GeoGuess.Model.Lookups;
using GeoGuess.Model.ViewModels;

namespace GeoGuess.Service.Interfaces;

public interface IPanoramaService
{
    Task<Panorama> CreatePanorama(PanoramaViewModel newPanorama);
    Task<Panorama> UpdatePanorama(Panorama panorama);
    Task<Panorama> SafeDeletePanorama(int id, bool isDeleted);
    Task<List<int>> SafeDeleteRangePanorama(List<int> ids, bool isDeleted);
    Task<Panorama> SetActivityPanorama(int id, bool activity);
    Task<List<int>> SetActivityRangePanorama(List<int> ids, bool activity);
    Task<List<Panorama>> GetAllPanoramas();
    Task<Panorama> GetPanoramaById(int id);
    Task<Panorama> GetPanoramaByCoordinates(double latitude, double longitude);
    Task<List<Panorama>> GetPanoramasByCountryCode(string countryCode);
    Task<List<Panorama>> GetPanoramasByLabel(string label);
    Task<List<Panorama>> PanoramaLookup(PanoramaLookup lookup);
    bool IsPanoramaDuplicated(PanoramaViewModel model, double? distance);
    PanoramaViewModel ParseStreetViewUrl(string url);
}
