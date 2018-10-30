namespace ShoppingCart.Core.Application.Commands
{
    using OpenCqrs.Domain;

    public sealed class AdjustQuantity : DomainCommand
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
