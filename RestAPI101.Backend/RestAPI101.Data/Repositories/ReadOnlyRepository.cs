using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestAPI101.Data.Context;
using RestAPI101.Domain.Entities;
using RestAPI101.Domain.Services;

namespace RestAPI101.Data.Repositories
{
    public class ReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly DbSet<TEntity> set;

        public ReadOnlyRepository(RestAppContext context)
        {
            this.set = context.Set<TEntity>();
        }

        public IQueryable<TEntity> ReadQuery() =>
            set.AsNoTrackingWithIdentityResolution();

        public async Task<TEntity?> ReadAsync(int id) =>
            await ReadQuery().FirstOrDefaultAsync(entity => entity.Id == id);

        public async Task<TEntity?> ReadAsync(Expression<Func<TEntity, bool>> expression) =>
            await ReadQuery().FirstOrDefaultAsync(expression);

        public async Task<IEnumerable<TEntity>> ReadAllAsync() =>
            await ReadQuery().ToListAsync();

        public async Task<IEnumerable<TEntity>> ReadAllAsync(Expression<Func<TEntity, bool>> expression) =>
            await ReadQuery().Where(expression).ToListAsync();
    }
}
