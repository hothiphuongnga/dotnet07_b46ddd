using Microsoft.EntityFrameworkCore;
using SalesDb.Domain.Common;
using SalesDb.Domain.Interfaces;
using SalesDb.Infrastructure.Persistence;

namespace SalesDb.Infrastructure.Repositories;

public class RepositoryBase<T, TKey> : IRepositoryBase<T, TKey> where T : BaseEntity<TKey>
{
    protected readonly AppDbContext Db;
    public RepositoryBase(AppDbContext db) => Db = db;
    public async Task<T?> GetByIdAsync(TKey id) => await Db.Set<T>().FindAsync(id);
    public async Task<IEnumerable<T>> GetAllAsync() => await Db.Set<T>().AsNoTracking().ToListAsync();
    public async Task AddAsync(T e) => await Db.Set<T>().AddAsync(e);
    public void Update(T e) => Db.Set<T>().Update(e);
    public void Remove(T e) => Db.Set<T>().Remove(e);
}