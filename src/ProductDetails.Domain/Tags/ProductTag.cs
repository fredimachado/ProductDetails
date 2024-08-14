namespace ProductDetails.Domain.Tags;

public record ProductTag
{
    public ProductTag(string stockcode, Tag[] tags)
    {
        Stockcode = stockcode;
        Tags = tags;
    }

    public string Stockcode { get; init; }
    public Tag[] Tags { get; private set; }

    internal void AddSaveTag(string promotionId, decimal price, decimal promotionalPrice)
    {
        var saveAmount = price - promotionalPrice;

        Tags = [.. Tags, new Tag(TagKind.Promotion, TagCategory.Save, promotionId, $"Save {saveAmount}", $"{saveAmount}")];
    }

    internal void AddFlashDealTag(string promotionId)
    {
        Tags = [.. Tags, new Tag(TagKind.Promotion, TagCategory.FlashDeal, promotionId, $"Flash Deal", "")];
    }

    internal void DisableTags(string promotionId)
    {
        foreach (var tag in Tags.Where(t => t.PromotionId == promotionId))
        {
            tag.IsEnabled = false;
        }
    }
}
