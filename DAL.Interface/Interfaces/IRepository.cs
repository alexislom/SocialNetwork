using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DAL.Interfaces.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        void Create(TEntity e);
        void Update(TEntity e);
        void Delete(TEntity e);

        IEnumerable<TEntity> GetAll();
        TEntity GetById(int key);
        TEntity GetOneByPredicate(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetAllByPredicate(Expression<Func<TEntity, bool>> predicate);
    }
}