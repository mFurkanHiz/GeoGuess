using GeoGuess.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeoGuess.API.Controllers;

public class GameController : ControllerBase
{

    private readonly IStreetViewService _streetViewService;
    private readonly IGameService _gameService;
    public GameController(IStreetViewService streetViewService, IGameService gameService)
    {
        _streetViewService = streetViewService;
        _gameService = gameService;
    }

    /// <summary>
    /// Uses any random panorama to play for a player by player Id
    /// </summary>
    /// <param name="playerId">Player Id</param>
    /// <returns>Panorama</returns>
    /// <exception cref="Exception"></exception>
    [HttpPost("UseAvailablePanoramaByPlayerId/{playerId}")]
    public async Task<IActionResult> UseAvailablePanoramaByPlayerId(int playerId)
    {

        try
        {
            var panorama = await _gameService.UseAvailablePanoramaByPlayerId(playerId);

            if (panorama is null) return BadRequest("No available panorama found");

            return Ok(panorama);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}
