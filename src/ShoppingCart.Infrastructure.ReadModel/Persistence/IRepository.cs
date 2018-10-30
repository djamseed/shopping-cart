namespace ShoppingCart.Infrastructure.ReadModel.Persistence
{
    using System;
    using System.Threading.Tasks;
    using ShoppingCart.Infrastructure.ReadModel.Common;

    public interface IRepository<T> : IReadOnlyRepository<T> where T : IEntity
    {
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task SaveChangesAsync();
    }
}
