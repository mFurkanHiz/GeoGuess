using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using GeoGuess.DataAccess.Context;

namespace GeoGuess.Core.Repository;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{

    // burada Contex i çekebilmek için Program.cs de AddDbContext içine yazmamız gerekir
    //Context _context;
    //private readonly GeoGuessContext context;

    protected readonly GeoGuessContext DbContext;
    protected readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    public BaseRepository(GeoGuessContext context)
    {
        //this.context = context;
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public virtual void ChangeState(T entity, EntityState state)
    {
        _context.Entry(entity).State = state;
    }

    public virtual void Attach(T entity)
    {
        _dbSet.Attach(entity);
    }

    public virtual void AttachRange(IEnumerable<T> list)
    {
        _dbSet.AttachRange(list);
    }

    public void Detach(T entity)
    {
        _context.Entry(entity).State = EntityState.Detached;
    }

    public virtual void DetachRange(IEnumerable<T> list)
    {
        foreach (var item in list) _context.Entry(item).State = EntityState.Detached;
    }

    public virtual EntityState GetState(T entity)
    {
        return _context.Entry(entity).State;
    }

    public virtual T FindById(int id)
    {
        var entity = _dbSet.Find(id);
        ArgumentNullException.ThrowIfNull(entity);
        return entity;
    }

    public virtual async Task<T> FindByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }
    public virtual IEnumerable<T> Find(Expression<Func<T, bool>> expression)
    {
        return _dbSet.Where(expression).AsNoTracking().ToList();
    }

    public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
    {
        return await _dbSet.Where(expression).AsNoTracking().ToListAsync();
    }

    public virtual IEnumerable<T> GetAll()
    {
        return _dbSet.AsNoTracking().ToList();
    }

    public List<T> List()
    {
        return _dbSet.ToList();
    }

    public virtual async Task<List<T>> ListAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual List<T> List(Expression<Func<T, bool>> expression)
    {
        return _dbSet.Where(expression).ToList();
    }

    public virtual async Task<List<T>> ListAsync(Expression<Func<T, bool>> expression)
    {
        return await _dbSet.Where(expression).ToListAsync();
    }

    public virtual int Count()
    {
        return _dbSet.Count();
    }

    public virtual int Count(Expression<Func<T, bool>> predicate)
    {
        return _dbSet.Count(predicate);
    }

    public virtual T SingleOrDefault(Expression<Func<T, bool>> expression)
    {
        return _dbSet.SingleOrDefault(expression);
    }

    public virtual bool Any(Expression<Func<T, bool>> expression)
    {
        return _dbSet.Any(expression);
    }

    public virtual IEnumerable<T> Paginate(int pageNumber, int pageSize)
    {
        return _dbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
    }

    public virtual void Add(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        _dbSet.Add(entity);
    }

    public virtual async Task AddAsync(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        await _dbSet.AddAsync(entity);
    }

    public virtual void AddRange(IEnumerable<T> list)
    {
        _dbSet.AddRange(list);
    }

    public virtual void HardDelete(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        _dbSet.Remove(entity);
    }

    public virtual void HardDelete(int id)
    {
        var entity = FindById(id);
        if (entity is null) return;
        HardDelete(entity);
    }

    public virtual void HardDeleteRange(List<int> ids)
    {
        var entities = _dbSet.Where(e => ids.Contains((int)typeof(T).GetProperty("Id").GetValue(e, null))).ToList();
        if (entities.Count > 0) _dbSet.RemoveRange(entities);
    }


    public virtual void SafeDelete(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        if (entity.GetType().GetProperty("IsDeleted") is null) return;
        var _entity = entity;
        _entity.GetType().GetProperty("IsDeleted").SetValue(_entity, true);
        Update(_entity);
    }

    public virtual void SafeDelete(int id)
    {
        var entity = FindById(id);
        if (entity is null) return;
        SafeDelete(entity);
    }

    public virtual void SafeDeleteRange(List<int> ids)
    {
        foreach (var id in ids)
        {
            var entity = FindById(id);
            if (entity is null) continue;
            SafeDelete(entity);
        }
    }

    public virtual void Update(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        _dbSet.Update(entity);
    }

    public virtual void UpdateRange(IEnumerable<T> list)
    {
        _dbSet.UpdateRange(list);
    }


    public virtual void AddOrUpdate(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        var exists = _dbSet.Any(e => e == entity);
        if (exists) Update(entity);
        else Add(entity);
    }

    public virtual void SaveChanges()
    {
        _context.SaveChanges();
    }

    public virtual async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public virtual async Task<IEnumerable<T>> IncludeAsync(params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = _dbSet.AsNoTracking();

        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        return await query.ToListAsync();
    }

}