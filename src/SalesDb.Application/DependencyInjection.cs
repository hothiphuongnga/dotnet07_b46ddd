using Microsoft.Extensions.DependencyInjection;
using SalesDb.Application.Common;
using SalesDb.Application.Products;
using SalesDb.Application.Mappings;
using SalesDb.Application.Orders;

namespace SalesDb.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // auto mapper
        services.AddAutoMapper(configuration => configuration.AddMaps(typeof(MappingProfile).Assembly));


        // Register repositories
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IOrderService, OrderService>();
        

        return services;
    }
}