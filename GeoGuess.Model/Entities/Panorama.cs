namespace GeoGuess.Model.Entities;

public class Panorama : BaseEntity
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int? PanoramaCode { get; set; } = 3;
    public int Zoom { get; set; }
    public double Heading { get; set; }
    public double Pitch { get; set; }
    public string? CountryCode { get; set; }
    public int? ViewToleracne { get; set; } = 2;
    public int Viewed { get; set; } = 0;
    public string? Label { get; set; }
}
