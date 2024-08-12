using HotChocolate.Authorization;
using ProductDetails.Api.Products;
using System.Security.Claims;

namespace ProductDetails.Api.GraphQL;

public class ProductQuery
{
    public async Task<IEnumerable<ProductModel>> GetProductsAsync(
        [Service] IProductService productService,
        string[] stockcodes,
        CancellationToken cancellationToken)
    {
        return stockcodes.Length == 0 ? [] : await productService.GetProductsAsync(stockcodes);
    }
}
