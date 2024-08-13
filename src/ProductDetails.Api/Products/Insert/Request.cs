namespace ProductDetails.Api.Products.Insert;

public record Request(string Stockcode, string Name, string Description, decimal Price);
