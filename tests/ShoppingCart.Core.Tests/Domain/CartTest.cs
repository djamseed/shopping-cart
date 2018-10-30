namespace ShoppingCart.Core.Tests.Domain
{
    using System;
    using System.Linq;
    using Xunit;
    using ShoppingCart.Core.Domain.Cart.Exceptions;
    using ShoppingCart.Core.Domain.Cart.Models;
    using ShoppingCart.Core.Domain.Cart.Events;

    public class CartTest
    {
        private static readonly Guid CartId = Guid.NewGuid();
        private static readonly Guid CustomerId = Guid.NewGuid();
        private const string ProductName = "Nikon D610";

        [Fact]
        public void GivenNoCartExistsWhenCreatingOneForCustomerThatDoesNotExistsThenThrowsCartException()
        {
            var customerId = Guid.Empty;

            Assert.Throws<CartException>(() => new Cart(CartId, customerId));
        }

        [Fact]
        public void GivenNoCartExistsForCustomerWhenCreatedOneThenCreated()
        {
            var cart = new Cart(CartId, CustomerId);

            var @event = cart.Events.OfType<CartCreated>().Single();

            Assert.Equal(CartId, @event.AggregateRootId);
            Assert.Equal(CustomerId, @event.CustomerId);
            Assert.Equal(0, cart.CartItems.Count);
        }

        [Fact]
        public void GivenACartWhenAddItemWithQuantityBelowOneThenThrowsCartException()
        {
            var cart = new Cart(CartId, CustomerId);

            Assert.Throws<CartException>(() => cart.AddItem("Go sticker", 0.99m, 0));
        }

        [Fact]
        public void GivenACartWhenAddItemThenItemAdded()
        {
            var cart = new Cart(CartId, CustomerId);

            cart.AddItem(ProductName, 1000m, 1);

            var @event = cart.Events.OfType<ItemAdded>().Single();

            Assert.Equal(ProductName, @event.ProductName);
            Assert.Equal(1000m, @event.Price);
            Assert.Equal(1, @event.Quantity);
            Assert.Single(cart.CartItems);
        }

        [Fact]
        public void GivenACartWithAnItemWhenAddingSameItemThenThrowsCartException()
        {
            var cart = new Cart(CartId, CustomerId);

            cart.AddItem(ProductName, 1000m, 1);

            Assert.Throws<CartException>(() => cart.AddItem(ProductName, 1000m, 1));
        }

        [Fact]
        public void GivenACartWhenAdjustingQuantityForItemNotInCartThenThrowsCartException()
        {
            var cart = new Cart(CartId, CustomerId);

            cart.AddItem(ProductName, 1000m, 1);

            Assert.Throws<CartException>(() => cart.AdjustQuantity("iPhone X", 2));
        }



        [Fact]
        public void GivenACartWithAnItemWhenAdjustingQuantityBelowOneThenThrowsCartException()
        {
            var cart = new Cart(CartId, CustomerId);

            cart.AddItem(ProductName, 1000m, 1);

            Assert.Throws<CartException>(() => cart.AdjustQuantity(ProductName, 0));
        }

        [Fact]
        public void GivenACartWithAnItemWhenAdjustingQuantityThenQuantityAdjusted()
        {
            var cart = new Cart(CartId, CustomerId);

            cart.AddItem(ProductName, 1000m, 1);

            cart.AdjustQuantity(ProductName, 2);

            var @event = cart.Events.OfType<QuantityAdjusted>().Single();

            Assert.Equal(ProductName, @event.ProductName);
            Assert.Equal(2, @event.Quantity);
        }

        [Fact]
        public void GivenACartWhenRemovingItemNotInCartThenThrowsCartException()
        {
            var cart = new Cart(CartId, CustomerId);

            cart.AddItem(ProductName, 1000m, 1);

            Assert.Throws<CartException>(() => cart.RemoveItem("Macbook Pro"));
        }

        [Fact]
        public void GivenACartWithItemWhenRemovingItemThenItemRemoved()
        {
            var cart = new Cart(CartId, CustomerId);

            cart.AddItem(ProductName, 1000m, 1);

            cart.RemoveItem(ProductName);

            var @event = cart.Events.OfType<ItemRemoved>().Single();

            Assert.Equal(ProductName, @event.ProductName);
        }

        [Fact]
        public void GivenACartWithItemsWhenClearingCartThenCartEmptied()
        {
            var cart = new Cart(CartId, CustomerId);

            cart.AddItem(ProductName, 1000m, 1);
            cart.AddItem("Nikkor 50mm f/1.4", 500m, 1);

            cart.Clear();

            var @event = cart.Events.OfType<CartEmptied>().Single();

            Assert.Equal(CartId, @event.AggregateRootId);
            Assert.Equal(0, cart.CartItems.Count);
        }
    }
}
