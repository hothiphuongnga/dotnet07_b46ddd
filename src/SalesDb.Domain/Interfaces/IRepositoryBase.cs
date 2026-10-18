using SalesDb.Domain.Common;

namespace SalesDb.Domain.Interfaces;

public interface IRepositoryBase<T, TKey> where T : BaseEntity<TKey>
{
    
    Task<T?> GetByIdAsync(TKey id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
}