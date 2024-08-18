using MongoDB.Entities;
using ProductDetails.Domain.Promotions;

namespace ProductDetails.Infrastructure.Data.Promotions;

internal class PromotionRepository : IPromotionRepository
{
    public async Task<Promotion?> GetOverlappingPromotion(string stockcode, DateTimeOffset startDateUtc, DateTimeOffset endDateUtc, CancellationToken cancellationToken)
        => await DB.Find<PromotionEntity, Promotion>()
            .Match(p => p.Stockcode == stockcode && startDateUtc < p.EndDateUtc && endDateUtc > p.StartDateUtc)
            .Project(p => new Promotion(p.Stockcode, p.PromotionalPrice, p.StartDateUtc, p.EndDateUtc)
            {
                PromotionId = p.ID
            })
            .ExecuteFirstAsync(cancellationToken);

    public async Task<IEnumerable<Promotion>> GetPendingPromotionsAsync(CancellationToken cancellationToken)
    {
        return await DB.Find<PromotionEntity, Promotion>()
            .Match(p => p.PublishedDateUtc == null)
            .Sort(p => p.StartDateUtc, Order.Ascending)
            .Project(p => new Promotion(p.Stockcode, p.PromotionalPrice, p.StartDateUtc, p.EndDateUtc)
            {
                PromotionId = p.ID
            })
            .ExecuteAsync(cancellationToken);
    }

    public async Task<IEnumerable<Promotion>> GetExpiredPromotionsAsync(CancellationToken cancellationToken)
    {
        return await DB.Find<PromotionEntity, Promotion>()
            .Match(p => p.PublishedExpiry == null && p.EndDateUtc <= DateTimeOffset.UtcNow)
            .Sort(p => p.EndDateUtc, Order.Ascending)
            .Project(p => new Promotion(p.Stockcode, p.PromotionalPrice, p.StartDateUtc, p.EndDateUtc)
            {
                PromotionId = p.ID
            })
            .ExecuteAsync(cancellationToken);
    }

    public async Task SetPublishedDateAsync(string promotionId, CancellationToken cancellationToken)
    {
        await DB.Update<PromotionEntity>()
            .MatchID(promotionId)
            .Modify(p => p.PublishedDateUtc, DateTimeOffset.UtcNow)
            .ExecuteAsync(cancellationToken);
    }

    public async Task SetPublishedExpiryAsync(string promotionId, CancellationToken cancellationToken)
    {
        await DB.Update<PromotionEntity>()
            .MatchID(promotionId)
            .Modify(p => p.PublishedExpiry, true)
            .ExecuteAsync(cancellationToken);
    }

    public async Task InsertAsync(Promotion promotion, CancellationToken cancellationToken)
    {
        var entity = new PromotionEntity(promotion.Stockcode, promotion.PromotionalPrice, promotion.StartDateUtc, promotion.EndDateUtc);

        await DB.InsertAsync(entity, cancellation: cancellationToken);
    }
}
