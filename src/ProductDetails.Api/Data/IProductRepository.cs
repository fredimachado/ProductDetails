using ProductDetails.Api.Data.Entities;

namespace ProductDetails.Api.Data;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetByStockcodesAsync(string[] stockcodes, CancellationToken cancellationToken);
    Task<(int inserted, int updated)> UpsertAsync(Product product, CancellationToken cancellationToken);
}
