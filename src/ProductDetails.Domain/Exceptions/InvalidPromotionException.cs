namespace ProductDetails.Domain.Exceptions;

public sealed class InvalidPromotionException(string promotionId, decimal price, decimal promotionalPrice)
    : DomainException($"Invalid promotion {promotionId} with Price: {price} and Promotional Price: {promotionalPrice}.")
{
    public string PromotionId { get; } = promotionId;
    public decimal Price { get; } = price;
    public decimal PromotionalPrice { get; } = promotionalPrice;
}
