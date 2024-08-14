namespace ProductDetails.Domain.Promotions;

public interface IPromotionRepository
{
    Task InsertAsync(Promotion promotion, CancellationToken cancellationToken);
}
