using ProductDetails.Infrastructure;
using ProductDetails.Promotion.Worker;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddInfrastructure(builder.Configuration)
                .AddRepositories()
                .AddRepositories();

builder.AddRabbitMQClient("rabbitmq");

builder.Services.AddRabbitMqPublisher(builder.Configuration);

builder.Services.AddHostedService<Worker>();

var host = builder.Build();

host.Run();
