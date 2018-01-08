using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage;

namespace Domain.Repositories {
    public interface IRepository<T> {

        void Add(T model);
        void Update(T model);
        List<T> GetAll(bool ativo);
        IDbContextTransaction BeginTransaction();
        void SaveChanges();

    }

}