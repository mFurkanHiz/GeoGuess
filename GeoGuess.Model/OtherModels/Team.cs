namespace GeoGuess.Model.OtherModels;

public class Team
{
    public Guid TeamId { get; set; }
    public string Name { get; set; }
    public int Health { get; set; }
    public List<Player> Players { get; set; }
    public List<RoundResult> RoundResults { get; set; }
}
