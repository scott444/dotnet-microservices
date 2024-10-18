﻿using ECommerce.Shared.Infrastructure.EventBus;
using ECommerce.Shared.Infrastructure.EventBus.Abstractions;
using System.Text.Json;

namespace ECommerce.Shared.Infrastructure.RabbitMq;
public class RabbitMqEventBus(IRabbitMqConnection rabbitMqConnection) : IEventBus
{
    private const string ExchangeName = "ecommerce-exchange";

    private readonly IRabbitMqConnection _rabbitMqConnection = rabbitMqConnection;

    public Task PublishAsync(Event @event)
    {
        var routingKey = @event.GetType().Name;

        using var channel = _rabbitMqConnection.Connection.CreateModel();

        channel.ExchangeDeclare(
            exchange: ExchangeName,
            type: "fanout",
            durable: false,
            autoDelete: false,
            null);

        var body = JsonSerializer.SerializeToUtf8Bytes(@event, @event.GetType());

        channel.BasicPublish(
            exchange: ExchangeName,
            routingKey: routingKey,
            mandatory: false,
            basicProperties: null,
            body: body);

        return Task.CompletedTask;
    }
}
