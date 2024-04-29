using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using YourCommerce.Application.Common.Behaviours;

namespace YourCommerce.Application;

public static class DependencyInjection
{

    public static IServiceCollection AddApplication (this IServiceCollection services)
    {
        services.AddMediatR(config => {
            config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();
        });

        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehaviour<,>)
        );

        services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyReference>();

        return services;
    }

}