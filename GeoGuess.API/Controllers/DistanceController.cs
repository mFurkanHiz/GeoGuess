using GeoGuess.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeoGuess.API.Controllers;

public class DistanceController : ControllerBase
{
    private readonly IDistanceService _distanceService;


    public DistanceController(IDistanceService distanceService)
    {
        _distanceService = distanceService;

    }

    /// <summary>
    /// Calculate the distance between 2 coordinates by using Haversine formula
    /// </summary>
    /// <param name="lat1"></param>
    /// <param name="long1"></param>
    /// <param name="lat2"></param>
    /// <param name="long2"></param>
    /// <returns>distance in Km</returns>
    [HttpGet("CalculateDistance")]
    public IActionResult CalculateDistance(double lat1, double long1, double lat2, double long2)
    {

        var distance = _distanceService.CalculateDistance(lat1, long1, lat2, long2);

        return Ok(distance);
    }

    /// <summary>
    /// Calculate the distance in radians
    /// </summary>
    /// <param name="degrees">Distance in km</param>
    /// <returns>Distance in radians</returns>
    [HttpGet("DegreesToRadians")]
    public IActionResult DegreesToRadians(double degrees)
    {

        var distance = _distanceService.DegreesToRadians(degrees);

        return Ok(distance);
    }

    /// <summary>
    /// Calculate the score by distance in km
    /// </summary>
    /// <param name="distanceKm"></param>
    /// <returns>score</returns>
    [HttpGet("CalculateScore")]
    public IActionResult CalculateScore(double distanceKm)
    {

        var score = _distanceService.CalculateScore(distanceKm);

        if (score < 0) BadRequest("Score is not calculated.");

        return Ok(score);
    }

}
