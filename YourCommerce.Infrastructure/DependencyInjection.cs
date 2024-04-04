using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YourCommerce.Application.Data;
using YourCommerce.Domain.Customers;
using YourCommerce.Domain.Primitives;
using YourCommerce.Infrastructure.Persistence;
using YourCommerce.Infrastructure.Persistence.Repository;

namespace YourCommerce.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistance(configuration);
        return services;
    }

    private static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("YourCommerceDb")));

        services.AddScoped<IApplicationDbContext>(c => c.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IUnitOfWork>(c => c.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        
        return services;

    }
}