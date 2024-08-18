using MediatR;
using ProductDetails.Domain.Exceptions;

namespace ProductDetails.Domain.Products.Commands;

public sealed record AddPromotionPriceCommand(string PromotionId, string Stockcode, decimal Price, decimal PromotionalPrice) : IRequest;

internal sealed class AddPromotionPriceCommandHandler(IProductRepository productRepository) : IRequestHandler<AddPromotionPriceCommand>
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task Handle(AddPromotionPriceCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByStockcodeAsync(request.Stockcode, cancellationToken)
            ?? throw new ProductNotFoundException(request.Stockcode);

        product.SetPromotionalPrice(request.PromotionalPrice, request.PromotionId);

        await _productRepository.UpdateAsync(product, cancellationToken);
    }
}
