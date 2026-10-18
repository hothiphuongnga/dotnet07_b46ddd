using AutoMapper;
using SalesDb.Application.Common;
using SalesDb.Domain.Entities;
using SalesDb.Domain.Interfaces;

namespace SalesDb.Application.Products;

public class ProductService : ServiceBase<Product, ProductDto, ProductCreateDto, int>, IProductService
{
    public ProductService(IProductRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        : base(repository, unitOfWork, mapper)
    {
    }
}