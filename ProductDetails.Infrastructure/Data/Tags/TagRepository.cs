using MongoDB.Entities;
using ProductDetails.Domain.Tags;

namespace ProductDetails.Infrastructure.Data.Tags;

internal class TagRepository : ITagRepository
{
    public async Task<ProductTag?> GetByStockcodeAsync(string stockcode, CancellationToken cancellationToken)
    {
        return await DB.Find<ProductTagEntity, ProductTag>()
            .Match(t => t.Stockcode == stockcode)
            .Project(p => new(p.Stockcode, p.Tags.Select(t => new Domain.Tags.Tag(t.Kind, t.Category, t.Text, t.Value)).ToArray()))
            .ExecuteFirstAsync(cancellationToken);
    }

    public async Task<IEnumerable<ProductTag>> GetByStockcodeAsync(string[] stockcodes, CancellationToken cancellationToken)
    {
        return await DB.Find<ProductTagEntity, ProductTag>()
            .Match(t => stockcodes.Contains(t.Stockcode))
            .Project(p => new(p.Stockcode, p.Tags.Select(t => new Domain.Tags.Tag(t.Kind, t.Category, t.Text, t.Value)).ToArray()))
            .ExecuteAsync(cancellationToken);
    }
}
