using Aspirant.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var mongoDb = builder.AddMongoDB("mongodb", port: 57017)
    .WithDataVolume();

var rabbitMq = builder.AddRabbitMQ("rabbitmq", port: 55672)
    .WithDataVolume()
    .WithManagementPlugin(port: 45672)
    .WithHealthCheck();

builder.AddProject<Projects.ProductDetails_DbMigration>("productdetails-dbmigration")
    .WithReferenceWait(mongoDb);

builder.AddProject<Projects.ProductDetails_Api>("productdetails-api")
    .WithExternalHttpEndpoints()
    .WithReferenceWait(mongoDb);

builder.AddProject<Projects.ProductDetails_Tagging_Worker>("productdetails-tagging-worker")
    .WithReferenceWait(mongoDb)
    .WithReferenceWait(rabbitMq);

builder.AddProject<Projects.ProductDetails_Promotion_Api>("productdetails-promotion-api")
    .WithReferenceWait(mongoDb)
    .WithReferenceWait(rabbitMq);

builder.AddProject<Projects.ProductDetails_Promotion_Worker>("productdetails-promotion-worker")
    .WithReferenceWait(mongoDb)
    .WithReferenceWait(rabbitMq);

builder.Build().Run();
