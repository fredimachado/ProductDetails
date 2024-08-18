using MediatR;
using ProductDetails.Domain.Exceptions;

namespace ProductDetails.Domain.Promotions.Commands;

public sealed record CreatePromotionCommand(string Stockcode, decimal PromotionalPrice, DateTimeOffset StartDateUtc, DateTimeOffset EndDateUtc) : IRequest;

public sealed class CreatePromotionCommandHandler(IPromotionRepository promotionRepository) : IRequestHandler<CreatePromotionCommand>
{
    private readonly IPromotionRepository _promotionRepository = promotionRepository;

    public async Task Handle(CreatePromotionCommand request, CancellationToken cancellationToken)
    {
        var overlappingPromotion = await _promotionRepository.GetOverlappingPromotion(
            request.Stockcode,
            request.StartDateUtc,
            request.EndDateUtc,
            cancellationToken);

        if (overlappingPromotion != null)
        {
            throw new OverlappingPromotionException(
                overlappingPromotion.PromotionId!,
                request.Stockcode,
                request.StartDateUtc,
                request.EndDateUtc);
        }

        var promotion = new Promotion(request.Stockcode, request.PromotionalPrice, request.StartDateUtc, request.EndDateUtc);

        await _promotionRepository.InsertAsync(promotion, cancellationToken);
    }
}
