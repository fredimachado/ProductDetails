using MediatR;
using Microsoft.Extensions.Logging;
using ProductDetails.Domain.Exceptions;

namespace ProductDetails.Domain.Products.Commands;

public class ExpirePromotionPriceCommand(string promotionId, string stockcode) : IRequest
{
    public string PromotionId { get; } = promotionId;
    public string Stockcode { get; } = stockcode;
}

internal sealed class ExpirePromotionPriceCommandHandler(IProductRepository productRepository, ILogger<ExpirePromotionPriceCommandHandler> logger) : IRequestHandler<ExpirePromotionPriceCommand>
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly ILogger _logger = logger;

    public async Task Handle(ExpirePromotionPriceCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByStockcodeAsync(request.Stockcode, cancellationToken)
            ?? throw new ProductNotFoundException(request.Stockcode);

        product.DisablePromotionalPrice();

        await _productRepository.UpdateAsync(product, cancellationToken);
    }
}
