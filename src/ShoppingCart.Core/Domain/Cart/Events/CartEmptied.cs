namespace ShoppingCart.Core.Domain.Cart.Events
{
    using System;
    using OpenCqrs.Domain;

    public sealed class CartEmptied : DomainEvent
    {
        public CartEmptied(Guid id)
        {
            AggregateRootId = id;
        }
    }
}
