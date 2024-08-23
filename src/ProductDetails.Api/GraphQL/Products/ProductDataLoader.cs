
using ProductDetails.Domain.Products;

namespace ProductDetails.Api.GraphQL.Products;

public sealed class ProductDataLoader(
    IProductRepository productRepository,
    IBatchScheduler batchScheduler,
    DataLoaderOptions? options = null) : BatchDataLoader<string, ProductModel>(batchScheduler, options)
{
    private readonly IProductRepository _productRepository = productRepository;

    protected override async Task<IReadOnlyDictionary<string, ProductModel>> LoadBatchAsync(
        IReadOnlyList<string> keys,
        CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetByStockcodeAsync([.. keys], cancellationToken);

        return products.Select(p => new ProductModel(p.Stockcode, p.Name, p.Description, p.Image, p.Price, p.WasPrice))
                       .ToDictionary(p => p.Stockcode);
    }
}
