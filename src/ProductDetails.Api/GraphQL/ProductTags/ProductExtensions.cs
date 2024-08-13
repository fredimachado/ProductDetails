using ProductDetails.Api.GraphQL.Products;

namespace ProductDetails.Api.GraphQL.ProductTags;

[ExtendObjectType<ProductModel>]
public class ProductTagsExtensions
{
    public async Task<IEnumerable<TagModel>> GetTagsAsync(
        [Parent] ProductModel product,
        ProductTagDataLoader productTagDataLoader,
        CancellationToken cancellationToken)
    {
        var productTags = await productTagDataLoader.LoadAsync(product.Stockcode, cancellationToken);

        return productTags ?? [];
    }
}
