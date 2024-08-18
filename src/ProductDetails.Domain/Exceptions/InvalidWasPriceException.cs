namespace ProductDetails.Domain.Exceptions;

public sealed class InvalidWasPriceException(string stockcode, decimal? wasPrice)
    : DomainException($"Invalid Was Price '{wasPrice}' for product {stockcode}.")
{
    public string Stockcode { get; } = stockcode;
    public decimal? WasPrice { get; } = wasPrice;
}
