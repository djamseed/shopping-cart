namespace ShoppingCart.Infrastructure.ReadModel.Cart.EventHandlers
{
    using System.Threading.Tasks;
    using OpenCqrs.Events;
    using ShoppingCart.Core.Domain.Cart.Events;
    using ShoppingCart.Infrastructure.ReadModel.Cart.Models;
    using ShoppingCart.Infrastructure.ReadModel.Persistence;

    public sealed class CartCreatedHandler : IEventHandlerAsync<CartCreated>
    {
        private readonly IRepository<CartReadModel> repository;

        public CartCreatedHandler(IRepository<CartReadModel> repository)
        {
            this.repository = repository;
        }

        public async Task HandleAsync(CartCreated @event)
        {
            var cart = new CartReadModel
            {
                Id = @event.AggregateRootId,
                CustomerId = @event.CustomerId
            };

            await repository.InsertAsync(cart);
            await repository.SaveChangesAsync();
        }
    }
}
