using Microsoft.EntityFrameworkCore;
using GeoGuess.Core.Repository;
using Microsoft.AspNetCore.Mvc;
using GeoGuess.DataAccess.Context;

namespace GeoGuess.Core.UnityOfWork;

public class UnitOfWork : IUnitOfWork
{
    protected readonly GeoGuessContext _context;
    private bool disposed;

    public UnitOfWork(GeoGuessContext context)
    {
        _context = context;
    }

    public void ChangeTracking(bool changeTracking = false)
    {
        _context.ChangeTracker.AutoDetectChangesEnabled = changeTracking;
    }

    public void DetachAllEntities()
    {
        var changedEntriesCopy = _context.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified ||
                        e.State == EntityState.Deleted)
            .ToList();

        foreach (var entry in changedEntriesCopy) entry.State = EntityState.Detached;
    }




    public IBaseRepository<T> Repository<T>() where T : class
    {
        return new BaseRepository<T>(_context);
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    public async Task<int> SaveChangesAsync(bool generateLog = true)
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed && disposing)
        {
            _context.Dispose();
        }

        disposed = true;
    }
}