namespace ShoppingCart.Core.Domain.Cart.Events
{
    using System;
    using OpenCqrs.Domain;

    public sealed class QuantityAdjusted : DomainEvent
    {
        public QuantityAdjusted(Guid id, string productName, int quantity)
        {
            AggregateRootId = id;
            ProductName = productName;
            Quantity = quantity;
        }

        public string ProductName { get; }
        public int Quantity { get; }
    }
}
