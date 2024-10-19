namespace GeoGuess.Model.ViewModels;

public class PanoramaViewModel
{
    //@{lat},{long},3a,{zoom}y,{heading}h,{pitch}t

    public int? Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int? PanoramaCode { get; set; } = 3;
    public int Zoom { get; set; }
    public double Heading { get; set; }
    public double Pitch { get; set; }
    public string? CountryCode { get; set; }
    public int? ViewToleracne { get; set; }
    public int? Viewed { get; set; }
    public string? Label { get; set; }

}
