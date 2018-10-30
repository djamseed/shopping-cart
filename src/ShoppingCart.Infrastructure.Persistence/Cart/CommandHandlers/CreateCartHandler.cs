namespace ShoppingCart.Infrastructure.Persistence.Cart.CommandHandlers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using OpenCqrs.Commands;
    using OpenCqrs.Domain;
    using ShoppingCart.Core.Application.Commands;
    using ShoppingCart.Core.Domain.Cart.Models;

    public sealed class CreateCartHandler : ICommandHandlerWithDomainEventsAsync<CreateCart>
    {
        private readonly IRepository<Cart> repository;

        public CreateCartHandler(IRepository<Cart> repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<IDomainEvent>> HandleAsync(CreateCart command)
        {
            var cart = new Cart(command.AggregateRootId, command.CustomerId);

            await repository.SaveAsync(cart).ConfigureAwait(false);

            return cart.Events;
        }
    }
}
