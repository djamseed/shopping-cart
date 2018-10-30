namespace ShoppingCart.Infrastructure.ReadModel.Cart.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ShoppingCart.Infrastructure.ReadModel.Common;

    public class CartReadModel : IEntity
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public ICollection<CartItemReadModel> CartItems { get; set; } = new List<CartItemReadModel>();

        public decimal TotalPrice => CartItems.Sum(x => x.Price * x.Quantity);
    }
}
