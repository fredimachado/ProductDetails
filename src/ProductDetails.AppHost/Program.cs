var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ProductDetails_Api>("productdetails-api");

builder.Build().Run();
