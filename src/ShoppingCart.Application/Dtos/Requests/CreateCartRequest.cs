namespace ShoppingCart.Application.Dtos.Requests
{
    using System;

    public sealed class CreateCartRequest
    {
        public Guid CustomerId { get; set; }
    }
}
