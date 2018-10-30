namespace ShoppingCart.Core.Domain.Cart.Events
{
    using System;
    using OpenCqrs.Domain;

    public sealed class ItemRemoved : DomainEvent
    {
        public ItemRemoved(Guid id, string productName)
        {
            AggregateRootId = id;
            ProductName = productName;
        }

        public string ProductName { get; }
    }
}
