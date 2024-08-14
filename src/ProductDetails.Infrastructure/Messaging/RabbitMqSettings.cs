namespace ProductDetails.Infrastructure.Messaging;

internal class RabbitMqSettings
{
    public const string SectionName = "RabbitMq";

    public required string ExchangeName { get; init; }

    public required string QueuePrefix { get; init; }
}
