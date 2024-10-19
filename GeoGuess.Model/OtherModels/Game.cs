namespace GeoGuess.Model.OtherModels;

public class Game
{
    public Guid GameId { get; set; }
    public List<Team> Teams { get; set; }
    public List<Round> Rounds { get; set; }
}
