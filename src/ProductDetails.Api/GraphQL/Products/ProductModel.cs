namespace ProductDetails.Api.GraphQL.Products;

public record ProductModel(string Stockcode, string Name, string Description, string Image, decimal Price, decimal? WasPrice = null);
