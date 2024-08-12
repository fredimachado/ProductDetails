using Microsoft.AspNetCore.Authentication.JwtBearer;
using ProductDetails.Api.GraphQL;
using ProductDetails.Api.Products;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer()
                .AddAuthorization()
                .AddInstrumentation(options => options.RenameRootActivity = true)
                .AddQueryType<ProductQuery>()
                .InitializeOnStartup();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();

builder.Services.AddSingleton<IProductService, ProductService>();

var app = builder.Build();

app.MapGraphQL();
app.MapGet("/healthz", () => "Ok");

app.RunWithGraphQLCommands(args);
