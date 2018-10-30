namespace ShoppingCart.Infrastructure.ReadModel.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using ShoppingCart.Infrastructure.ReadModel.Cart.Models;

    public sealed class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<CartReadModel> Carts { get; set; }
    }
}
