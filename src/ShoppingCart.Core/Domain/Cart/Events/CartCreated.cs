namespace ShoppingCart.Core.Domain.Cart.Events
{
    using System;
    using OpenCqrs.Domain;

    public sealed class CartCreated : DomainEvent
    {
        public CartCreated(Guid id, Guid customerId)
        {
            AggregateRootId = id;
            CustomerId = customerId;
        }

        public Guid CustomerId { get; }
    }
}
