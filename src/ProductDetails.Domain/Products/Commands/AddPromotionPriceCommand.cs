using MediatR;
using ProductDetails.Domain.Exceptions;

namespace ProductDetails.Domain.Products.Commands;

public class AddPromotionPriceCommand(string promotionId, string stockcode, decimal price, decimal promotionalPrice) : IRequest
{
    public string PromotionId { get; } = promotionId;
    public string Stockcode { get; } = stockcode;
    public decimal Price { get; } = price;
    public decimal PromotionalPrice { get; } = promotionalPrice;
}

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
