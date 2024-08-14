using ProductDetails.Domain.Messaging;

namespace ProductDetails.Domain.Messages;

[Message("promotion")]
public sealed record ProductPromotionMessage(string PromotionId, string Stockcode, decimal Price, decimal PromotionalPrice, DateTimeOffset EndDate) : IBusMessage;
