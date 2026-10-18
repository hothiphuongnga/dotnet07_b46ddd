using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesDb.Domain.Interfaces;
using SalesDb.Infrastructure.Persistence;
using SalesDb.Infrastructure.Repositories;

namespace SalesDb.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        // base
        services.AddScoped(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));

        // Register repositories
        services.AddScoped<IProductRepository, ProductRepository>();

        // DI UOW
        services.AddScoped<IUnitOfWork>(sp=> sp.GetRequiredService<AppDbContext>());

        return services;
    }
}