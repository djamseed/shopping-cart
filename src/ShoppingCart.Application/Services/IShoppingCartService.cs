namespace ShoppingCart.Application.Services
{
    using System;
    using System.Threading.Tasks;
    using ShoppingCart.Application.Dtos.Requests;
    using ShoppingCart.Application.Dtos.Responses;

    public interface IShoppingCartService
    {
        Task<CartResponse> CreateCart(CreateCartRequest request);
        Task<CartResponse> GetCart(Guid cartId);
        Task<CartResponse> AddItemToCart(Guid cartId, AddItemRequest request);
        Task UpdateItemQuantity(Guid cartId, AdjustQuantityRequest request);
        Task RemoveItemFromCart(Guid cartId, RemoveItemRequest request);
        Task ClearCart(Guid cartId);
    }
}
