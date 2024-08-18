namespace ProductDetails.Domain.Exceptions;

public sealed class ProductNotFoundException(string stockcode)
    : DomainException($"The product {stockcode} was not found.")
{
    public string Stockcode { get; } = stockcode;
}
