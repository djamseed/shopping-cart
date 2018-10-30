namespace ShoppingCart.WebApi.Infrastructure.Filters
{
    using System.Net;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;
    using ShoppingCart.Application.Dtos.Responses;
    using ShoppingCart.Core.Domain.Cart.Exceptions;

    public sealed class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment env;
        private readonly ILogger<GlobalExceptionFilter> logger;

        public GlobalExceptionFilter(IHostingEnvironment env, ILogger<GlobalExceptionFilter> logger)
        {
            this.env = env;
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            logger.LogError(new EventId(context.Exception.HResult), context.Exception, context.Exception.Message);

            if (context.Exception is CartException)
            {
                var errorResponse = new ErrorMessageResponse
                {
                    Message = context.Exception.Message,
                    StatusCode = (int)HttpStatusCode.BadRequest
                };

                context.Result = new BadRequestObjectResult(errorResponse);
                context.HttpContext.Response.StatusCode = errorResponse.StatusCode;
            }
            else
            {
                var errorResponse = new ErrorMessageResponse
                {
                    Message = "An unexpected error has occured.  Please try again later.",
                };

                if (env.IsDevelopment())
                {
                    errorResponse.Exception = context.Exception;
                }

                errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;

                context.Result = new ObjectResult(errorResponse);
                context.HttpContext.Response.StatusCode = errorResponse.StatusCode;
            }

            context.ExceptionHandled = true;
        }
    }
}
