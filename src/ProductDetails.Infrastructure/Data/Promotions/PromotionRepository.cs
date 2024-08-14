﻿using MongoDB.Entities;
using ProductDetails.Domain.Promotions;

namespace ProductDetails.Infrastructure.Data.Promotions;

internal class PromotionRepository : IPromotionRepository
{
    public async Task<IEnumerable<Promotion>> GetPendingPromotionsAsync(CancellationToken cancellationToken)
    {
        return await DB.Find<PromotionEntity, Promotion>()
            .Match(p => p.PublishedDateUtc == null)
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

    public async Task InsertAsync(Promotion promotion, CancellationToken cancellationToken)
    {
        var entity = new PromotionEntity(promotion.Stockcode, promotion.PromotionalPrice, promotion.StartDate, promotion.EndDate);

        await DB.InsertAsync(entity, cancellation: cancellationToken);
    }
}