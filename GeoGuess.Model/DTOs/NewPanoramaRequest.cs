namespace GeoGuess.Model.DTOs;

public class NewPanoramaRequest
{
    public string Url { get; set; }
    public int? ViewTolerance { get; set; }
    public string? Label { get; set; }
}
