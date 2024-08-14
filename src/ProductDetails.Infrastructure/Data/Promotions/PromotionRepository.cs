using MongoDB.Entities;
using ProductDetails.Domain.Promotions;

namespace ProductDetails.Infrastructure.Data.Promotions;

internal class PromotionRepository : IPromotionRepository
{
    public async Task InsertAsync(Promotion promotion, CancellationToken cancellationToken)
    {
        var entity = new PromotionEntity(promotion.Stockcode, promotion.PromotionalPrice, promotion.StartDate, promotion.EndDate);

        await DB.InsertAsync(entity, cancellation: cancellationToken);
    }
}
