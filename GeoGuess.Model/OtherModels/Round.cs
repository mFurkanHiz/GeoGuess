namespace GeoGuess.Model.OtherModels;

public class Round
{
    public int RoundNumber { get; set; }
    public string PanoId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string CountryCode { get; set; }
    public double Heading { get; set; }
    public double Pitch { get; set; }
}
