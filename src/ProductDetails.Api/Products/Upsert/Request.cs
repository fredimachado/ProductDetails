namespace ProductDetails.Api.Products.Upsert;

public record Request(string Stockcode, string Name, string Description, decimal Price);
