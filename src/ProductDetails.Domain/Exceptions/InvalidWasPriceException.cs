namespace ProductDetails.Domain.Exceptions;

public sealed class InvalidWasPriceException(string stockcode, decimal? wasPrice)
    : Exception($"Invalid Was Price '{wasPrice}' for product {stockcode}.");
