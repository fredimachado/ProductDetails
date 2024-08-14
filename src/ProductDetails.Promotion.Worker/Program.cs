using ProductDetails.Promotion.Worker;
using ProductDetails.Infrastructure;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddInfrastructure(builder.Configuration)
                .AddPromotionRepositories()
                .AddProductRepositories();

builder.AddRabbitMQClient("rabbitmq");

builder.Services.AddRabbitMqPublisher(builder.Configuration);

builder.Services.AddHostedService<Worker>();

var host = builder.Build();

host.Run();
