using ProductDetails.Domain.Messaging;

namespace ProductDetails.Domain.Messages;

[Message("promotion")]
public sealed record ProductPromotionExpiredMessage(string PromotionId, string Stockcode) : IBusMessage;
