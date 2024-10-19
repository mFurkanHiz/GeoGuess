using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GeoGuess.Core.Repository;

public interface IBaseRepository<T> where T : class
{
    void ChangeState(T entity, EntityState state);
    void Attach(T entity);
    void AttachRange(IEnumerable<T> list);
    void Detach(T entity);
    void DetachRange(IEnumerable<T> list);
    EntityState GetState(T entity);
    T FindById(int id);
    Task<T> FindByIdAsync(int id);
    IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
    IEnumerable<T> GetAll();
    List<T> List();
    Task<List<T>> ListAsync();
    List<T> List(Expression<Func<T, bool>> expression);
    Task<List<T>> ListAsync(Expression<Func<T, bool>> expression);
    int Count();
    int Count(Expression<Func<T, bool>> predicate);
    T SingleOrDefault(Expression<Func<T, bool>> expression);
    bool Any(Expression<Func<T, bool>> expression);
    IEnumerable<T> Paginate(int pageNumber, int pageSize);
    void Add(T entity);
    Task AddAsync(T entity);
    void AddRange(IEnumerable<T> list);
    void HardDelete(T entity);
    void HardDelete(int id);
    void HardDeleteRange(List<int> ids);
    void SafeDelete(T entity);
    void SafeDelete(int id);
    void SafeDeleteRange(List<int> ids);
    void Update(T entity);
    void UpdateRange(IEnumerable<T> list);
    void AddOrUpdate(T entity);
    void SaveChanges();
    Task SaveChangesAsync();
    Task<IEnumerable<T>> IncludeAsync(params Expression<Func<T, object>>[] includeProperties);


}