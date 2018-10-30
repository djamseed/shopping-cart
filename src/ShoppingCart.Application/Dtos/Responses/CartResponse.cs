namespace ShoppingCart.Application.Dtos.Responses
{
    using System;
    using System.Collections.Generic;

    public sealed class CartResponse
    {
        public Guid CartId { get; set; }
        public Guid CustomerId { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<CartItemResponse> Items { get; set; } = new List<CartItemResponse>();
    }
}
