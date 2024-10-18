using Basket.Service.Infrastructure.Data;
using ECommerce.Shared.Infrastructure.EventBus.Abstractions;

namespace Basket.Service.IntegrationEvents.EventHandlers;

internal class OrderCreatedEventHandler(IBasketStore basketStore) : IEventHandler<OrderCreatedEvent>
{
    private readonly IBasketStore _basketStore = basketStore;

    public Task Handle(OrderCreatedEvent @event)
    {
        _basketStore.DeleteCustomerBasket(@event.CustomerId);

        return Task.CompletedTask;
    }
}
