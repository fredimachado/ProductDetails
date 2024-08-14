
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

        Tags = Tags.Append(new Tag(TagKind.Promotion, TagCategory.Save, promotionId, IsEnabled: true, $"Save {saveAmount}", $"{saveAmount}")).ToArray();
    }

    internal void AddFlashDealTag(string promotionId)
    {
        Tags = Tags.Append(new Tag(TagKind.Promotion, TagCategory.FlashDeal, promotionId, IsEnabled: true, $"Flash Deal", "")).ToArray();
    }
}
