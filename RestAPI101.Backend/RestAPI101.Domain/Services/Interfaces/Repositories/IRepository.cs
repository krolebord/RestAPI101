using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RestAPI101.Domain.Entities;

namespace RestAPI101.Domain.Services
{
    public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class, IEntity
    {
        public IQueryable<TEntity> Query();

        public Task<TEntity?> GetAsync(int id);

        public Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression);

        public Task<IEnumerable<TEntity>> GetAllAsync();

        public Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression);

        public TEntity Add(TEntity entity);

        public TEntity Delete(TEntity entity);

        public Task<int> SaveChangesAsync();
    }
}
