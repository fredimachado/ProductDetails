namespace ProductDetails.Domain.Promotions;

public interface IPromotionRepository
{
    Task<IEnumerable<Promotion>> GetPendingPromotionsAsync(CancellationToken cancellationToken);
    Task SetPublishedDateAsync(string promotionId, CancellationToken cancellationToken);
    Task InsertAsync(Promotion promotion, CancellationToken cancellationToken);
}
