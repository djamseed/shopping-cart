namespace ShoppingCart.Infrastructure.Persistence.Cart.CommandHandlers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using OpenCqrs.Commands;
    using OpenCqrs.Domain;
    using ShoppingCart.Core.Application.Commands;
    using ShoppingCart.Core.Domain.Cart.Exceptions;
    using ShoppingCart.Core.Domain.Cart.Models;

    public sealed class AdjustQuantityHandler : ICommandHandlerWithDomainEventsAsync<AdjustQuantity>
    {
        private readonly IRepository<Cart> repository;

        public AdjustQuantityHandler(IRepository<Cart> repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<IDomainEvent>> HandleAsync(AdjustQuantity command)
        {
            var cart = await repository.GetByIdAsync(command.AggregateRootId).ConfigureAwait(false);

            if (cart == null)
            {
                throw new CartException($"cart {command.AggregateRootId} not found");
            }

            cart.AdjustQuantity(command.ProductName, command.Quantity);

            return cart.Events;
        }
    }
}
