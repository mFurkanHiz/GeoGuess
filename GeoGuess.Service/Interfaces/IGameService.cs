using GeoGuess.Model.Entities;

namespace GeoGuess.Service.Interfaces;

public interface IGameService
{
    Task<Panorama> UseAvailablePanoramaByPlayerId(int playerId);

    List<int> GetAvailablePanoramasByPlayerıd(int playerId);
    Task<int> AssignRandomPanoramaToPlayer(List<int> availablePanoramaIds, int playerId);
    Task<Panorama> UsePanorama(int panoramaId);

}
