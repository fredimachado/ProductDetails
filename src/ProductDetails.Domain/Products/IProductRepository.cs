namespace ProductDetails.Domain.Products;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetByStockcodeAsync(string[] stockcodes, CancellationToken cancellationToken);
    Task<Product?> GetByStockcodeAsync(string stockcode, CancellationToken cancellationToken);
    Task InsertAsync(Product product, CancellationToken cancellationToken);
    Task UpdateAsync(Product product, CancellationToken cancellationToken);
}
