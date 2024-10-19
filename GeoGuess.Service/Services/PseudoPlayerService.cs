using GeoGuess.Core.Repository;
using GeoGuess.Core.UnityOfWork;
using GeoGuess.DataAccess.Context;
using GeoGuess.Model.Entities;
using GeoGuess.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeoGuess.Service.Services;

public class PseudoPlayerService : IPseudoPlayerService
{

    private readonly IBaseRepository<PseudoPlayer> _repository;
    private readonly IUnitOfWork _uow;
    private readonly GeoGuessContext _context;

    public PseudoPlayerService(
        IBaseRepository<PseudoPlayer> repository,
        IUnitOfWork uow,
        GeoGuessContext context
        )
    {
        _repository = repository;
        _uow = uow;
        _context = context;
    }

    public async Task<PseudoPlayer> CreatePlayer(string username)
    {

        if (string.IsNullOrEmpty(username)) throw new Exception("Username cannot be empty.");

        PseudoPlayer newPlayer = new()
        {
            Name = username,
        };

        _repository.Add(newPlayer);
        await _uow.SaveChangesAsync();

        return newPlayer;
    }

    public async Task<bool> IsPlayerExistById(int playerId)
    {

        if (playerId is 0) return false;

        var isPlayerExist = await _context.PseudoPlayers
            .AsNoTracking()
            .AnyAsync(x => x.Id == playerId);

        return isPlayerExist;
    }

    public async Task<List<PseudoPlayer>> GetAllPlayers()
    {

        var players = await _context.PseudoPlayers
            .Where(x => !x.IsDeleted)
            .AsNoTracking()
            .ToListAsync();

        return players;
    }

    public async Task<PseudoPlayer> GetPlayerByName(string username)
    {

        if (string.IsNullOrEmpty(username)) throw new Exception("Username cannot be empty.");

        var player = await _context.PseudoPlayers
            .Where(x => x.Name == username)
            .AsNoTracking()
            .FirstOrDefaultAsync() ??
            throw new Exception("Player is not found.");

        return player;
    }

    public async Task<PseudoPlayer> GetPlayerById(int id)
    {

        var player = await _context.PseudoPlayers
            .Where(x => x.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync() ??
            throw new Exception("Player is not found.");

        return player;
    }


}
