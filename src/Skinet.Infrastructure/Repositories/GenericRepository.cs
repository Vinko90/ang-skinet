using Microsoft.EntityFrameworkCore;
using Skinet.Core.Entities;
using Skinet.Core.Interfaces;
using Skinet.Core.Specifications;
using Skinet.Infrastructure.Data;

namespace Skinet.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly StoreContext _db;
    
    public GenericRepository(StoreContext db)
    {
        _db = db;
    }
    
    public async Task<T> GetByIdAsync(int id)
    {
        return await _db.Set<T>()
            .FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _db.Set<T>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<int> CountAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).CountAsync();
    }

    public void Add(T entity)
    {
        _db.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        _db.Set<T>().Attach(entity);
        _db.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        _db.Set<T>().Remove(entity);
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(_db.Set<T>().AsQueryable(), spec);
    }
}
