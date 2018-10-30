namespace ShoppingCart.Infrastructure.ReadModel.Cart.Queries
{
    using System.Threading.Tasks;
    using OpenCqrs.Queries;
    using ShoppingCart.Infrastructure.ReadModel.Cart.Models;
    using ShoppingCart.Infrastructure.ReadModel.Persistence;

    public sealed class GetCartHandler : IQueryHandlerAsync<GetCart, CartReadModel>
    {
        private readonly IReadOnlyRepository<CartReadModel> repository;

        public GetCartHandler(IReadOnlyRepository<CartReadModel> repository)
        {
            this.repository = repository;
        }

        public async Task<CartReadModel> RetrieveAsync(GetCart query)
        {
            return await repository.GetByIdAsync(query.CartId, x => x.CartItems);
        }
    }
}
