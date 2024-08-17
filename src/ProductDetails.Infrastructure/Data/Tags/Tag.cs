using ProductDetails.Domain.Tags;

namespace ProductDetails.Infrastructure.Data.Tags;

internal class Tag(TagKind kind, TagCategory category, string promotionId, bool isEnabled, decimal? value)
{
    public TagKind Kind { get; init; } = kind;
    public TagCategory Category { get; init; } = category;
    public string PromotionId { get; init; } = promotionId;
    public bool IsEnabled { get; init; } = isEnabled;
    public decimal? Value { get; init; } = value;
}
