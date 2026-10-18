using SalesDb.Application.Common;
using SalesDb.Domain.Entities;

namespace SalesDb.Application.Products;


public interface IProductService : IServiceBase<Product, ProductDto, ProductCreateDto, int>
{
    
}