using ProductDetails.Domain.Messages;
using ProductDetails.Domain.Products.Commands;
using ProductDetails.Infrastructure;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddInfrastructure(builder.Configuration)
                .AddProductRepositories();

builder.AddRabbitMQClient("rabbitmq", configureConnectionFactory: config =>
{
    config.DispatchConsumersAsync = true;
});

builder.Services.AddRabbitMqConsumerService(builder.Configuration);

builder.Services.AddMediatR<AddPromotionPriceCommand>();

var host = builder.Build();

host.UseMessageSubscriber()
   .SubscribeAndSend<ProductPromotionMessage>(message => new AddPromotionPriceCommand(
           message.PromotionId,
           message.Stockcode,
           message.Price,
           message.PromotionalPrice))
   .SubscribeAndSend<ProductPromotionExpiredMessage>(message => new ExpirePromotionPriceCommand(
           message.PromotionId,
           message.Stockcode));

host.Run();
