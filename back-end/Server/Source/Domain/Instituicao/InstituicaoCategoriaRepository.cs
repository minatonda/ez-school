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
            this.db.IttcCtgr.Add(instituicaoCategoria);
        }

        public void AddHistoryInstituicaoCategoria(long id) {
            var history = this.Get(id);
            history.ID = 0;
            history.Ativo = DateTime.Now;
            this.Add(history);
        }

        public void Update(InstituicaoCategoria instituicaoCategoria) {
            var model = this.db.IttcCtgr.Find(instituicaoCategoria.ID);

            model.Nome = instituicaoCategoria.Nome;
            model.Descricao = instituicaoCategoria.Descricao;

            this.db.IttcCtgr.Update(model);
        }

        public void Disable(long ID) {
            var model = this.db.IttcCtgr.Find(ID);
            model.Ativo = DateTime.Now;
            this.db.IttcCtgr.Update(model);
        }

        public InstituicaoCategoria Get(long ID) => this.db.IttcCtgr.Find(ID);

        public List<InstituicaoCategoria> GetAll(bool ativo) {
            if (ativo) {
                return this.db.IttcCtgr.Where(x => !x.Ativo.HasValue).ToList();
            } else {
                return this.db.IttcCtgr.ToList();
            }
        }

        public IEnumerable<InstituicaoCategoria> Query(Expression<Func<InstituicaoCategoria, bool>> predicate, params Expression<Func<InstituicaoCategoria, object>>[] includeExpressions) {
            return includeExpressions.Aggregate<Expression<Func<InstituicaoCategoria, object>>, IQueryable<InstituicaoCategoria>>(db.IttcCtgr, (current, expression) => current.Include(expression)).Where(predicate.Compile());
        }

        public IDbContextTransaction BeginTransaction() {
            return this.db.Database.BeginTransaction();
        }
        
        public void SaveChanges() {
            this.db.SaveChanges();
        }

    }

}