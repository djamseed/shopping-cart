namespace ShoppingCart.Infrastructure.ReadModel.Cart.EventHandlers
{
    using System;
    using System.Threading.Tasks;
    using OpenCqrs.Events;
    using ShoppingCart.Core.Domain.Cart.Events;
    using ShoppingCart.Infrastructure.ReadModel.Cart.Models;
    using ShoppingCart.Infrastructure.ReadModel.Persistence;

    public sealed class ItemAddedHandler : IEventHandlerAsync<ItemAdded>
    {
        private readonly IRepository<CartReadModel> repository;

        public ItemAddedHandler(IRepository<CartReadModel> repository)
        {
            this.repository = repository;
        }

        public async Task HandleAsync(ItemAdded @event)
        {
            var cart = await repository.GetByIdAsync(@event.AggregateRootId, c => c.CartItems);

            if (cart != null)
            {
                cart.CartItems.Add(new CartItemReadModel
                {
                    Id = Guid.NewGuid(),
                    CartReadModelId = cart.Id,
                    ProductName = @event.ProductName,
                    Price = @event.Price,
                    Quantity = @event.Quantity
                });
            }

            await repository.SaveChangesAsync();
        }
    }
}
