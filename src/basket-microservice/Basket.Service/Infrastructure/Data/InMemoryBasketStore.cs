﻿using Basket.Service.Models;

namespace Basket.Service.Infrastructure.Data;

internal class InMemoryBasketStore : IBasketStore
{
    private static readonly Dictionary<string, CustomerBasket> Baskets = [];

    public CustomerBasket GetBasketByCustomerId(string customerId) =>
        Baskets.TryGetValue(customerId, out var value) ? value
        : new CustomerBasket { CustomerId = customerId };

    public void CreateCustomerBasket(CustomerBasket customerBasket) =>
   Baskets[customerBasket.CustomerId] = customerBasket;

    public void UpdateCustomerBasket(CustomerBasket customerBasket)
    {
        if (Baskets.TryGetValue(customerBasket.CustomerId, out _))
        {
            Baskets[customerBasket.CustomerId] = customerBasket;
        }
        else
        {
            CreateCustomerBasket(customerBasket);
        }
    }

    public void DeleteCustomerBasket(string customerId) => Baskets.Remove(customerId);
}
