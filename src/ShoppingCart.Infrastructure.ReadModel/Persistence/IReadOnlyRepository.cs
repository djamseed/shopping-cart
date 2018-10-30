namespace ShoppingCart.Infrastructure.ReadModel.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using ShoppingCart.Infrastructure.ReadModel.Common;

    public interface IReadOnlyRepository<T> where T : IEntity
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null);
        Task<T> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] properties);
    }
}
