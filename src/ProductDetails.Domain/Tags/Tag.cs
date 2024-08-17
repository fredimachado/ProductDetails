namespace ProductDetails.Domain.Tags;

public record Tag(TagKind Kind, TagCategory Category, string PromotionId, decimal? Value = null)
{
    public bool IsEnabled { get; internal set; } = true;
}