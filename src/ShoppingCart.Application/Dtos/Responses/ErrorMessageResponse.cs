namespace ShoppingCart.Application.Dtos.Responses
{
    using System;

    public sealed class ErrorMessageResponse
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public Exception Exception { get; set; }
    }
}
