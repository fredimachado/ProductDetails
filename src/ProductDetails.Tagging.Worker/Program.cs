using MediatR;
using ProductDetails.Domain.Messages;
using ProductDetails.Domain.Tags.Commands;
using ProductDetails.Infrastructure;

var builder = Host.CreateApplicationBuilder(args);

builder.AddRabbitMQClient("rabbitmq", configureConnectionFactory: config =>
{
    config.DispatchConsumersAsync = true;
});

builder.Services.AddRabbitMqConsumerService(builder.Configuration);

var host = builder.Build();

host.UseMessageSubscriber()
   .Subscribe<ProductPromotionMessage>(async (serviceProvider, message) =>
   {
       var mediator = serviceProvider.GetRequiredService<IMediator>();

       // Dispatch domain command
       await mediator.Send(new PromotionCommand(
           message.PromotionId,
           message.Stockcode,
           message.Price,
           message.PromotionalPrice,
           message.EndDate));
   });

host.Run();
