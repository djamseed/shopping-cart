namespace ShoppingCart.Core.Domain.Cart.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using OpenCqrs.Domain;
    using ShoppingCart.Core.Domain.Cart.Events;
    using ShoppingCart.Core.Domain.Cart.Exceptions;

    public sealed class Cart : AggregateRoot
    {
        #region Constructors

        public Cart()
        {
        }

        public Cart(Guid id, Guid customerId) : base(id)
        {
            if (customerId == Guid.Empty)
            {
                throw new CartException("customer should have a valid id");
            }

            AddAndApplyEvent(new CartCreated(Id, customerId));
        }

        #endregion

        #region Properties

        public Guid CustomerId { get; private set; }
        public ICollection<CartItem> CartItems { get; } = new List<CartItem>();

        #endregion

        #region Command Processors

        public void AddItem(string productName, decimal price, int quantity)
        {
            if (string.IsNullOrEmpty(productName))
            {
                throw new ArgumentNullException(nameof(productName), "product name is required");
            }

            if (ContainsItem(productName))
            {
                throw new CartException($"product {productName} is already in cart");
            }

            ValidateQuantity(quantity);

            AddAndApplyEvent(new ItemAdded(Id, productName, price, quantity));
        }

        public void AdjustQuantity(string productName, int quantity)
        {
            if (string.IsNullOrEmpty(productName))
            {
                throw new ArgumentNullException(nameof(productName), "product name is required");
            }

            if (!ContainsItem(productName))
            {
                throw new CartException($"product {productName} not in cart");
            }

            ValidateQuantity(quantity);

            AddAndApplyEvent(new QuantityAdjusted(Id, productName, quantity));
        }

        public void RemoveItem(string productName)
        {
            if (string.IsNullOrEmpty(productName))
            {
                throw new ArgumentNullException(nameof(productName), "product name is required");
            }

            if (!ContainsItem(productName))
            {
                throw new CartException($"product {productName} not in cart");
            }

            AddAndApplyEvent(new ItemRemoved(Id, productName));
        }

        public void Clear()
        {
            if (CartItems.Count > 0)
            {
                AddAndApplyEvent(new CartEmptied(Id));
            }
        }

        #endregion

        #region Apply Methods

        internal void Apply(CartCreated @event)
        {
            Id = @event.AggregateRootId;
            CustomerId = @event.CustomerId;
        }

        internal void Apply(ItemAdded @event)
        {
            Id = @event.AggregateRootId;
            CartItems.Add(new CartItem(@event.ProductName, @event.Price, @event.Quantity));
        }

        internal void Apply(QuantityAdjusted @event)
        {
            Id = @event.AggregateRootId;
            var item = GetCartItem(@event.ProductName);
            CartItems.Remove(item);
            CartItems.Add(item.WithQuantity(@event.Quantity));
        }

        internal void Apply(ItemRemoved @event)
        {
            Id = @event.AggregateRootId;
            var item = GetCartItem(@event.ProductName);
            CartItems.Remove(item);
        }

        internal void Apply(CartEmptied @event)
        {
            Id = @event.AggregateRootId;
            CartItems.Clear();
        }

        #endregion

        #region Helpers

        private CartItem GetCartItem(string productName) => CartItems.Single(x => x.ProductName == productName);

        private bool ContainsItem(string productName) => CartItems.Any(x => x.ProductName == productName);

        private void ValidateQuantity(int quantity)
        {
            if (quantity <= 0)
            {
                throw new CartException("quantity should be greater than zero");
            }
        }

        #endregion
    }
}
