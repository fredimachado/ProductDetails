using MongoDB.Entities;

namespace ProductDetails.Infrastructure.Data.Products;

[Collection("Products")]
internal class ProductEntity(string stockcode, string name, string description, string image, decimal price, decimal? wasPrice = null) : Entity
{
    public string Stockcode { get; init; } = stockcode;
    public string Name { get; init; } = name;
    public string Description { get; init; } = description;
    public string Image { get; init; } = image;
    public decimal Price { get; init; } = price;
    public decimal? WasPrice { get; init; } = wasPrice;
}
