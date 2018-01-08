using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Domain.Repositories {
    public class InstituicaoCategoriaRepository : IRepository<InstituicaoCategoria> {
        private BaseContext db;

        public InstituicaoCategoriaRepository(BaseContext db) {
            this.db = db;
        }

        public InstituicaoCategoria Add(InstituicaoCategoria instituicaoCategoria) {
            this.db.InstituicaoCategorias.Add(instituicaoCategoria);
            return instituicaoCategoria;
        }

        public void AddHistoryInstituicaoCategoria(long id) {
            var history = this.Get(id);
            history.ID = 0;
            history.Ativo = DateTime.Now;
            this.Add(history);
        }

        public InstituicaoCategoria Update(InstituicaoCategoria instituicaoCategoria) {
            var model = this.db.InstituicaoCategorias.Find(instituicaoCategoria.ID);

            model.Nome = instituicaoCategoria.Nome;
            model.Descricao = instituicaoCategoria.Descricao;

            this.db.InstituicaoCategorias.Update(model);
            return model;
        }

        public void Disable(long ID) {
            var model = this.db.InstituicaoCategorias.Find(ID);
            model.Ativo = DateTime.Now;
            this.db.InstituicaoCategorias.Update(model);
        }

        public InstituicaoCategoria Get(long ID) => this.db.InstituicaoCategorias.Find(ID);

        public List<InstituicaoCategoria> GetAll(bool ativo) {
            if (ativo) {
                return this.db.InstituicaoCategorias.Where(x => !x.Ativo.HasValue).ToList();
            } else {
                return this.db.InstituicaoCategorias.ToList();
            }
        }

        public IEnumerable<InstituicaoCategoria> Query(Expression<Func<InstituicaoCategoria, bool>> predicate, params Expression<Func<InstituicaoCategoria, object>>[] includeExpressions) {
            return includeExpressions.Aggregate<Expression<Func<InstituicaoCategoria, object>>, IQueryable<InstituicaoCategoria>>(db.InstituicaoCategorias, (current, expression) => current.Include(expression)).Where(predicate.Compile());
        }

        public IDbContextTransaction BeginTransaction() {
            return this.db.Database.BeginTransaction();
        }
        
        public void SaveChanges() {
            this.db.SaveChanges();
        }

    }

}