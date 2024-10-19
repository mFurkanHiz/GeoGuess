namespace GeoGuess.Model.Lookups;

public class PanoramaLookup
{
    public int? Id { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public double? MaxLatitude { get; set; }
    public double? MaxLongitude { get; set; }
    public double? MinLatitude { get; set; }
    public double? MinLongitude { get; set; }
    public int? PanoramaCode { get; set; }
    public int? Zoom { get; set; }
    public double? Heading { get; set; }
    public double? Pitch { get; set; }
    public string? CountryCode { get; set; }
    public int? MaxViewToleracne { get; set; }
    public int? MinViewToleracne { get; set; }
    public int? MaxViewed { get; set; }
    public int? MinViewed { get; set; }
    public string? Label { get; set; }
    public string? MaxDateCreated { get; set; } // Expected format: "dd/MM/yyyy HH:mm"
    public string? MinDateCreated { get; set; } // Expected format: "dd/MM/yyyy HH:mm"
    public string? MaxDateUpdated { get; set; } // Expected format: "dd/MM/yyyy HH:mm"
    public string? MinDateUpdated { get; set; } // Expected format: "dd/MM/yyyy HH:mm"
    public bool? IsActive { get; set; }
    public bool? IsDeleted { get; set; }
}
