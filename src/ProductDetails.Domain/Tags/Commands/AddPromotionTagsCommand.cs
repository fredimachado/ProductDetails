using MediatR;
using ProductDetails.Domain.Exceptions;

namespace ProductDetails.Domain.Tags.Commands;

public sealed record AddPromotionTagsCommand(string PromotionId, string Stockcode, decimal Price, decimal PromotionalPrice, DateTimeOffset EndDate) : IRequest;

internal sealed class AddPromotionTagsCommandHandler(ITagRepository tagRepository) : IRequestHandler<AddPromotionTagsCommand>
{
    private readonly ITagRepository _tagRepository = tagRepository;

    private const int FlashDealDurationInDays = 1;

    public async Task Handle(AddPromotionTagsCommand request, CancellationToken cancellationToken)
    {
        if (request.PromotionalPrice >= request.Price)
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
