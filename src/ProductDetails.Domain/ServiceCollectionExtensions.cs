using Microsoft.Extensions.DependencyInjection;
using ProductDetails.Domain.Promotions.UseCases;

namespace ProductDetails.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<CreatePromotion>();

        return services;
    }
}
