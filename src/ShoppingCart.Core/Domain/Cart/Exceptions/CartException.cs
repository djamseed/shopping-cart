namespace ShoppingCart.Core.Domain.Cart.Exceptions
{
    using System;

    public sealed class CartException : Exception
    {
        public CartException(string message) : base(message)
        {
        }

        public CartException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
