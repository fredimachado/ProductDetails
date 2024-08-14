using MediatR;
using ProductDetails.Domain.Exceptions;

namespace ProductDetails.Domain.Tags.Commands;

public class PromotionCommand(string promotionId, string stockcode, decimal price, decimal promotionalPrice, DateTimeOffset EndDate) : IRequest
{
    public string PromotionId { get; } = promotionId;
    public string Stockcode { get; } = stockcode;
    public decimal Price { get; } = price;
    public decimal PromotionalPrice { get; } = promotionalPrice;
    public DateTimeOffset EndDate { get; } = EndDate;
}

internal sealed class PromotionCommandHandler(ITagRepository tagRepository) : IRequestHandler<PromotionCommand>
{
    private readonly ITagRepository _tagRepository = tagRepository;

    private const int FlashDealDurationInDays = 1;

    public async Task Handle(PromotionCommand request, CancellationToken cancellationToken)
    {
        if (request.PromotionalPrice <= request.Price)
        {
            throw new InvalidPromotionException(request.PromotionId, request.Price, request.PromotionalPrice);
        }

        var productTag = await _tagRepository.GetByStockcodeAsync(request.Stockcode, cancellationToken)
            ?? new ProductTag(request.Stockcode, []);

        productTag.AddSaveTag(request.PromotionId, request.Price, request.PromotionalPrice);

        if (request.EndDate.Subtract(DateTimeOffset.UtcNow).Days <= FlashDealDurationInDays)
        {
            productTag.AddFlashDealTag(request.PromotionId);
        }

        await _tagRepository.SaveAsync(productTag, cancellationToken);
    }
}
