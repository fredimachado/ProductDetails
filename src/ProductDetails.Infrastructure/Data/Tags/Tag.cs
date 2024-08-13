using ProductDetails.Domain.Tags;

namespace ProductDetails.Infrastructure.Data.Tags;

internal class Tag(TagKind kind, TagCategory category, string text, string value)
{
    public TagKind Kind { get; init; } = kind;
    public TagCategory Category { get; init; } = category;
    public string Text { get; init; } = text;
    public string Value { get; init; } = value;
}
