using GeoGuess.Model.DTOs;
using GeoGuess.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class StreetViewController : ControllerBase
{
    private readonly IStreetViewService _streetViewService;
    private readonly IPanoramaService _panoramaService;
    private readonly IRandomService _randomService;

    public StreetViewController
        (IStreetViewService streetViewService, 
        IPanoramaService panoramaService,
        IRandomService randomService)
    {
        _streetViewService = streetViewService;
        _panoramaService = panoramaService;
        _randomService = randomService;
    }

    /// <summary>
    /// Gets random coordinates
    /// </summary>
    /// <returns></returns>
    [HttpGet("RandomCoordinates")]
    public IActionResult GetRandomCoordinates()
    {

        var pin = _randomService.GetRandomLocation();

        return Ok(pin);
    }

    /// <summary>
    /// Get picture of any street view point by coordinates
    /// </summary>
    /// <param name="latitude"></param>
    /// <param name="longitude"></param>
    /// <returns>URL as a string</returns>
    [HttpGet("GetStreetViewByCoordinates")]
    public async Task<IActionResult> GetStreetView(double latitude, double longitude)
    {

        var streetViewUrl = await _streetViewService.GetStreetView(latitude, longitude);

        if (streetViewUrl is null) return NotFound("Street View not available for this location.");
        return Ok(streetViewUrl);
    }

    /// <summary>
    /// Get picture of any street view location. has very low probability to catch any available street view point
    /// </summary>
    /// <returns>Coordinates, URL as a string</returns>
    [HttpGet("RandomLocation")]
    public async Task<IActionResult> GetRandomStreetView()
    {

        var pin = _randomService.GetRandomLocation();
        var streetViewUrl = await _streetViewService.GetStreetView(pin.Latitude, pin.Longitude);

        if (streetViewUrl is null) return NotFound("Street View not available for this location.");
        return Ok(new { pin, streetViewUrl });
    }

    /// <summary>
    /// Easily register any panorama into the database by using Chrome Extension. Panorama duplicated distance default value is 0.01
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Added Panorama</returns>
    /// <exception cref="Exception"></exception>
    [HttpPost("RegisterPanorama")]
    public async Task<IActionResult> RegisterPanorama([FromBody] NewPanoramaRequest request)
    {

        //https://www.google.com/maps/@{lat},{long},3a,{zoom}y,{heading}h,{pitch}t

        try
        {
            // Parse Url
            var panoramaViewModel = _panoramaService.ParseStreetViewUrl(request.Url);

            // Check does this panorama exist. 
            if (_panoramaService.IsPanoramaDuplicated(panoramaViewModel, 0.01)) return BadRequest("Panorama exists");

            // Assign view tolerance and label values
            panoramaViewModel.ViewToleracne = request.ViewTolerance;
            panoramaViewModel.Label = request.Label;

            // Add CountryCode
            panoramaViewModel.CountryCode = await _streetViewService.GetCountryCodeByCoordinates(panoramaViewModel.Latitude, panoramaViewModel.Longitude);

            // Create new Panorama
            var newPanorama = await _panoramaService.CreatePanorama(panoramaViewModel);

            if (newPanorama is null) return BadRequest("Panorama couldn't be added");

            return Ok(newPanorama);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}
