namespace ShoppingCart.Infrastructure.ReadModel.Common
{
    using System;

    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
