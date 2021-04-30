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
    public class Repository<TEntity> : ReadOnlyRepository<TEntity>, IRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly RestAppContext context;

        public Repository(RestAppContext context) : base(context)
        {
            this.context = context;
        }

        public IQueryable<TEntity> Query() =>
            set.AsQueryable();

        public async Task<TEntity?> GetAsync(int id) =>
            await set.FindAsync(id);

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression) =>
            await set.FirstOrDefaultAsync(expression);

        public  async Task<IEnumerable<TEntity>> GetAllAsync() =>
            await set.ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression) =>
            await set.Where(expression).ToListAsync();

        public TEntity Add(TEntity entity) =>
            set.Add(entity).Entity;

        public TEntity Delete(TEntity entity) =>
            set.Remove(entity).Entity;

        public Task<int> SaveChangesAsync() =>
            context.SaveChangesAsync();
    }
}
