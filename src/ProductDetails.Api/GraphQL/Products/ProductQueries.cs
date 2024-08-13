namespace ProductDetails.Api.GraphQL.Products;

public sealed class ProductQueries
{
    public async Task<ProductModel?> GetProductAsync(
        string stockcode,
        ProductDataLoader dataLoader,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(stockcode) || stockcode.Length == 0)
        {
            return null;
        }

        var product = await dataLoader.LoadAsync(stockcode, cancellationToken);

        return product is not null
            ? new ProductModel(
                product.Stockcode,
                product.Name,
                product.Description,
                product.Price
                )
            : null;
    }
}
