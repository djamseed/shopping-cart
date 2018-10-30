namespace ShoppingCart.WebApi
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using OpenCqrs.Bus.ServiceBus.Extensions;
    using OpenCqrs.Extensions;
    using OpenCqrs.Store.EF.InMemory;
    using Swashbuckle.AspNetCore.Swagger;
    using ShoppingCart.Core.Application.Commands;
    using ShoppingCart.Application.Services;
    using ShoppingCart.Infrastructure.Persistence.Cart.CommandHandlers;
    using ShoppingCart.Infrastructure.ReadModel.Cart.Queries;
    using ShoppingCart.Infrastructure.ReadModel.Persistence;
    using ShoppingCart.WebApi.Infrastructure.Filters;
    using ShoppingCart.WebApi.Infrastructure.Middlewares.Authentication;
    using ShoppingCart.WebApi.Models;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(opt =>
            {
                opt.Filters.Add(typeof(GlobalExceptionFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddOptions();

            services.Configure<AuthenticationSetting>(Configuration.GetSection("Authentication"));

            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("Carts"));

            services.AddScoped(typeof(IRepository<>), typeof(EntityFrameworkRepository<>));

            services.AddOpenCqrs(typeof(CreateCart), typeof(CreateCartHandler), typeof(GetCart))
                    .AddInMemoryProvider(Configuration)
                    .AddServiceBusProvider(Configuration);
                    

            services.AddScoped<IShoppingCartService, ShoppingCartService>();

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new Info
                {
                    Title = "Shopping Cart API",
                    Version = "v1",
                    Description = "Shopping Cart RESTful API"
                });

                //opt.AddSecurityDefinition("AuthHeader", new ApiKeyScheme
                //{
                //    Type = "apiKey",
                //    In = "header",
                //    Name = "Authorization"
                //});

                //opt.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                //{
                //    { "apiKey", System.Linq.Enumerable.Empty<string>() }
                //});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                
            }
            else
            {
                app.UseHsts();
            }

            app.UseMvcWithDefaultRoute();
            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShoppingCart.API V1");
            });
            // app.UseApiAuthentication();
            app.UseOpenCqrs();
        }
    }
}
