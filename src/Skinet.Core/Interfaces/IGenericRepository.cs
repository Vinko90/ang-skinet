using Skinet.Core.Entities;
using Skinet.Core.Specifications;

namespace Skinet.Core.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity //Limit repo only to data models
{
    Task<T> GetByIdAsync(int id);

    Task<IReadOnlyList<T>> GetAllAsync();

    Task<IReadOnlyList<T>> GetAsync(ISpecification<T> spec);
    
    Task<T> GetEntityWithSpec(ISpecification<T> spec);
}
