namespace ShoppingCart.Application.Services
{
    using System;
    using System.Threading.Tasks;
    using OpenCqrs;
    using ShoppingCart.Application.Dtos.Requests;
    using ShoppingCart.Application.Dtos.Responses;
    using ShoppingCart.Core.Application.Commands;
    using ShoppingCart.Core.Domain.Cart.Models;
    using ShoppingCart.Infrastructure.ReadModel.Cart.Models;
    using ShoppingCart.Infrastructure.ReadModel.Cart.Queries;

    public sealed class ShoppingCartService : IShoppingCartService
    {
        private readonly IDispatcher dispatcher;

        public ShoppingCartService(IDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        public async Task<CartResponse> AddItemToCart(Guid cartId, AddItemRequest request)
        {
            var command = new AddItem
            {
                AggregateRootId = cartId,
                ProductName = request.ProductName,
                Price = request.Price,
                Quantity = request.Quantity
            };

            await dispatcher.SendAndPublishAsync<AddItem, Cart>(command);

            var model = await GetCartReadModel(cartId);

            return model.ToCartResponse();
        }

        public async Task ClearCart(Guid cartId)
        {
            var command = new EmptyCart
            {
                AggregateRootId = cartId
            };

            await dispatcher.SendAndPublishAsync<EmptyCart, Cart>(command);
        }

        public async Task<CartResponse> CreateCart(CreateCartRequest request)
        {
            var command = new CreateCart
            {
                AggregateRootId = Guid.NewGuid(),
                CustomerId = request.CustomerId
            };

            await dispatcher.SendAndPublishAsync<CreateCart, Cart>(command);

            var model = await GetCartReadModel(command.AggregateRootId);

            return model.ToCartResponse();
        }

        public async Task<CartResponse> GetCart(Guid cartId)
        {
            var model = await GetCartReadModel(cartId);

            return model.ToCartResponse();
        }

        public async Task RemoveItemFromCart(Guid cartId, RemoveItemRequest request)
        {
            var command = new RemoveItem
            {
                AggregateRootId = cartId,
                ProductName = request.ProductName
            };

            await dispatcher.SendAndPublishAsync<RemoveItem, Cart>(command);
        }

        public async Task UpdateItemQuantity(Guid cartId, AdjustQuantityRequest request)
        {
            var command = new AdjustQuantity
            {
                AggregateRootId = cartId,
                ProductName = request.ProductName,
                Quantity = request.Quantity
            };

            await dispatcher.SendAndPublishAsync<AdjustQuantity, Cart>(command);
        }

        #region Helpers

        private async Task<CartReadModel> GetCartReadModel(Guid cartId)
        {
            var model = await dispatcher.GetResultAsync<GetCart, CartReadModel>(new GetCart
            {
                CartId = cartId
            });

            return model;
        }

        #endregion
    }
}
