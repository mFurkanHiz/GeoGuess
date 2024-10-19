using GeoGuess.Core.Repository;
using GeoGuess.Core.UnityOfWork;
using GeoGuess.DataAccess.Context;
using GeoGuess.Model.Entities;
using GeoGuess.Model.Lookups;
using GeoGuess.Model.ViewModels;
using GeoGuess.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace GeoGuess.Service.Services;

public class PanoramaService : IPanoramaService
{
    private readonly IDateTimeHandlerService _dateTimeHandlerService;
    private readonly IBaseRepository<Panorama> _repository;
    private readonly IUnitOfWork _uow;
    private readonly GeoGuessContext _context;
    public PanoramaService(
        IDateTimeHandlerService dateTimeHandlerService,
        IBaseRepository<Panorama> repository,
        IUnitOfWork uow,
        GeoGuessContext context
        )
    {
        _dateTimeHandlerService = dateTimeHandlerService;
        _repository = repository;
        _uow = uow;
        _context = context;
    }

    public async Task<Panorama> CreatePanorama(PanoramaViewModel newPanorama)
    {

        if (newPanorama is null) throw new Exception("Panorama couldn't be created due to null value.");

        Panorama panorama = new()
        {
            Latitude = newPanorama.Latitude,
            Longitude = newPanorama.Longitude,
            PanoramaCode = newPanorama.PanoramaCode,
            Zoom = newPanorama.Zoom,
            Heading = newPanorama.Heading,
            Pitch = newPanorama.Pitch,
            CountryCode = newPanorama.CountryCode,
            ViewToleracne = newPanorama.ViewToleracne ?? 2,
            Viewed = newPanorama.Viewed ?? 0,
            Label = newPanorama.Label,
        };

        _repository.Add(panorama);
        await _uow.SaveChangesAsync();

        return panorama;
    }

    public async Task<Panorama> UpdatePanorama(Panorama panorama)
    {

        if (panorama is null) throw new Exception("Panorama shouldn't be null.");

        _ = await GetPanoramaById(panorama.Id) ?? throw new Exception("Panorama is not exist.");

        panorama.DateUpdated = DateTime.Now;

        _repository.Update(panorama);
        await _uow.SaveChangesAsync();

        return panorama;
    }

    public async Task<Panorama> SafeDeletePanorama(int id, bool isDeleted)
    {

        var panorama = await GetPanoramaById(id) ?? throw new Exception("Panorama is not exist.");

        panorama.IsDeleted = isDeleted;

        _repository.Update(panorama);
        await _uow.SaveChangesAsync();

        return panorama;
    }

    public async Task<List<int>> SafeDeleteRangePanorama(List<int> ids, bool isDeleted)
    {

        List<int> deletedIds = [];

        foreach (int id in ids)
        {
            var panorama = await GetPanoramaById(id);

            if (panorama is null) continue;

            panorama.IsDeleted = isDeleted;

            _repository.Update(panorama);

            deletedIds.Add(id);
        }

        await _uow.SaveChangesAsync();

        return deletedIds;
    }

    public async Task<Panorama> SetActivityPanorama(int id, bool activity)
    {

        var panorama = await GetPanoramaById(id) ?? throw new Exception("Panorama is not exist.");

        panorama.IsActive = activity;

        _repository.Update(panorama);
        await _uow.SaveChangesAsync();

        return panorama;
    }

    public async Task<List<int>> SetActivityRangePanorama(List<int> ids, bool activity)
    {

        List<int> changedIds = [];

        foreach (int id in ids)
        {
            var panorama = await GetPanoramaById(id);

            if (panorama is null) continue;

            panorama.IsActive = activity;

            _repository.Update(panorama);

            changedIds.Add(id);
        }

        await _uow.SaveChangesAsync();

        return changedIds;
    }

    public async Task<List<Panorama>> GetAllPanoramas()
    {
        var panoramas = await _context.Panoramas
            .Where(x => !x.IsDeleted)
            .AsNoTracking()
            .ToListAsync();

        return panoramas;
    }

