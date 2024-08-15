using MongoDB.Entities;

namespace ProductDetails.Infrastructure.Data.Tags;

[Collection("ProductTags")]
internal class ProductTagEntity(string stockcode, Tag[] tags) : Entity
{
    public string Stockcode { get; init; } = stockcode;
    public Tag[] Tags { get; private set; } = tags;

    internal void CombineTags(Domain.Tags.Tag[] tags)
    {
        var newTags = new List<Tag>(Tags);

        foreach (var existingTag in Tags)
        {
            if (tags.Any(tags => tags.Kind == existingTag.Kind && tags.Category == existingTag.Category))
            {
                newTags.Remove(existingTag);
            }
        }

        newTags.AddRange(tags.Select(t => new Tag(t.Kind, t.Category, t.PromotionId, t.IsEnabled, t.Value)));

        Tags = [.. newTags];
    }
}
