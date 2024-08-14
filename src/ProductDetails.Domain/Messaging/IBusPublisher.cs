namespace ProductDetails.Domain.Messaging;

public interface IBusPublisher
{
    Task Publish<T>(T message) where T : class, IBusMessage;
}
