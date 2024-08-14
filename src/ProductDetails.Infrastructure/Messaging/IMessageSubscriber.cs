﻿using ProductDetails.Domain.Messaging;

namespace ProductDetails.Infrastructure.Messaging;

public interface IMessageSubscriber
{
    IReadOnlyDictionary<Type, Func<IServiceProvider, object, Task>> Handlers { get; }
    IMessageSubscriber Subscribe<T>(Func<IServiceProvider, T, Task> handle) where T : class, IBusMessage;
}