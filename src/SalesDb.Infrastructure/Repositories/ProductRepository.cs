using SalesDb.Domain.Entities;
using SalesDb.Domain.Interfaces;
using SalesDb.Infrastructure.Persistence;

namespace SalesDb.Infrastructure.Repositories;

public class ProductRepository : RepositoryBase<Product, int>, IProductRepository
{
    public ProductRepository(AppDbContext db) : base(db)
    {
    }
}