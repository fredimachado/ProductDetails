using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProductDetails.Domain.Messaging;
using RabbitMQ.Client;
using System.Diagnostics;
using System.Text.Json;

namespace ProductDetails.Infrastructure.Messaging;

internal class RabbitMqPublisher(IConnection connection, IOptions<RabbitMqSettings> options, ILogger<RabbitMqPublisher> logger) : IBusPublisher
{
    private readonly IConnection _connection = connection;
    private readonly RabbitMqSettings _settings = options.Value;
    private readonly ILogger _logger = logger;

    public Task Publish<T>(T message) where T : class, IBusMessage
    {
        var routingKey = JsonNamingPolicy.SnakeCaseLower.ConvertName(typeof(T).Name);
        var exchange = ExchangeNameProvider.Get(_settings.ExchangeName);

        using var channel = _connection.CreateModel();

        RabbitMqHelper.AddMessagingTags(Activity.Current, message, ExchangeType.Topic, exchange, routingKey);

        var body = JsonSerializer.SerializeToUtf8Bytes(message);

        var basicProperties = channel.CreateBasicProperties();
        basicProperties.MessageId = Guid.NewGuid().ToString("N");
        basicProperties.CorrelationId = Guid.NewGuid().ToString("N");
        basicProperties.Timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

        channel.BasicPublish(exchange,
                             routingKey,
                             basicProperties,
                             body);

        _logger.LogInformation("Published {MessageType} message with routing key {RoutingKey} to Exchange {Exchange}. Message: {@Message}",
            message.GetType().Name, routingKey, exchange, message);

        return Task.CompletedTask;
    }
}
