using GeoGuess.Core.Repository;
using GeoGuess.Core.UnityOfWork;
using GeoGuess.DataAccess.Context;
using GeoGuess.Model.Entities;
using GeoGuess.Service.Interfaces;

namespace GeoGuess.Service.Services;

public class GameService : IGameService
{

    private readonly IPanoramaService _panoramaService;
    private readonly IPseudoPlayerService _playerService;
    private readonly IBaseRepository<PlayerPanorama> _repositoryPlayerPanorama;
    private readonly IUnitOfWork _uow;
    private readonly GeoGuessContext _context;

    public GameService(
        IPanoramaService panoramaService,
        IPseudoPlayerService playerService,
        IBaseRepository<PlayerPanorama> repositoryPlayerPanorama,
        IUnitOfWork uow,
        GeoGuessContext context
        )
    {
        _panoramaService = panoramaService;
        _playerService = playerService;
        _repositoryPlayerPanorama = repositoryPlayerPanorama;
        _uow = uow;
        _context = context;
    }

    public async Task<Panorama> UseAvailablePanoramaByPlayerId(int playerId)
    {

        Console.WriteLine(_context.ChangeTracker.DebugView.ShortView);//#debug

        // Check does player exist. (Optional)
        var isPlayerExist = await _playerService.IsPlayerExistById(playerId);

        if (!isPlayerExist) throw new Exception("Player is not exist");

        // Get available panoramas (Id List) which is considered by ViewTolerance value of the panoramas
        var availablePanoramaIds = GetAvailablePanoramasByPlayerıd(playerId);

        // Pick any available panorama (Id) and insert it into PlayerPanoramas table
        var panoramaId = await AssignRandomPanoramaToPlayer(availablePanoramaIds, playerId);

        // Update view value of that panorama, and get the panorama
        var panorama = await UsePanorama(panoramaId);

        return panorama;
    }

    public List<int> GetAvailablePanoramasByPlayerıd(int playerId)
    {

        try
        {

            var availablePanoramaIds = _context.Panoramas
                .Where(p =>
                _context.PlayerPanoramas.Count(pp => pp.PlayerId == playerId && pp.PanoramaId == p.Id) < p.ViewToleracne)
                .Select(x => x.Id)
                .ToList();

            if (availablePanoramaIds.Count is 0) throw new Exception($"No available panoramas left for Player {playerId}");

            return availablePanoramaIds;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<int> AssignRandomPanoramaToPlayer(List<int> availablePanoramaIds, int playerId)
    {

        try
        {
            var panoramaId = availablePanoramaIds[new Random().Next(availablePanoramaIds.Count)];

            PlayerPanorama newPlayerPanorama = new()
            {
                PlayerId = playerId,
                PanoramaId = panoramaId
            };

            _repositoryPlayerPanorama.Add(newPlayerPanorama);
            //_context.PlayerPanoramas.Add(newPlayerPanorama);
            //_uow.Repository<PlayerPanorama>().Add(newPlayerPanorama);
            await _uow.SaveChangesAsync();

            return panoramaId;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Panorama> UsePanorama(int panoramaId)
    {

        try
        {
            var panorama = await _panoramaService.GetPanoramaById(panoramaId);
            panorama.Viewed++;
            await _panoramaService.UpdatePanorama(panorama);

            return panorama;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}
