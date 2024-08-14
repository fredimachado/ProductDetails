namespace ProductDetails.Domain.Products;

public record Product(string Stockcode, string Name, string Description, decimal Price, decimal? WasPrice = null);
