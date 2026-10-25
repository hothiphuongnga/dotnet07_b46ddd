using SalesDb.Application.Common;
using SalesDb.Domain.Entities;

namespace SalesDb.Application.Products;


public interface IProductService : IServiceBase<Product, ProductDto, ProductCreateDto, int>
{
        Task<ApiResponse<ProductImageDto>> AddImageAsync(int productId, AddProductImageDto dto);

        // get with image

        Task<ApiResponse<ProductDto>> GetWithImages(int productId);

}