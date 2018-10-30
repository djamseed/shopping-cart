namespace ShoppingCart.Application.Dtos.Responses
{
    public sealed class CartItemResponse
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
