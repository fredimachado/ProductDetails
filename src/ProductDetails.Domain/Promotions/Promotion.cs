namespace ProductDetails.Domain.Promotions;

public record Promotion(string Stockcode, decimal PromotionalPrice, DateTimeOffset StartDateUtc, DateTimeOffset EndDateUtc)
{
    public string? PromotionId { get; set; }
}
