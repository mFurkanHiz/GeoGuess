using GeoGuess.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeoGuess.API.Controllers;

public class PseudoPlayerController : Controller
{

    private readonly IPseudoPlayerService _playerService;

    public PseudoPlayerController(IPseudoPlayerService playerService)
    {
        _playerService = playerService;
    }

    /// <summary>
    /// Get all players
    /// </summary>
    /// <returns>Players List</returns>
    [HttpGet("GetAllPlayers")]
    public async Task<IActionResult> GetAllPlayers()
    {

        var allPlayers = await _playerService.GetAllPlayers();

        return Ok(allPlayers);
    }

    /// <summary>
    /// Gets player by its name
    /// </summary>
    /// <param name="username"></param>
    /// <returns>Player</returns>
    [HttpGet("GetPlayerByName")]
    public async Task<IActionResult> GetPlayerByName(string username)
    {

        var player = await _playerService.GetPlayerByName(username);

        return Ok(player);
    }

    /// <summary>
    /// Get player by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Player</returns>
    [HttpGet("GetPlayerById/{id}")]
    public async Task<IActionResult> GetPlayerById(int id)
    {

        var player = await _playerService.GetPlayerById(id);

        return Ok(player);
    }

    /// <summary>
    /// Creates player
    /// </summary>
    /// <param name="username"></param>
    /// <returns>PsuedoPlayer</returns>
    /// <exception cref="Exception"></exception>
    [HttpPost("AddPlayer")]
    public async Task<IActionResult> RegisterPanorama([FromBody] string username)
    {

        try
        {
            var newPlayer = await _playerService.CreatePlayer(username);

            if (newPlayer is null) return BadRequest("Player couldn't be added");

            return Ok(newPlayer);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Checks if player exists
    /// </summary>
    /// <param name="playerId"></param>
    /// <returns>bool, message as a string</returns>
    /// <exception cref="Exception"></exception>
    [HttpGet("IsPlayerExistById/{playerId}")]
    public async Task<IActionResult> IsPlayerExistById(int playerId)
    {

        try
        {
            var isPlayerExist = await _playerService.IsPlayerExistById(playerId);

            return Ok(new { 
                Result = isPlayerExist, 
                Message = isPlayerExist ? $"Player ({playerId}) exists." : "Player does not exist." } 
            );
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}
