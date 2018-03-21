using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Domain.CategoriaProfissionalDomain  {

    public class CategoriaProfissionalRepository : IRepository<CategoriaProfissional> {

        private BaseContext db;

        public CategoriaProfissionalRepository(BaseContext db) {
            this.db = db;
        }

        public CategoriaProfissionalRepository() {
        }

        public void Add(CategoriaProfissional categoriaProfissional) {
            this.db.CtgPrfsn.Add(categoriaProfissional);
        }

        public void AddHistoryCategoriaProfissional(long id) {
            var history = this.Get(id);
            history.ID = 0;
            history.Ativo = DateTime.Now;
            this.Add(history);
        }

        public void Update(CategoriaProfissional categoriaProfissional) {
            var model = this.db.CtgPrfsn.Find(categoriaProfissional.ID);

            model.Nome = categoriaProfissional.Nome;
            model.Descricao = categoriaProfissional.Descricao;

            this.db.CtgPrfsn.Update(model);
        }
        public void Disable(long ID) {
            var model = this.db.CtgPrfsn.Find(ID);

            model.Ativo = DateTime.Now;
            this.db.CtgPrfsn.Update(model);
        }

        public CategoriaProfissional Get(long ID) => this.db.CtgPrfsn.Find(ID);

        public List<CategoriaProfissional> GetAll(bool ativo) {
            if (ativo) {
                return this.db.CtgPrfsn.Where(x => !x.Ativo.HasValue).ToList();
            } else {
                return this.db.CtgPrfsn.ToList();
            }
        }

        public IEnumerable<CategoriaProfissional> Query(Expression<Func<CategoriaProfissional, bool>> predicate, params Expression<Func<CategoriaProfissional, object>>[] includeExpressions) {
            return includeExpressions.Aggregate<Expression<Func<CategoriaProfissional, object>>, IQueryable<CategoriaProfissional>>(db.CtgPrfsn, (current, expression) => current.Include(expression)).Where(predicate.Compile());
        }

        public IDbContextTransaction BeginTransaction() {
            return this.db.Database.BeginTransaction();
        }

        public void SaveChanges() {
            this.db.SaveChanges();
        }
    }

}
