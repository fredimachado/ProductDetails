namespace ProductDetails.Domain.Exceptions;

public sealed class OverlappingPromotionException(string promotionId, string stockcode, DateTimeOffset startDateUtc, DateTimeOffset endDateUtc)
    : DomainException($"Promotion {promotionId} overlaps with new promotion for product {stockcode} from {startDateUtc} to {endDateUtc}.")
{
    public string PromotionId { get; } = promotionId;
    public string Stockcode { get; } = stockcode;
    public DateTimeOffset StartDateUtc { get; } = startDateUtc;
    public DateTimeOffset EndDateUtc { get; } = endDateUtc;
}
