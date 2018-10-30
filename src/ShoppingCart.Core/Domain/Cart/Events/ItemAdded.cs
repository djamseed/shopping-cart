namespace ShoppingCart.Core.Domain.Cart.Events
{
    using System;
    using OpenCqrs.Domain;

    public sealed class ItemAdded : DomainEvent
    {
        public ItemAdded(Guid id, string productName, decimal price, int quantity)
        {
            AggregateRootId = id;
            ProductName = productName;
            Price = price;
            Quantity = quantity;
        }

        public string ProductName { get; }
        public decimal Price { get; }
        public int Quantity { get; }
    }
}
