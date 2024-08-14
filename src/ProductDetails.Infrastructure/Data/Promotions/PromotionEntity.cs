using MongoDB.Entities;

namespace ProductDetails.Infrastructure.Data.Promotions;

[Collection("Promotions")]
internal class PromotionEntity(string stockcode, decimal promotionalPrice, DateTimeOffset startDate, DateTimeOffset endDate) : Entity
{
    public string Stockcode { get; init; } = stockcode;
    public decimal PromotionalPrice { get; init; } = promotionalPrice;
    public DateTimeOffset StartDate { get; init; } = startDate;
    public DateTimeOffset EndDate { get; init; } = endDate;
    public DateTimeOffset? PublishedDate { get; set; }
}
