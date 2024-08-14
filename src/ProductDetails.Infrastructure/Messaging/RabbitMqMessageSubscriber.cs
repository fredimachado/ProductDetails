﻿using ProductDetails.Domain.Messaging;
using System.Collections.Concurrent;

namespace ProductDetails.Infrastructure.Messaging;

internal class RabbitMqMessageSubscriber : IMessageSubscriber
{
    private readonly ConcurrentDictionary<Type, Func<IServiceProvider, object, Task>> _handlers = new();

    public IReadOnlyDictionary<Type, Func<IServiceProvider, object, Task>> Handlers => _handlers.AsReadOnly();

    public IMessageSubscriber Subscribe<T>(Func<IServiceProvider, T, Task> handle) where T : class, IBusMessage
    {
        _handlers.TryAdd(typeof(T), (serviceProvider, message) => handle(serviceProvider, (T)message));
        return this;
    }
}
