using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BLL.Interface.Interfaces
{
    public interface IService<T>
    {
        void Create(T item);
        void Update(T item);
        void Delete(T item);

        T GetById(int id);
        IEnumerable<T> GetAll();
        T GetOneByPredicate(Expression<Func<T, bool>> predicates);
        IEnumerable<T> GetAllByPredicate(Expression<Func<T, bool>> predicates);
    }
}
