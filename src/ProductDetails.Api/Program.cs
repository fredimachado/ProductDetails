using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ProductDetails.Api.GraphQL;
using ProductDetails.Infrastructure;
using ProductDetails.Infrastructure.Auth;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddProductDetailsGraphQL()
                .AddInfrastructure(builder.Configuration)
                .AddProductRepositories();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();

builder.Services.AddFastEndpoints()
                .AddAuthorization(options =>
                    options.AddPolicy(
                        AuthConstants.AdminPolicy,
                        p => p.RequireRole(AuthConstants.AdminRole)
                              .RequireClaim(AuthConstants.AdminIdClaim)))
                .SwaggerDocument();

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapGraphQL();

app.MapGet("/healthz", () => "Ok")
   .ExcludeFromDescription();

app.UseSwaggerGen()
   .UseAuthentication()
   .UseAuthorization()
   .UseFastEndpoints();


app.RunWithGraphQLCommands(args);
