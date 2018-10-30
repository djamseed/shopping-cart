namespace ShoppingCart.Client.Tests
{
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using ShoppingCart.Client;
    using ShoppingCart.Client.Models;
    using ShoppingCart.WebApi;
    using Xunit;

    public sealed class ClientTests
    {
        private const string Version = "1";
        private const string BaseUrl = "http://localhost:5000";
        private ShoppingCartAPI Client { get; }


        public ClientTests()
        {
            Client = new ShoppingCartAPI(new Uri(BaseUrl));
        }

        [Fact]
        public void CreateCart()
        {
            using (var host = new WebHostBuilder()
                   .UseUrls(BaseUrl)
                   .UseKestrel()
                   .UseStartup<Startup>()
                   .Build())
            {
                host.Start();

                var request = new CreateCartRequest(Guid.NewGuid());

                var cart = Client.CreateCartWithHttpMessagesAsync(Version, request);

                Assert.Equal(HttpStatusCode.Created, cart.Result.Response.StatusCode);
            }
        }

        [Fact]
        public void GetCart()
        {
            using (var host = new WebHostBuilder()
                   .UseUrls(BaseUrl)
                   .UseKestrel()
                   .UseStartup<Startup>()
                   .Build())
            {
                host.Start();

                var cart = Client.GetCartByIdWithHttpMessagesAsync(Guid.NewGuid(), Version);

                Assert.Equal(HttpStatusCode.OK, cart.Result.Response.StatusCode);
            }
        }

        [Fact]
        public void AddItemToCart()
        {
            using (var host = new WebHostBuilder()
                   .UseUrls(BaseUrl)
                   .UseKestrel()
                   .UseStartup<ShoppingCart.WebApi.Startup>()
                   .Build())
            {
                host.Start();


                var request = new AddItemRequest("iPhone X", 1000, 1);

                var cart = Client.AddItemToCartWithHttpMessagesAsync(Guid.NewGuid(), Version, request);

                Assert.Equal(HttpStatusCode.Created, cart.Result.Response.StatusCode);
            }
        }

        [Fact]
        public void UpdateItemQuantity()
        {
            using (var host = new WebHostBuilder()
                   .UseUrls(BaseUrl)
                   .UseKestrel()
                   .UseStartup<Startup>()
                   .Build())
            {
                host.Start();

                var request = new AdjustQuantityRequest("iPhone X", 2);

                var cart = Client.UpdateItemQuantityWithHttpMessagesAsync(Guid.NewGuid(), Version, request);

                Assert.Equal(HttpStatusCode.NoContent, cart.Result.Response.StatusCode);
            }
        }

        [Fact]
        public void RemoveItemFromCart()
        {
            using (var host = new WebHostBuilder()
                   .UseUrls(BaseUrl)
                   .UseKestrel()
                   .UseStartup<Startup>()
                   .Build())
            {
                host.Start();

                var request = new RemoveItemRequest("iPhone X");

                var cart = Client.RemoveItemFromCartWithHttpMessagesAsync(Guid.NewGuid(), Version, request);

                Assert.Equal(HttpStatusCode.NoContent, cart.Result.Response.StatusCode);
            }
        }

        [Fact]
        public void ClearCart()
        {
            using (var host = new WebHostBuilder()
                   .UseUrls(BaseUrl)
                   .UseKestrel()
                   .UseStartup<Startup>()
                   .Build())
            {
                host.Start();

                var cart = Client.ClearCartWithHttpMessagesAsync(Guid.NewGuid(), Version);

                Assert.Equal(HttpStatusCode.NoContent, cart.Result.Response.StatusCode);

            }
        }

    }
}
