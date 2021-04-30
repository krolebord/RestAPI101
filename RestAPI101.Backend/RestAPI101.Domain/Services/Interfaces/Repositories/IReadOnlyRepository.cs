using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RestAPI101.Domain.Entities;

namespace RestAPI101.Domain.Services
{
    public interface IReadOnlyRepository<TEntity> where TEntity : class, IEntity
    {
        public IQueryable<TEntity> ReadQuery();

        public Task<TEntity?> ReadAsync(int id);

        public Task<TEntity?> ReadAsync(Expression<Func<TEntity, bool>> expression);

        public Task<IEnumerable<TEntity>> ReadAllAsync();

        public Task<IEnumerable<TEntity>> ReadAllAsync(Expression<Func<TEntity, bool>> expression);
    }
}
