using GeoGuess.Model.ViewModels;

namespace GeoGuess.Service.Interfaces;

public interface IRandomService
{
    public Pin GetRandomLocation();
}
