namespace ShoppingCart.Application.Dtos.Requests
{
    public sealed class AdjustQuantityRequest
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
