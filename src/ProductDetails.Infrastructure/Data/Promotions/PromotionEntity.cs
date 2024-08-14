using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Entities;

namespace ProductDetails.Infrastructure.Data.Promotions;

[Collection("Promotions")]
internal class PromotionEntity(string stockcode, decimal promotionalPrice, DateTimeOffset startDateUtc, DateTimeOffset endDateUtc) : Entity
{
    public string Stockcode { get; init; } = stockcode;
    public decimal PromotionalPrice { get; init; } = promotionalPrice;
    [BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
    public DateTimeOffset StartDateUtc { get; init; } = startDateUtc;
    [BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
    public DateTimeOffset EndDateUtc { get; init; } = endDateUtc;
    [BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
    public DateTimeOffset? PublishedDateUtc { get; set; }
}
