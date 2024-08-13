using Aspirant.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var mongoDb = builder.AddMongoDB("mongodb", port: 57017)
    .WithDataVolume();

builder.AddProject<Projects.ProductDetails_DbMigration>("productdetails-dbmigration")
    .WithReference(mongoDb)
    .WaitFor(mongoDb);

builder.AddProject<Projects.ProductDetails_Api>("productdetails-api")
    .WithExternalHttpEndpoints()
    .WithReference(mongoDb)
    .WaitFor(mongoDb);

builder.Build().Run();
