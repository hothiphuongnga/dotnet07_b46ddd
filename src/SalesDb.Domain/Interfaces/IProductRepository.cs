using SalesDb.Domain.Entities;

namespace SalesDb.Domain.Interfaces;

public interface IProductRepository  : IRepositoryBase<Product, int>
{
    // có cRUD của base
    // thêm upload image cho product
    Task<Product?> GetWithImagesAsync(int productId);
}