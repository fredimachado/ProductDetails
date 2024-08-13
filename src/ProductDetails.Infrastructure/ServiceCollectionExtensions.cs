using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDB.Driver.Core.Extensions.DiagnosticSources;
using MongoDB.Entities;
using ProductDetails.Domain.Products;
using ProductDetails.Domain.Tags;
using ProductDetails.Infrastructure.Data.Products;
using ProductDetails.Infrastructure.Data.Tags;

namespace ProductDetails.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMongoDbWithTracing(configuration);

        services.AddSingleton<IProductRepository, ProductRepository>();
        services.AddSingleton<ITagRepository, TagRepository>();

        return services;
    }

    private static void AddMongoDbWithTracing(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenTelemetry()
                    .WithTracing(tracer => tracer.AddSource("MongoDB.Driver.Core.Extensions.DiagnosticSources"));

        var mongoClientSettings = MongoClientSettings.FromConnectionString(configuration.GetConnectionString("MongoDb"));
        mongoClientSettings.ClusterConfigurator = cb => cb.Subscribe(new DiagnosticsActivityEventSubscriber());

        DB.InitAsync("ProductDetails", mongoClientSettings)
            .GetAwaiter()
            .GetResult();
    }
}
