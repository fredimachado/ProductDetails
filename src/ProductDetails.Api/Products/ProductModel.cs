namespace ProductDetails.Api.Products;

public class ProductModel
{
    public required string Stockcode { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required decimal Price { get; init; }
}