    public async Task<Panorama> GetPanoramaById(int id)
    {
        var panorama = await _context.Panoramas
            .Where(x => x.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return panorama;
    }

    public async Task<Panorama> GetPanoramaByCoordinates(double latitude, double longitude)
    {
        var panorama = await _context.Panoramas
            .Where(x => !x.IsDeleted)
            .Where(x => x.Latitude == latitude)
            .Where(x => x.Longitude == longitude)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return panorama;
    }

    public async Task<List<Panorama>> GetPanoramasByCountryCode(string countryCode)
    {
        var panoramas = await _context.Panoramas
            .Where(x => !x.IsDeleted)
            .Where(x => x.CountryCode == countryCode)
            .AsNoTracking()
            .ToListAsync();

        return panoramas;
    }

    public async Task<List<Panorama>> GetPanoramasByLabel(string label)
    {
        var panoramas = await _context.Panoramas
            .Where(x => !x.IsDeleted)
            .Where(x => x.Label == label)
            .AsNoTracking()
            .ToListAsync();

        return panoramas;
    }

    public async Task<List<Panorama>> PanoramaLookup(PanoramaLookup lookup)
    {
        var panoramasQuery = _context.Panoramas
            .AsNoTracking();

        if (lookup.Id.HasValue)
            return await panoramasQuery.Where(x => x.Id == lookup.Id).ToListAsync();

        if (lookup.Latitude.HasValue)
            panoramasQuery = panoramasQuery.Where(x => x.Latitude == lookup.Latitude);

        if (lookup.Longitude.HasValue)
            panoramasQuery = panoramasQuery.Where(x => x.Longitude == lookup.Longitude);

        if (lookup.MaxLatitude.HasValue)
            panoramasQuery = panoramasQuery.Where(x => x.Latitude <= lookup.MaxLatitude);

        if (lookup.MaxLongitude.HasValue)
            panoramasQuery = panoramasQuery.Where(x => x.Longitude <= lookup.MaxLongitude);

        if (lookup.MinLatitude.HasValue)
            panoramasQuery = panoramasQuery.Where(x => x.Latitude >= lookup.MinLatitude);

        if (lookup.MinLongitude.HasValue)
            panoramasQuery = panoramasQuery.Where(x => x.Longitude >= lookup.MinLongitude);

        if (lookup.PanoramaCode.HasValue)
            panoramasQuery = panoramasQuery.Where(x => x.PanoramaCode == lookup.PanoramaCode);

        if (lookup.Zoom.HasValue)
            panoramasQuery = panoramasQuery.Where(x => x.Zoom == lookup.Zoom);

        if (lookup.Heading.HasValue)
            panoramasQuery = panoramasQuery.Where(x => x.Heading == lookup.Heading);

        if (lookup.Pitch.HasValue)
            panoramasQuery = panoramasQuery.Where(x => x.Pitch == lookup.Pitch);

        if (!string.IsNullOrEmpty(lookup.CountryCode))
            panoramasQuery = panoramasQuery.Where(x => x.CountryCode == lookup.CountryCode);

        if (lookup.MaxViewToleracne.HasValue)
            panoramasQuery = panoramasQuery.Where(x => x.ViewToleracne <= lookup.MaxViewToleracne);

        if (lookup.MinViewToleracne.HasValue)
            panoramasQuery = panoramasQuery.Where(x => x.ViewToleracne >= lookup.MinViewToleracne);

        if (lookup.MaxViewed.HasValue)
            panoramasQuery = panoramasQuery.Where(x => x.Viewed <= lookup.MaxViewed);

        if (lookup.MinViewed.HasValue)
            panoramasQuery = panoramasQuery.Where(x => x.Viewed >= lookup.MinViewed);

        if (!string.IsNullOrEmpty(lookup.Label))
            panoramasQuery = panoramasQuery.Where(x => x.Label == lookup.Label);

        if (!string.IsNullOrEmpty(lookup.MaxDateCreated))
        {
            var maxDateCreated = _dateTimeHandlerService.StringToDateTime(lookup.MaxDateCreated);
            panoramasQuery = panoramasQuery.Where(x => x.DateCreated <= maxDateCreated);
        }

        if (!string.IsNullOrEmpty(lookup.MinDateCreated))
        {
            var minDateCreated = _dateTimeHandlerService.StringToDateTime(lookup.MinDateCreated);
            panoramasQuery = panoramasQuery.Where(x => x.DateCreated >= minDateCreated);
        }

        if (!string.IsNullOrEmpty(lookup.MaxDateUpdated))
        {
            var maxDateUpdated = _dateTimeHandlerService.StringToDateTime(lookup.MaxDateUpdated);
            panoramasQuery = panoramasQuery.Where(x => x.DateUpdated <= maxDateUpdated);
        }

        if (!string.IsNullOrEmpty(lookup.MinDateUpdated))
        {
            var minDateUpdated = _dateTimeHandlerService.StringToDateTime(lookup.MinDateUpdated);
            panoramasQuery = panoramasQuery.Where(x => x.DateUpdated >= minDateUpdated);
        }

        if (lookup.IsActive.HasValue)
            panoramasQuery = panoramasQuery.Where(x => x.IsActive == lookup.IsActive);

        if (lookup.IsDeleted.HasValue)
            panoramasQuery = panoramasQuery.Where(x => x.IsDeleted == lookup.IsDeleted);

        // ToList
        var panoramas = await panoramasQuery.ToListAsync();

        return panoramas;
    }

    public bool IsPanoramaDuplicated(PanoramaViewModel model, double? distance)
    {
        if (distance is null || distance is 0)
        {
            // Check for exact match when distance is null or 0
            var exactMatch = _context.Panoramas
                .Where(x => x.Latitude == model.Latitude)
                .Where(x => x.Longitude == model.Longitude)
                .AsNoTracking()
                .FirstOrDefault();

            return exactMatch is not null;
        }

        // Earth radius in meters
        const double EarthRadius = 6371000;

        // Convert distance to meters
        double radius = distance ?? 0;

        // Convert degrees to radians
        double degToRad(double degrees) => degrees * (Math.PI / 180.0);

        // Min/max latitude and longitude based on the provided distance
        var minLatitude = model.Latitude - (radius / EarthRadius) * (180 / Math.PI);
        var maxLatitude = model.Latitude + (radius / EarthRadius) * (180 / Math.PI);

        var minLongitude = model.Longitude - (radius / (EarthRadius * Math.Cos(degToRad(model.Latitude)))) * (180 / Math.PI);
        var maxLongitude = model.Longitude + (radius / (EarthRadius * Math.Cos(degToRad(model.Latitude)))) * (180 / Math.PI);

        var exist = _context.Panoramas
            .Where(x => x.Latitude >= minLatitude && x.Latitude <= maxLatitude)
            .Where(x => x.Longitude >= minLongitude && x.Longitude <= maxLongitude)
            .AsNoTracking()
            .FirstOrDefault();

        //if (exist is null) return false;

        return exist is not null;
    }

    public PanoramaViewModel ParseStreetViewUrl(string url)
    {

        //https://www.google.com/maps/@{lat},{long},3a,{zoom}y,{heading}h,{pitch}t

        try
        {
            int startIndex = url.IndexOf('@') + 1;
            int endIndex = url.IndexOf("/data");
            string coordinates = url.Substring(startIndex, endIndex - startIndex);

            string[] parts = coordinates.Split(',');

            if (parts.Length < 6) throw new Exception("Url couldn't split");

            string latitude = parts[0];
            string longitude = parts[1];
            string panoramaCode = parts[2][0].ToString();
            string zoom = parts[3][0..^1];
            string heading = parts[4][0..^1];
            string pitch = parts[5][0..^1];

            PanoramaViewModel panoramaViewModel = new()
            {
                Latitude = double.Parse(latitude, CultureInfo.InvariantCulture),
                Longitude = double.Parse(longitude, CultureInfo.InvariantCulture),
                PanoramaCode = int.Parse(panoramaCode),
                Zoom = int.Parse(zoom),
                Heading = double.Parse(heading, CultureInfo.InvariantCulture),
                Pitch = double.Parse(pitch, CultureInfo.InvariantCulture),
                CountryCode = string.Empty,
            };

            return panoramaViewModel;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


}
