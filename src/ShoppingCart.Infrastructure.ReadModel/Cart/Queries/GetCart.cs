namespace ShoppingCart.Infrastructure.ReadModel.Cart.Queries
{
    using System;
    using OpenCqrs.Queries;

    public sealed class GetCart : IQuery
    {
        public Guid CartId { get; set; }
    }
}
