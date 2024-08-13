namespace ProductDetails.Api.Products.Update;

public record Request(string Stockcode, string Name, string Description, decimal Price);
