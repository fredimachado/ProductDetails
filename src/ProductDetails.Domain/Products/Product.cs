

using MediatR;
using ProductDetails.Domain.Exceptions;

namespace ProductDetails.Domain.Products;

public record Product(string Stockcode, string Name, string Description)
{
    public Product(string stockcode, string name, string description, decimal price, decimal? wasPrice = null)
        : this(stockcode, name, description)
    {
        Price = price;
        WasPrice = wasPrice;
    }

    public decimal Price { get; private set; }
    public decimal? WasPrice { get; private set; }

    internal void DisablePromotionalPrice()
    {
        if (WasPrice is null || WasPrice.Value <= 0)
        {
            throw new InvalidWasPriceException(Stockcode, WasPrice);
        }

        Price = WasPrice.Value;
        WasPrice = null;
    }

    internal void SetPromotionalPrice(decimal promotionalPrice, string promotionId)
    {
        if (promotionalPrice >= Price)
        {
            throw new InvalidPromotionException(promotionId, Price, promotionalPrice);
        }

        WasPrice = Price;
        Price = promotionalPrice;
    }
}
