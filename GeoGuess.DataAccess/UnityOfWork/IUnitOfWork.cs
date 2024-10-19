using GeoGuess.Core.Repository;

namespace GeoGuess.Core.UnityOfWork;

public interface IUnitOfWork : IDisposable
{
    IBaseRepository<T> Repository<T>() where T : class;
    void ChangeTracking(bool changeTracking = false);
    int SaveChanges();
    Task<int> SaveChangesAsync(bool generateLog = true);
    public void DetachAllEntities();
}