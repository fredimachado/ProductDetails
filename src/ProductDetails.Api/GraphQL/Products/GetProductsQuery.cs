using ProductDetails.Api.Data;

namespace ProductDetails.Api.GraphQL.Products;

public class GetProductsQuery
{
    public async Task<IEnumerable<ProductModel>> GetProductsAsync(
        [Service] IProductRepository productRepository,
        string[] stockcodes,
        CancellationToken cancellationToken)
    {
        if (stockcodes.Length == 0)
        {
            return [];
        }

        var products = await productRepository.GetByStockcodesAsync(stockcodes, cancellationToken);

        return products.Select(p => new ProductModel(
            p.Stockcode,
            p.Name,
            p.Description,
            p.Price
        ));
    }
}
