namespace ShoppingCart.Application.Services
{
    using System;
    using System.Threading.Tasks;
    using ShoppingCart.Application.Dtos.Responses;
    using ShoppingCart.Infrastructure.ReadModel.Cart.Models;

    public static class Extensions
    {
        public static CartResponse ToCartResponse(this CartReadModel cartReadModel)
        {
            var response = new CartResponse
            {
                CartId = cartReadModel.Id,
                CustomerId = cartReadModel.CustomerId,
                TotalPrice = cartReadModel.TotalPrice
            };

            foreach (var item in cartReadModel.CartItems)
            {
                response.Items.Add(new CartItemResponse
                {
                    ProductName = item.ProductName,
                    Price = item.Price,
                    Quantity = item.Quantity
                });
            }

            return response;
        }
    }
}
