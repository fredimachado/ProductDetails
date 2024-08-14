using MongoDB.Entities;
using ProductDetails.Domain.Tags;
using System.Linq.Expressions;

namespace ProductDetails.Infrastructure.Data.Tags;

internal class TagRepository : ITagRepository
{
    public async Task<ProductTag?> GetByStockcodeAsync(string stockcode, CancellationToken cancellationToken)
    {
        return await DB.Find<ProductTagEntity, ProductTag>()
            .Match(t => t.Stockcode == stockcode)
            .Project(EnabledTagsProjection)
            .ExecuteFirstAsync(cancellationToken);
    }

    public async Task<IEnumerable<ProductTag>> GetByStockcodeAsync(string[] stockcodes, CancellationToken cancellationToken)
    {
        return await DB.Find<ProductTagEntity, ProductTag>()
            .Match(t => stockcodes.Contains(t.Stockcode))
            .Project(EnabledTagsProjection)
            .ExecuteAsync(cancellationToken);
    }

    public async Task SaveAsync(ProductTag productTag, CancellationToken cancellationToken)
    {
        await DB.Replace<ProductTagEntity>()
            .Match(t => t.Stockcode == productTag.Stockcode)
            .WithEntity(new(productTag.Stockcode, productTag.Tags.Select(t => new Tag(t.Kind, t.Category, t.PromotionId, t.IsEnabled, t.Text, t.Value)).ToArray()))
            .ExecuteAsync(cancellationToken);
    }

    private readonly static Expression<Func<ProductTagEntity, ProductTag>> EnabledTagsProjection =
        p => new(p.Stockcode, p.Tags
                .Where(t => t.IsEnabled)
                .Select(t => new Domain.Tags.Tag(t.Kind, t.Category, t.PromotionId, t.IsEnabled, t.Text, t.Value)).ToArray());
}
