namespace ShoppingCart.Infrastructure.ReadModel.Cart.EventHandlers
{
    using System.Linq;
    using System.Threading.Tasks;
    using OpenCqrs.Events;
    using ShoppingCart.Core.Domain.Cart.Events;
    using ShoppingCart.Infrastructure.ReadModel.Cart.Models;
    using ShoppingCart.Infrastructure.ReadModel.Persistence;

    public sealed class QuantityAdjustedHandler : IEventHandlerAsync<QuantityAdjusted>
    {
        private readonly IRepository<CartReadModel> repository;

        public QuantityAdjustedHandler(IRepository<CartReadModel> repository)
        {
            this.repository = repository;
        }

        public async Task HandleAsync(QuantityAdjusted @event)
        {
            var cart = await repository.GetByIdAsync(@event.AggregateRootId, c => c.CartItems);

            if (cart != null)
            {
                var item = cart.CartItems.SingleOrDefault(x => x.ProductName == @event.ProductName);

                if (item != null)
                {
                    item.Quantity = @event.Quantity;

                    await repository.UpdateAsync(cart);
                    await repository.SaveChangesAsync();
                }
            }
        }
    }
}
