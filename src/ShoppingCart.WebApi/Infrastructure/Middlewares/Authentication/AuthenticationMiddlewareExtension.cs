namespace ShoppingCart.WebApi.Infrastructure.Middlewares.Authentication
{
    using Microsoft.AspNetCore.Builder;

    public static class AuthenticationMiddlewareExtension
    {
        public static IApplicationBuilder UseApiAuthentication(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AuthenticationMiddleware>();
        }
    }
}
