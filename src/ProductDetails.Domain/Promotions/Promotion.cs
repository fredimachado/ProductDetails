namespace ProductDetails.Domain.Promotions;

public record Promotion(string Stockcode, decimal PromotionalPrice, DateTimeOffset StartDate, DateTimeOffset EndDate)
{
    public string? PromotionId { get; set; }
}
