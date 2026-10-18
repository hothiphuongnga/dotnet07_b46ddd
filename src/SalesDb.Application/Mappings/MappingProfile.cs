namespace SalesDb.Application.Mappings;
using AutoMapper;
using SalesDb.Application.Products;
using SalesDb.Domain.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<ProductCreateDto, Product>();
    }
}