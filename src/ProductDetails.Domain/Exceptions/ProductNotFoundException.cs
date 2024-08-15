namespace ProductDetails.Domain.Exceptions;

public sealed class ProductNotFoundException(string stockcode)
    : Exception($"The product {stockcode} was not found.");
