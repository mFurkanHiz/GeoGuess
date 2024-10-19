using GeoGuess.Model.Entities;

namespace GeoGuess.Service.Interfaces;

public interface IPseudoPlayerService
{
    Task<PseudoPlayer> CreatePlayer(string username);
    Task<bool> IsPlayerExistById(int playerId);
    Task<List<PseudoPlayer>> GetAllPlayers();
    Task<PseudoPlayer> GetPlayerByName(string username);
    Task<PseudoPlayer> GetPlayerById(int id);

}
