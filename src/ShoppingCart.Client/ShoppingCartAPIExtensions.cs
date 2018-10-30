// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace ShoppingCart.Client
{
    using Models;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods for ShoppingCartAPI.
    /// </summary>
    public static partial class ShoppingCartAPIExtensions
    {
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='version'>
            /// </param>
            /// <param name='request'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task CreateCartAsync(this IShoppingCartAPI operations, string version, CreateCartRequest request = default(CreateCartRequest), CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.CreateCartWithHttpMessagesAsync(version, request, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cartId'>
            /// </param>
            /// <param name='version'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<CartResponse> GetCartAsync(this IShoppingCartAPI operations, System.Guid cartId, string version, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetCartByIdWithHttpMessagesAsync(cartId, version, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cartId'>
            /// </param>
            /// <param name='version'>
            /// </param>
            /// <param name='request'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task ChangeItemQuantityAsync(this IShoppingCartAPI operations, System.Guid cartId, string version, AdjustQuantityRequest request = default(AdjustQuantityRequest), CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.UpdateItemQuantityWithHttpMessagesAsync(cartId, version, request, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cartId'>
            /// </param>
            /// <param name='version'>
            /// </param>
            /// <param name='request'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<CartResponse> AddItemToCartAsync(this IShoppingCartAPI operations, System.Guid cartId, string version, AddItemRequest request = default(AddItemRequest), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.AddItemToCartWithHttpMessagesAsync(cartId, version, request, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cartId'>
            /// </param>
            /// <param name='version'>
            /// </param>
            /// <param name='request'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task RemoveItemFromCartAsync(this IShoppingCartAPI operations, System.Guid cartId, string version, RemoveItemRequest request = default(RemoveItemRequest), CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.RemoveItemFromCartWithHttpMessagesAsync(cartId, version, request, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cartId'>
            /// </param>
            /// <param name='version'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task ClearCartAsync(this IShoppingCartAPI operations, System.Guid cartId, string version, CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.ClearCartWithHttpMessagesAsync(cartId, version, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

    }
}