using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RestAPI101.Data.Context;
using RestAPI101.Domain.Models;
using RestAPI101.Domain.Services;

namespace RestAPI101.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly RestAppContext _context;
        private readonly DbSet<TEntity> _set;

        public Repository(RestAppContext context) {
            this._context = context;
            this._set = this._context.Set<TEntity>();
        }

        public int SaveChanges() =>
            _context.SaveChanges();

        public IQueryable<TEntity> Query() =>
            _set.AsQueryable();

        public TEntity? GetById(int id) =>
            _set.Find(id);

        public TEntity? Get(Expression<Func<TEntity, bool>> expression) =>
            _set.FirstOrDefault(expression);

        public TEntity Add(TEntity entity) =>
            _set.Add(entity).Entity;

        public TEntity Update(TEntity entity) =>
            _set.Update(entity).Entity;

        public TEntity Delete(TEntity entity) =>
            _set.Remove(entity).Entity;
    }
}
