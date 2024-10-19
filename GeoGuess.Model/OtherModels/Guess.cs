namespace GeoGuess.Model.OtherModels;

public class Guess
{
    public int RoundNumber { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Distance { get; set; }
    public DateTime Created { get; set; }
    public bool IsTeamsBestGuess { get; set; }
    public int? Score { get; set; }
}
