﻿namespace ProductDetails.Domain.Messaging;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class MessageAttribute(string exchangeName) : Attribute
{
    public string ExchangeName { get; } = exchangeName;
}
