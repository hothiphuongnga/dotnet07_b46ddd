using AutoMapper;
using SalesDb.Application.Common;
using SalesDb.Domain.Entities;
using SalesDb.Domain.Interfaces;

namespace SalesDb.Application.Products;

public class ProductService : ServiceBase<Product, ProductDto, ProductCreateDto, int>, IProductService
{
    private readonly IProductRepository _repository;
    public ProductService(IProductRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        : base(repository, unitOfWork, mapper)
    {
        _repository = repository;
    }

    public async Task<ApiResponse<ProductImageDto>> AddImageAsync(int productId, AddProductImageDto dto)
    {

        // lấy product theo productId
        var product = await _repository.GetWithImagesAsync(productId);
        // check null -> lỗi
        if (product == null)
            return ApiResponse<ProductImageDto>.Fail("Product not found", 404);
        if(dto.IsPrimary && product.ProductImages.Any(i=> i.IsPrimary))
        {
            product.ClearPrimaryImage();
            await Uow.SaveChangesAsync();
        }
        ProductImage newImage = product.AddImage(dto.ImageUrl, dto.StoredFileName, dto.IsPrimary);
        await Uow.SaveChangesAsync();
        return ApiResponse<ProductImageDto>.Ok(Mapper.Map<ProductImageDto>(newImage));
        // thêm 
        throw new NotImplementedException();
    }

    public async Task<ApiResponse<ProductDto>> GetWithImages(int productId)
    {
        var product = await _repository.GetWithImagesAsync(productId);
        if (product == null)
            return ApiResponse<ProductDto>.Fail("Product not found", 404);
        return ApiResponse<ProductDto>.Ok(Mapper.Map<ProductDto>(product));
    }
}