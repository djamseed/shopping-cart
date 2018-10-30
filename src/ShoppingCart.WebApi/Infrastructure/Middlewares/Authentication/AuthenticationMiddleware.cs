namespace ShoppingCart.WebApi.Infrastructure.Middlewares.Authentication
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using ShoppingCart.Application.Dtos.Responses;
    using ShoppingCart.WebApi.Models;

    public sealed class AuthenticationMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<AuthenticationMiddleware> logger;
        private readonly IOptions<AuthenticationSetting> authSettings;

        public AuthenticationMiddleware(RequestDelegate next, ILogger<AuthenticationMiddleware> logger, IOptions<AuthenticationSetting> authSettings)
        {
            this.next = next;
            this.logger = logger;
            this.authSettings = authSettings;
        }

        public async Task Invoke(HttpContext context)
        {
            ErrorMessageResponse errorMessageResponse = null;

            try
            {
                string authHeader = context.Request.Headers["Authorization"];

                var apiKey = authSettings.Value.ApiKey;

                if (authHeader != null && apiKey != null && authHeader.Equals(apiKey))
                {
                    await next.Invoke(context);
                }
                else
                {
                    context.Response.StatusCode = 401;
                    throw new UnauthorizedAccessException();
                }

            }
            catch (Exception ex)
            {
                logger.LogError(new EventId(ex.HResult), ex, ex.Message);

                errorMessageResponse = new ErrorMessageResponse
                {
                    Message = ex.Message,
                    Exception = ex,
                    StatusCode = 401
                };

                context.Response.StatusCode = 401;
            }

            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";

                var response = JsonConvert.SerializeObject(errorMessageResponse, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

                await context.Response.WriteAsync(response);
            }
        }
    }
}
