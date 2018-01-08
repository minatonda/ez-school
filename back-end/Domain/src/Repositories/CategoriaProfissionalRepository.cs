using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Domain.Repositories {
    public class CategoriaProfissionalRepository : IRepository<CategoriaProfissional> {

        private BaseContext db;

        public CategoriaProfissionalRepository(BaseContext db) {
            this.db = db;
        }

        public CategoriaProfissionalRepository() {
        }

        public CategoriaProfissional Add(CategoriaProfissional categoriaProfissional) {
            this.db.CategoriaProfissionais.Add(categoriaProfissional);
            return categoriaProfissional;
        }

        public void AddHistoryCategoriaProfissional(long id) {
            var history = this.Get(id);
            history.ID = 0;
            history.Ativo = DateTime.Now;
            this.Add(history);
        }

        public CategoriaProfissional Update(CategoriaProfissional categoriaProfissional) {
            var model = this.db.CategoriaProfissionais.Find(categoriaProfissional.ID);

            model.Nome = categoriaProfissional.Nome;
            model.Descricao = categoriaProfissional.Descricao;

            this.db.CategoriaProfissionais.Update(model);
            return model;
        }
        public void Disable(long ID) {
            var model = this.db.CategoriaProfissionais.Find(ID);

            model.Ativo = DateTime.Now;
            this.db.CategoriaProfissionais.Update(model);
        }

        public CategoriaProfissional Get(long ID) => this.db.CategoriaProfissionais.Find(ID);

        public List<CategoriaProfissional> GetAll(bool ativo) {
            if (ativo) {
                return this.db.CategoriaProfissionais.Where(x => !x.Ativo.HasValue).ToList();
            } else {
                return this.db.CategoriaProfissionais.ToList();
            }
        }

        public IEnumerable<CategoriaProfissional> Query(Expression<Func<CategoriaProfissional, bool>> predicate, params Expression<Func<CategoriaProfissional, object>>[] includeExpressions) {
            return includeExpressions.Aggregate<Expression<Func<CategoriaProfissional, object>>, IQueryable<CategoriaProfissional>>(db.CategoriaProfissionais, (current, expression) => current.Include(expression)).Where(predicate.Compile());
        }

        public IDbContextTransaction BeginTransaction() {
            return this.db.Database.BeginTransaction();
        }

        public void SaveChanges() {
            this.db.SaveChanges();
        }
    }

}
