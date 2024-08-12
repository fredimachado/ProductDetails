using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ProductDetails.Api.Data;
using ProductDetails.Api.GraphQL.Products;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer()
                .AddAuthorization()
                .AddInstrumentation(options => options.RenameRootActivity = true)
                .AddQueryType<GetProductsQuery>()
                .InitializeOnStartup();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();

builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();

builder.Services.AddFastEndpoints()
                .AddAuthorization()
                .SwaggerDocument();

var app = builder.Build();

app.MapGraphQL();

app.MapGet("/healthz", () => "Ok");

app.UseSwaggerGen()
   .UseAuthentication()
   .UseAuthorization()
   .UseFastEndpoints();


app.RunWithGraphQLCommands(args);
