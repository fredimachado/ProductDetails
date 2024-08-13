using ProductDetails.Domain.Products;

namespace ProductDetails.Api.GraphQL.Products;

public sealed class ProductQueries
{
    public async Task<IEnumerable<ProductModel>> GetProductsAsync(
        [Service] IProductRepository productRepository,
        CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAllAsync(cancellationToken);

        return products.Select(p => new ProductModel(
            p.Stockcode,
            p.Name,
            p.Description,
            p.Price
        ));
    }

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
