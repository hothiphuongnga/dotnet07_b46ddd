using SalesDb.Domain.Entities;

namespace SalesDb.Domain.Interfaces;

public interface IProductRepository  : IRepositoryBase<Product, int>
{
}