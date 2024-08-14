using MediatR;
using ProductDetails.Domain.Messages;
using ProductDetails.Domain.Tags.Commands;
using ProductDetails.Infrastructure;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration)
                .AddProductRepositories();

builder.AddRabbitMQClient("rabbitmq", configureConnectionFactory: config =>
{
    config.DispatchConsumersAsync = true;
});

builder.Services.AddRabbitMqConsumerService(builder.Configuration);

builder.Services.AddMediatR<AddPromotionCommand>();

var host = builder.Build();

host.UseMessageSubscriber()
   .SubscribeAndSend<ProductPromotionMessage>(message => new AddPromotionCommand(
           message.PromotionId,
           message.Stockcode,
           message.Price,
           message.PromotionalPrice,
           message.EndDate))
   .SubscribeAndSend<ProductPromotionExpiredMessage>(message => new ExpirePromotionCommand(
           message.PromotionId,
           message.Stockcode));

host.Run();
