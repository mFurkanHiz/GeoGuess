using GeoGuess.Model.Entities;
using GeoGuess.Model.Lookups;
using GeoGuess.Model.ViewModels;
using GeoGuess.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeoGuess.API.Controllers;

public class PanoramaController : ControllerBase
{

    private readonly IPanoramaService _panoramaService;

    public PanoramaController(IPanoramaService panoramaService)
    {
        _panoramaService = panoramaService;
    }

    /// <summary>
    /// Lookup for panorama by using any parameter available
    /// </summary>
    /// <param name="lookup"></param>
    /// <returns>Panoramas List</returns>
    /// <exception cref="Exception"></exception>
    [HttpPost("PanoramaLookup")]
    public async Task<IActionResult> PanoramaLookup(PanoramaLookup lookup)
    {

        try
        {
            var panoramas = await _panoramaService.PanoramaLookup(lookup);

            if (panoramas is null) return BadRequest("Panorama is not found");

            return Ok(panoramas);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Get all panoramas
    /// </summary>
    /// <returns>Panoramas List</returns>
    /// <exception cref="Exception"></exception>
    [HttpGet("GetAllPanoramas")]
    public async Task<IActionResult> GetAllPanoramas()
    {

        try
        {
            var panoramas = await _panoramaService.GetAllPanoramas();

            if (panoramas is null) return BadRequest("Panorama is not found");

            return Ok(panoramas);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Get panorama by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Panorama</returns>
    /// <exception cref="Exception"></exception>
    [HttpGet("GetPanoramaById/{id}")]
    public async Task<IActionResult> GetPanoramaById(int id)
    {

        try
        {
            var panorama = await _panoramaService.GetPanoramaById(id);

            if (panorama is null) return BadRequest("Panorama is not found");

            return Ok(panorama);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Get panorama by coordinates
    /// </summary>
    /// <param name="latitude"></param>
    /// <param name="longitude"></param>
    /// <returns>Panorama</returns>
    /// <exception cref="Exception"></exception>
    [HttpGet("GetPanoramaByCoordinates")]
    public async Task<IActionResult> GetPanoramaByCoordinates(double latitude, double longitude)
    {

        try
        {
            var panorama = await _panoramaService.GetPanoramaByCoordinates(latitude, longitude);

            if (panorama is null) return BadRequest("Panorama is not found");

            return Ok(panorama);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Get panoramas by country code
    /// </summary>
    /// <param name="countryCode"></param>
    /// <returns>Panoramas List</returns>
    /// <exception cref="Exception"></exception>
    [HttpGet("GetPanoramasByCountryCode/{countryCode}")]
    public async Task<IActionResult> GetPanoramasByCountryCode(string countryCode)
    {

        try
        {
            var panoramas = await _panoramaService.GetPanoramasByCountryCode(countryCode);

            if (panoramas is null) return BadRequest("Panorama is not found");

            return Ok(panoramas);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Get panoramas by labels
    /// </summary>
    /// <param name="label"></param>
    /// <returns>Panoramas List</returns>
    /// <exception cref="Exception"></exception>
    [HttpGet("GetPanoramasByLabel/{label}")]
    public async Task<IActionResult> GetPanoramasByLabel(string label)
    {

        try
        {
            var panoramas = await _panoramaService.GetPanoramasByLabel(label);

            if (panoramas is null) return BadRequest("Panorama is not found");

            return Ok(panoramas);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Checks if the panorama is exist in designated distance. Distance must be null or 0 in order to disable the distance check
    /// </summary>
    /// <param name="model"></param>
    /// <param name="distance">Distance in km (nullable)</param>
    /// <returns>bool, message as a string</returns>
    /// <exception cref="Exception"></exception>
    [HttpPost("IsPanoramaDuplicated")]
    public async Task<IActionResult> IsPanoramaDuplicated([FromBody] PanoramaViewModel model, double? distance)
    {

        try
        {
            var panorama = _panoramaService.IsPanoramaDuplicated(model, distance);

            string message = panorama ? "Panorama is exist nearby" : "Panorama is not exist nearby";

            return Ok(new { panorama, message });
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Creates panorama
    /// </summary>
    /// <param name="newPanorama"></param>
    /// <returns>Added panorama</returns>
    /// <exception cref="Exception"></exception>
    [HttpPost("Create")]
    public async Task<IActionResult> CreatePanorama([FromBody] PanoramaViewModel newPanorama)
    {

        try
        {
            var createdPanorama = await _panoramaService.CreatePanorama(newPanorama);

            return Ok(createdPanorama);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Update panorama
    /// </summary>
    /// <param name="panorama"></param>
    /// <returns>panorama</returns>
    /// <exception cref="Exception"></exception>
    [HttpPut("Update")]
    public async Task<IActionResult> UpdatePanorama([FromBody] Panorama panorama)
    {

        try
        {
            var updatedPanorama = await _panoramaService.UpdatePanorama(panorama);

            return Ok(updatedPanorama);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Set activity of any panorama
    /// </summary>
    /// <param name="id">Id</param>
    /// <param name="activity">IsActive</param>
    /// <returns>panorama</returns>
    /// <exception cref="Exception"></exception>
    [HttpPut("SetActivity/{id}/{activity}")]
    public async Task<IActionResult> SetActivityPanorama(int id, bool activity)
    {

        try
        {
            var updatedPanorama = await _panoramaService.SetActivityPanorama(id, activity);

            return Ok(updatedPanorama);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Set activity of panoramas
    /// </summary>
    /// <param name="ids">Id List</param>
    /// <param name="activity">IsActive</param>
    /// <returns>List of changed panorama Ids</returns>
    /// <exception cref="Exception"></exception>
    [HttpPut("SetActivityRange")]
    public async Task<IActionResult> SetActivityRangePanorama(List<int> ids, bool activity)
    {

        try
        {
            var updatedPanoramas = await _panoramaService.SetActivityRangePanorama(ids, activity);

            return Ok(updatedPanoramas);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Safely deletes panorama. Still stays in the database 
    /// </summary>
    /// <param name="id">Id</param>
    /// <param name="isDeleted">IsDeleted</param>
    /// <returns>Panorama</returns>
    /// <exception cref="Exception"></exception>
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> SafeDeletePanorama(int id, bool isDeleted)
    {

        try
        {
            var deletedPanorama = await _panoramaService.SafeDeletePanorama(id, isDeleted);

            return Ok(deletedPanorama);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Safely deletes panoramas. Still stays in the database 
    /// </summary>
    /// <param name="ids">Id List</param>
    /// <param name="isDeleted">IsDeleted</param>
    /// <returns>List of changed panorama Ids</returns>
    /// <exception cref="Exception"></exception>
    [HttpDelete("DeleteRange")]
    public async Task<IActionResult> SafeDeleteRangePanorama(List<int> ids, bool isDeleted)
    {

        try
        {
            var deletedPanoramaIds = await _panoramaService.SafeDeleteRangePanorama(ids, isDeleted);

            return Ok(deletedPanoramaIds);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


}
