using MongoDB.Entities;

namespace ProductDetails.Infrastructure.Data.Products;

[Collection("Products")]
internal class ProductEntity(string stockcode, string name, string description, decimal price) : Entity
{
    public string Stockcode { get; set; } = stockcode;
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
    public decimal Price { get; set; } = price;
}
