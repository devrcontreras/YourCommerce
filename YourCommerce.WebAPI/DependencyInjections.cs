using YourCommerce.WebAPI.Middlewares;

namespace YourCommerce.WebAPI;

public static class DependencyInjection
{

    public static IServiceCollection AddWebAPI(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddTransient<GlobalExceptionHandlerMiddlerMiddleware>();
        return services;
    }

}