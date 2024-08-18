using MediatR;
using ProductDetails.Domain.Exceptions;

namespace ProductDetails.Domain.Products.Commands;

public sealed record ExpirePromotionPriceCommand(string PromotionId, string Stockcode) : IRequest;

internal sealed class ExpirePromotionPriceCommandHandler(IProductRepository productRepository) : IRequestHandler<ExpirePromotionPriceCommand>
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task Handle(ExpirePromotionPriceCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByStockcodeAsync(request.Stockcode, cancellationToken)
            ?? throw new ProductNotFoundException(request.Stockcode);

        product.DisablePromotionalPrice();

        await _productRepository.UpdateAsync(product, cancellationToken);
    }
}
