using ProductDetails.Api.GraphQL.Products;
using ProductDetails.Api.GraphQL.ProductTags;

namespace ProductDetails.Api.GraphQL;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProductDetailsGraphQL(this IServiceCollection services)
    {
        services.AddGraphQLServer()
                .AddAuthorization()
                .AddInstrumentation(options => options.RenameRootActivity = true)
                .AddQueryType<ProductQueries>()
                .AddTypeExtension<ProductTagsExtensions>()
                .InitializeOnStartup();

        return services;
    }
}
