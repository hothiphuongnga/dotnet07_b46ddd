namespace SalesDb.Application.Mappings;

using AutoMapper;
using SalesDb.Application.Orders;
using SalesDb.Application.Products;
using SalesDb.Domain.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<ProductCreateDto, Product>();

        // CreateMap<OrderDto, Order>().ReverseMap();
        CreateMap<ProductImage, ProductImageDto>();

    }
}