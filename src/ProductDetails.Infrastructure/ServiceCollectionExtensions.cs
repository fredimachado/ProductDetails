using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using MongoDB.Driver.Core.Extensions.DiagnosticSources;
using MongoDB.Entities;
using ProductDetails.Domain.Products;
using ProductDetails.Domain.Promotions;
using ProductDetails.Domain.Tags;
using ProductDetails.Infrastructure.Data.Products;
using ProductDetails.Infrastructure.Data.Promotions;
using ProductDetails.Infrastructure.Data.Tags;
using ProductDetails.Infrastructure.Messaging;

namespace ProductDetails.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMongoDbWithTracing(configuration);

        return services;
    }

    public static IServiceCollection AddProductRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IProductRepository, ProductRepository>();
        services.AddSingleton<ITagRepository, TagRepository>();

        return services;
    }

    public static IServiceCollection AddPromotionRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IPromotionRepository, PromotionRepository>();

        return services;
    }

    public static IServiceCollection AddRabbitMqConsumerService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<RabbitMqSettings>()
                .Bind(configuration.GetSection(RabbitMqSettings.SectionName));

        return services.AddSingleton<IMessageSubscriber, RabbitMqMessageSubscriber>()
                       .AddHostedService<RabbitMqConsumerService>();
    }

    public static IMessageSubscriber UseMessageSubscriber(this IHost app)
    {
        var messageSubscriber = app.Services.GetService<IMessageSubscriber>();

        return messageSubscriber ??
            throw new InvalidOperationException("Message Subscriber is not registered. Make sure to call Services.AddRabbitMqConsumerService.");
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
