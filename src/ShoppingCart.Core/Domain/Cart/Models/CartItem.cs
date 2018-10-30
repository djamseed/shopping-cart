namespace ShoppingCart.Core.Domain.Cart.Models
{
    public sealed class CartItem
    {
        public CartItem(string productName, decimal price, int quantity)
        {
            ProductName = productName;
            Price = price;
            Quantity = quantity;
        }

        public string ProductName { get; }
        public decimal Price { get; }
        public int Quantity { get; }

        internal CartItem WithQuantity(int quantity)
        {
            return new CartItem(ProductName, Price, quantity);
        }
    }
}
