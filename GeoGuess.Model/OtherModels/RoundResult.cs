namespace GeoGuess.Model.OtherModels;

public class RoundResult
{
    public int RoundNumber { get; set; }
    public int Score { get; set; }
    public int HealthBefore { get; set; }
    public int HealthAfter { get; set; }
    public Guess BestGuess { get; set; }
}
