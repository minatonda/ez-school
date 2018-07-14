using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Domain.InstituicaoDomain {

    public class InstituicaoCategoriaRepository : IRepository<InstituicaoCategoria> {
    
        private BaseContext db;

        public InstituicaoCategoriaRepository(BaseContext db) {
            this.db = db;
        }

        public void Add(InstituicaoCategoria instituicaoCategoria) {
            this.db.InstituicoesCategorias.Add(instituicaoCategoria);
        }

        public void AddHistoryInstituicaoCategoria(long id) {
            var history = this.Get(id);
            history.ID = 0;
            history.Ativo = DateTime.Now;
            this.Add(history);
        }

        public void Update(InstituicaoCategoria instituicaoCategoria) {
            var model = this.db.InstituicoesCategorias.Find(instituicaoCategoria.ID);

            model.Nome = instituicaoCategoria.Nome;
            model.Descricao = instituicaoCategoria.Descricao;

            this.db.InstituicoesCategorias.Update(model);
        }

        public void Disable(long ID) {
            var model = this.db.InstituicoesCategorias.Find(ID);
            model.Ativo = DateTime.Now;
            this.db.InstituicoesCategorias.Update(model);
        }

        public InstituicaoCategoria Get(long ID) => this.db.InstituicoesCategorias.Find(ID);

        public List<InstituicaoCategoria> GetAll(bool ativo) {
            if (ativo) {
                return this.db.InstituicoesCategorias.Where(x => !x.Ativo.HasValue).ToList();
            } else {
                return this.db.InstituicoesCategorias.ToList();
            }
        }

        public IEnumerable<InstituicaoCategoria> Query(Expression<Func<InstituicaoCategoria, bool>> predicate, params Expression<Func<InstituicaoCategoria, object>>[] includeExpressions) {
            return includeExpressions.Aggregate<Expression<Func<InstituicaoCategoria, object>>, IQueryable<InstituicaoCategoria>>(db.InstituicoesCategorias, (current, expression) => current.Include(expression)).Where(predicate.Compile());
        }

        public IDbContextTransaction BeginTransaction() {
            return this.db.Database.BeginTransaction();
        }
        
        public void SaveChanges() {
            this.db.SaveChanges();
        }

    }

}