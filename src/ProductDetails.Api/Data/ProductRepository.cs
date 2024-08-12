using ProductDetails.Api.Data.Entities;
using System.Collections.Concurrent;

namespace ProductDetails.Api.Data;

public class InMemoryProductRepository : IProductRepository
{
    private ConcurrentDictionary<string, Product> _products = new();

    public Task<IEnumerable<Product>> GetByStockcodesAsync(string[] stockcodes, CancellationToken cancellationToken)
    {
        return Task.FromResult(_products.Values.Where(p => stockcodes.Contains(p.Stockcode)));
    }

    public Task<(int inserted, int updated)> UpsertAsync(Product product, CancellationToken cancellationToken)
    {
        if (_products.TryAdd(product.Stockcode, product))
        {
            return Task.FromResult((1, 0));
        }

        _products[product.Stockcode] = product;
        return Task.FromResult((0, 1));
    }
}
