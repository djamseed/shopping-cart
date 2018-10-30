namespace ShoppingCart.Infrastructure.Persistence.Cart.CommandHandlers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using OpenCqrs.Commands;
    using OpenCqrs.Domain;
    using ShoppingCart.Core.Application.Commands;
    using ShoppingCart.Core.Domain.Cart.Exceptions;
    using ShoppingCart.Core.Domain.Cart.Models;

    public sealed class RemoveItemHandler : ICommandHandlerWithDomainEventsAsync<RemoveItem>
    {
        public RemoveItemHandler(IRepository<Cart> repository)
        {
            Repository = repository;
        }

        public IRepository<Cart> Repository { get; }

        public async Task<IEnumerable<IDomainEvent>> HandleAsync(RemoveItem command)
        {
            var cart = await Repository.GetByIdAsync(command.AggregateRootId).ConfigureAwait(false);

            if (cart == null)
            {
                throw new CartException($"cart {command.AggregateRootId} not found");
            }

            cart.RemoveItem(command.ProductName);

            return cart.Events;
        }
    }
}
