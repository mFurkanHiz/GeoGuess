using GeoGuess.Model.ViewModels;

namespace GeoGuess.Model.OtherModels;

public class Player
{
    public string PlayerId { get; set; }
    public int Rank { get; set; }
    public int Rating { get; set; }
    public string CountryCode { get; set; }
    public List<Guess> Guesses { get; set; }
    public Pin Pin { get; set; }
}
