namespace ProductDetails.Domain.Tags;

public record Tag(TagKind Kind, TagCategory Category, string PromotionId, string? Value = null)
{
    public bool IsEnabled { get; internal set; } = true;
}