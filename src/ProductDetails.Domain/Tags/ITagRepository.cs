
namespace ProductDetails.Domain.Tags;

public interface ITagRepository
{
    Task<ProductTag?> GetByStockcodeAsync(string stockcode, CancellationToken cancellationToken);
    Task<IEnumerable<ProductTag>> GetByStockcodeAsync(string[] stockcode, CancellationToken cancellationToken);
    Task SaveAsync(ProductTag productTag, CancellationToken cancellationToken);
}
