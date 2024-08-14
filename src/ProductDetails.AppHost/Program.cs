using Aspirant.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var mongoDb = builder.AddMongoDB("mongodb", port: 57017)
    .WithDataVolume();

var rabbitMq = builder.AddRabbitMQ("rabbitmq")
    .WithDataVolume()
    .WithManagementPlugin()
    .WithHealthCheck();

builder.AddProject<Projects.ProductDetails_DbMigration>("productdetails-dbmigration")
    .WithReference(mongoDb)
    .WaitFor(mongoDb);

builder.AddProject<Projects.ProductDetails_Api>("productdetails-api")
    .WithExternalHttpEndpoints()
    .WithReference(mongoDb)
    .WaitFor(mongoDb);

builder.AddProject<Projects.ProductDetails_Tagging_Worker>("productdetails-tagging-worker")
    .WithReference(mongoDb)
    .WithReference(rabbitMq)
    .WaitFor(mongoDb)
    .WaitFor(rabbitMq);

builder.Build().Run();
