﻿namespace ProductDetails.Domain.Promotions;

public interface IPromotionRepository
{
    Task<Promotion?> GetOverlappingPromotion(string stockcode, DateTimeOffset startDateUtc, DateTimeOffset endDateUtc, CancellationToken cancellationToken);
    Task<IEnumerable<Promotion>> GetPendingPromotionsAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Promotion>> GetExpiredPromotionsAsync(CancellationToken cancellationToken);
    Task SetPublishedDateAsync(string promotionId, CancellationToken cancellationToken);
    Task SetPublishedExpiryAsync(string promotionId, CancellationToken cancellationToken);
    Task InsertAsync(Promotion promotion, CancellationToken cancellationToken);
}
