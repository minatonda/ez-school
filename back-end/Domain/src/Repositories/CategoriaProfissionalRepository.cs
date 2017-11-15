using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories {
    public class CategoriaProfissionalRepository : IRepository<CategoriaProfissional> {

        private BaseContext db;

        public CategoriaProfissionalRepository(BaseContext db) {
            this.db = db;
        }

        public CategoriaProfissionalRepository() {
        }

        public CategoriaProfissional Add(CategoriaProfissional model) {
            this.db.CategoriaProfissionais.Add(model);
            this.db.SaveChanges();
            return model;
        }
        public CategoriaProfissional Update(CategoriaProfissional model) {
            var _model = this.db.CategoriaProfissionais.Find(model.ID);
            _model.Nome = model.Nome;
            _model.Descricao = model.Descricao;
            this.db.CategoriaProfissionais.Update(_model);
            this.db.SaveChanges();
            return model;
        }
        public void Disable(long ID) {
            this.db.CategoriaProfissionais.Find(ID).Ativo = DateTime.Now;
            this.db.CategoriaProfissionais.Update(this.db.CategoriaProfissionais.Find(ID));
            this.db.SaveChanges();
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


    }

}
