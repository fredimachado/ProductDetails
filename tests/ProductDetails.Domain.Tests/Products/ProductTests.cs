using FluentAssertions;
using ProductDetails.Domain.Exceptions;
using ProductDetails.Domain.Products;

namespace ProductDetails.Domain.Tests.Products;

public class ProductTests
{
    [Theory]
    [InlineData(100.0, 80.0)]
    [InlineData(100.0, 9999)]
    [InlineData(200.0, 100.0)]
    public void SetPromotionalPrice_ValidPromotionalPrice_ShouldSetPriceAndWasPrice(decimal price, decimal promotionalPrice)
    {
        var product = new Product("stockcode", "name", "description", "image", price);

        product.SetPromotionalPrice(promotionalPrice, "promotionId");

        product.Price.Should().Be(promotionalPrice);
        product.WasPrice.Should().Be(price);
    }

    [Theory]
    [InlineData(100.0, 100.0)]
    [InlineData(100.0, 100.1)]
    [InlineData(100.0, 200.0)]
    public void SetPromotionalPrice_InvalidPromotionalPrice_ShouldThrowInvalidPromotionException(decimal price, decimal promotionalPrice)
    {
        var product = new Product("stockcode", "name", "description", "image", price);

        var result = () => product.SetPromotionalPrice(promotionalPrice, "promotionId");

        result.Should().Throw<InvalidPromotionException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData(0)]
    [InlineData(-1)]
    public void DisablePromotionalPrice_WhenWasPriceIsNullOrZeroOrLessThanZero_ShouldThrowInvalidWasPriceException(int? wasPrice)
    {
        var product = new Product("stockcode", "name", "description", "image", price: 100m, wasPrice);

        var result = product.DisablePromotionalPrice;

        result.Should().Throw<InvalidWasPriceException>();
    }

    [Fact]
    public void DisablePromotionalPrice_WhenWasPriceIsValid_ShouldSetPriceAndWasPriceToNull()
    {
        var product = new Product("stockcode", "name", "description", "image", price: 80m, wasPrice: 100m);

        product.DisablePromotionalPrice();

        product.Price.Should().Be(100m);
        product.WasPrice.Should().BeNull();
    }
}
