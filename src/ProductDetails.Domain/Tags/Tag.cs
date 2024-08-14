namespace ProductDetails.Domain.Tags;

public record Tag(TagKind Kind, TagCategory Category, string PromotionId, bool IsEnabled, string Text, string Value);
