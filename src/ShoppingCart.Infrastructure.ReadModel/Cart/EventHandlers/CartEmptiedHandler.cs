namespace ShoppingCart.Infrastructure.ReadModel.Cart.EventHandlers
{
    using System.Threading.Tasks;
    using OpenCqrs.Events;
    using ShoppingCart.Core.Domain.Cart.Events;
    using ShoppingCart.Infrastructure.ReadModel.Cart.Models;
    using ShoppingCart.Infrastructure.ReadModel.Persistence;

    public sealed class CartEmptiedHandler : IEventHandlerAsync<CartEmptied>
    {
        private readonly IRepository<CartReadModel> repository;

        public CartEmptiedHandler(IRepository<CartReadModel> repository)
        {
            this.repository = repository;
        }

        public async Task HandleAsync(CartEmptied @event)
        {
            var cart = await repository.GetByIdAsync(@event.AggregateRootId, c => c.CartItems);

            if (cart != null)
            {
                if (cart.CartItems.Count > 0)
                {
                    cart.CartItems.Clear();
                    await repository.SaveChangesAsync();
                }
            }
        }
    }
}
