namespace ShoppingCart.Infrastructure.Persistence.Cart.CommandHandlers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using OpenCqrs.Commands;
    using OpenCqrs.Domain;
    using ShoppingCart.Core.Application.Commands;
    using ShoppingCart.Core.Domain.Cart.Exceptions;
    using ShoppingCart.Core.Domain.Cart.Models;

    public sealed class EmptyCartHandler : ICommandHandlerWithDomainEventsAsync<EmptyCart>
    {
        public EmptyCartHandler(IRepository<Cart> repository)
        {
            Repository = repository;
        }

        public IRepository<Cart> Repository { get; }

        public async Task<IEnumerable<IDomainEvent>> HandleAsync(EmptyCart command)
        {
            var cart = await Repository.GetByIdAsync(command.AggregateRootId).ConfigureAwait(false);

            if (cart == null)
            {
                throw new CartException($"cart {command.AggregateRootId} not found");
            }

            cart.Clear();

            return cart.Events;
        }
    }
}
