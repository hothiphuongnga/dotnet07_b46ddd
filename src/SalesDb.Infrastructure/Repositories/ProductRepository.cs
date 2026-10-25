using Microsoft.EntityFrameworkCore;
using SalesDb.Domain.Entities;
using SalesDb.Domain.Interfaces;
using SalesDb.Infrastructure.Persistence;

namespace SalesDb.Infrastructure.Repositories;

public class ProductRepository : RepositoryBase<Product, int>, IProductRepository
{
    public ProductRepository(AppDbContext db) : base(db)
    {
        
        
    }

    public async Task<Product?> GetWithImagesAsync(int productId)
    {
        return await Db.Products.Include(p=> p.ProductImages).SingleOrDefaultAsync(p=> p.Id == productId);
    }
}