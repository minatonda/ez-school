using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Domain.Repositories
{
    public interface IRepository<T>
    {

        T Add(T model);
        T Update(T model);
        void Disable(long ID);
        T Get(long ID);
        List<T> GetAll(bool? ativo);
        IEnumerable<T> Query(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeExpressions);


    }

}