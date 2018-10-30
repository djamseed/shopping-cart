namespace ShoppingCart.Core.Application.Commands
{
    using System;
    using OpenCqrs.Domain;

    public sealed class CreateCart : DomainCommand
    {
        public Guid CustomerId { get; set; }
    }
}
