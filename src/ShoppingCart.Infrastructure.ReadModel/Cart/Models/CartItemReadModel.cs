namespace ShoppingCart.Infrastructure.ReadModel.Cart.Models
{
    using System;
    using ShoppingCart.Infrastructure.ReadModel.Common;

    public class CartItemReadModel : IEntity
    {
        public Guid Id { get; set; }
        public Guid CartReadModelId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public CartReadModel Cart { get; set; }
    }
}
