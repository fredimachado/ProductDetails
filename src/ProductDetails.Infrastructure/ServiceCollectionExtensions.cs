using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using MongoDB.Driver.Core.Extensions.DiagnosticSources;
using MongoDB.Entities;
using ProductDetails.Domain.Messaging;
using ProductDetails.Domain.Products;
using ProductDetails.Domain.Promotions;
using ProductDetails.Domain.Tags;
using ProductDetails.Infrastructure.Behaviors;
using ProductDetails.Infrastructure.Data.Products;
using ProductDetails.Infrastructure.Data.Promotions;
using ProductDetails.Infrastructure.Data.Tags;
using ProductDetails.Infrastructure.Messaging;
using RabbitMQ.Client;

namespace ProductDetails.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMongoDbWithTracing(configuration);

        return services;
    }

    public static IServiceCollection AddMediatR<T>(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<T>();
            config.AddOpenBehavior(typeof(RequestHandlerLoggingBehavior<,>));
        });

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IProductRepository, ProductRepository>();
        services.AddSingleton<ITagRepository, TagRepository>();

        services.AddSingleton<IPromotionRepository, PromotionRepository>();

        return services;
    }

    public static IServiceCollection AddRabbitMqPublisher(this IServiceCollection services, IConfiguration configuration)
    {
        if (string.IsNullOrWhiteSpace(configuration.GetConnectionString("RabbitMq")))
        {
            // Fail fast. Service shouldn't be able to start with invalid configuration.
            throw new InvalidOperationException("Invalid RabbitMq configuration.");
        }

        services.AddOptions<RabbitMqSettings>()
                .Bind(configuration.GetSection(RabbitMqSettings.SectionName));

        services.AddSingleton<IBusPublisher, RabbitMqPublisher>();

        if (configuration.GetValue("RabbitMq:InitializeExchange", false))
        {
            InitializeRabbitMqExchange(services);
        }

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

        ConventionRegistry.Register(
            "EnumStringConvention",
            new ConventionPack { new EnumRepresentationConvention(BsonType.String) },
            _ => true);

        DB.InitAsync("ProductDetails", mongoClientSettings)
            .GetAwaiter()
            .GetResult();
    }

    private static void InitializeRabbitMqExchange(IServiceCollection services)
    {
        using var scope = services.BuildServiceProvider().CreateScope();
        var connection = scope.ServiceProvider.GetRequiredService<IConnection>();
        var settings = scope.ServiceProvider.GetRequiredService<IOptions<RabbitMqSettings>>();

        var channel = connection.CreateModel();
        channel.ExchangeDeclare(exchange: ExchangeNameProvider.Get(settings.Value.ExchangeName),
                                type: ExchangeType.Topic,
                                durable: true,
                                autoDelete: false);
    }
}
