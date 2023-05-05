using System.Collections;
using Skinet.Core.Entities;
using Skinet.Core.Interfaces;
using Skinet.Infrastructure.Data;

namespace Skinet.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly StoreContext _db;
    private Hashtable _repositories;

    public UnitOfWork(StoreContext db)
    {
        _db = db;
    }
    
    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        _repositories ??= new Hashtable();

        var type = typeof(TEntity).Name;

        if (_repositories.ContainsKey(type)) return (IGenericRepository<TEntity>) _repositories[type];
        var repoType = typeof(GenericRepository<>);
        var repoInstance = Activator.CreateInstance(repoType.MakeGenericType(typeof(TEntity)), _db);
        _repositories.Add(type, repoInstance);
        return (IGenericRepository<TEntity>) _repositories[type];
    }

    public async Task<int> Complete()
    {
        return await _db.SaveChangesAsync();
    }
    
    public void Dispose()
    {
        _db.Dispose();
    }
}
