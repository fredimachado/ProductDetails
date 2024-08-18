using ProductDetails.Domain.Messages;
using ProductDetails.Domain.Tags.Commands;
using ProductDetails.Infrastructure;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddInfrastructure(builder.Configuration)
                .AddRepositories()
                .AddMediatR<AddPromotionTagsCommand>();

builder.AddRabbitMQClient("rabbitmq", configureConnectionFactory: config =>
{
    config.DispatchConsumersAsync = true;
});

builder.Services.AddRabbitMqConsumerService(builder.Configuration);

var host = builder.Build();

host.UseMessageSubscriber()
   .SubscribeAndSend<ProductPromotionMessage>(message => new AddPromotionTagsCommand(
           message.PromotionId,
           message.Stockcode,
           message.Price,
           message.PromotionalPrice,
           message.EndDate))
   .SubscribeAndSend<ProductPromotionExpiredMessage>(message => new ExpirePromotionTagsCommand(
           message.PromotionId,
           message.Stockcode));

host.Run();
