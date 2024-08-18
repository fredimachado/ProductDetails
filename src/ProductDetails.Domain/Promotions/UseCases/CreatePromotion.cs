using ProductDetails.Domain.Exceptions;

namespace ProductDetails.Domain.Promotions.UseCases;

public sealed class CreatePromotion(IPromotionRepository promotionRepository)
{
    private readonly IPromotionRepository _promotionRepository = promotionRepository;

    public async Task ExecuteAsync(string stockcode, decimal promotionalPrice, DateTimeOffset startDateUtc, DateTimeOffset endDateUtc, CancellationToken cancellationToken)
    {
        var overlappingPromotion = await _promotionRepository.GetOverlappingPromotion(
            stockcode,
            startDateUtc,
            endDateUtc,
            cancellationToken);

        if (overlappingPromotion != null)
        {
            throw new OverlappingPromotionException(
                overlappingPromotion.PromotionId!,
                stockcode,
                startDateUtc,
                endDateUtc);
        }

        var promotion = new Promotion(stockcode, promotionalPrice, startDateUtc, endDateUtc);

        await _promotionRepository.InsertAsync(promotion, cancellationToken);
    }
}
