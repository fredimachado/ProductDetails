namespace ProductDetails.Domain.Exceptions;

public sealed class InvalidPromotionException(string promotionId, decimal price, decimal promotionalPrice)
    : Exception($"Invalid promotion {promotionId} with Price: {price} and Promotional Price: {promotionalPrice}.");
