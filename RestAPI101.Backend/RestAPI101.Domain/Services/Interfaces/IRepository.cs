using System;
using System.Linq;
using System.Linq.Expressions;
using RestAPI101.Domain.Models;

namespace RestAPI101.Domain.Services
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        public int SaveChanges();

        public IQueryable<TEntity> Query();

        public TEntity? GetById(int id);

        public TEntity? Get(Expression<Func<TEntity, bool>> expression);

        public TEntity Add(TEntity entity);

        public TEntity Update(TEntity entity);

        public TEntity Delete(TEntity entity);
    }
}
