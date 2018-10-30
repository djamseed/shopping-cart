namespace ShoppingCart.Application.Dtos.Requests
{
    public sealed class AddItemRequest
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
