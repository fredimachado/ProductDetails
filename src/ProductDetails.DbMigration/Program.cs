using MongoDB.Driver;
using ProductDetails.DbMigration;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

await DB.InitAsync("ProductDetails",
    MongoClientSettings.FromConnectionString(builder.Configuration.GetConnectionString("MongoDb")));

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
