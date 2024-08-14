namespace ProductDetails.Domain.Tags;

public record Tag(TagKind Kind, TagCategory Category, string PromotionId, string Text, string Value)
{
    public bool IsEnabled { get; internal set; } = true;
}