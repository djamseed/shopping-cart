namespace ShoppingCart.Core.Application.Commands
{
    using OpenCqrs.Domain;

    public sealed class RemoveItem : DomainCommand
    {
        public string ProductName { get; set; }
    }
}
