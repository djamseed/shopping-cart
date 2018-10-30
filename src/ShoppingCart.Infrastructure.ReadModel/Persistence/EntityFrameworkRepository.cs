namespace ShoppingCart.Infrastructure.ReadModel.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using ShoppingCart.Infrastructure.ReadModel.Common;

    public sealed class EntityFrameworkRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly ApplicationDbContext context;

        public EntityFrameworkRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null)
        {
            IQueryable<T> query = Table();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await Task.FromResult(query.AsNoTracking());
        }

        public async Task<T> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] properties)
        {
            var query = Table().Include(properties.First());

            foreach (var prop in properties.Skip(1))
            {
                query = query.Include(prop);
            }

            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task InsertAsync(T entity)
        {
            await Table().AddAsync(entity);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            Table().Update(entity);

            await Task.CompletedTask;
        }

        private DbSet<T> Table() => context.Set<T>();
    }
}
