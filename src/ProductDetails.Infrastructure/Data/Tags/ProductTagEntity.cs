using MongoDB.Entities;

namespace ProductDetails.Infrastructure.Data.Tags;

[Collection("ProductTags")]
internal class ProductTagEntity(string stockcode, Tag[] tags) : Entity
{
    public string Stockcode { get; set; } = stockcode;
    public Tag[] Tags { get; set; } = tags;
}
