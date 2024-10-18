using Basket.Service.Endpoints;
using Basket.Service.Infrastructure.Data;
using Basket.Service.IntegrationEvents;
using Basket.Service.IntegrationEvents.EventHandlers;
using ECommerce.Shared.Infrastructure.EventBus;
using ECommerce.Shared.Infrastructure.RabbitMq;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IBasketStore, InMemoryBasketStore>();

builder.Services.AddRabbitMqEventBus(builder.Configuration)
    .AddRabbitMqSubscriberService(builder.Configuration)
    .AddEventHandler<OrderCreatedEvent, OrderCreatedEventHandler>();

var app = builder.Build();

app.RegisterEndpoints();

await app.RunAsync();
