namespace ShoppingCart.Core.Application.Commands
{
    using OpenCqrs.Domain;

    public sealed class AddItem : DomainCommand
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
