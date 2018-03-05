using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Domain.Common
{
    public interface IRepository<T>
    {

        void Add(T model);
        void Update(T model);
        List<T> GetAll(bool ativo);
        IEnumerable<T> Query(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeExpressions);


    }

}