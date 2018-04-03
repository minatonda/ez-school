using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Domain.AnexoDomain {
    public class AnexoRepository : IRepository<Anexo> {

        private BaseContext db;

        public AnexoRepository(BaseContext db) {
            this.db = db;
        }

        public void Add(Anexo anexo) {
            this.db.Anx.Add(anexo);
        }

        public void Update(Anexo anexo) {
            var model = this.db.Anx.Find(anexo.ID);
            this.db.Anx.Update(model);
        }

        public void Disable(long id) {
            var model = this.db.Crs.Find(id);
            model.Ativo = DateTime.Now;
            this.db.Crs.Update(model);
        }

        public Anexo Get(string ID) {
            return this.db.Anx
            .AsNoTracking()
            .SingleOrDefault(x => x.ID == ID);
        }

        public List<Anexo> GetAll(bool ativo) {
            return this.db.Anx
            .AsNoTracking()
            .Where(x => x.Ativo.HasValue == !ativo)
            .ToList();
        }

        public IEnumerable<Anexo> Query(Expression<Func<Anexo, bool>> predicate, params Expression<Func<Anexo, object>>[] includeExpressions) {
            return includeExpressions.Aggregate<Expression<Func<Anexo, object>>, IQueryable<Anexo>>(db.Anx, (current, expression) => current.Include(expression)).Where(predicate.Compile());
        }

        public IDbContextTransaction BeginTransaction() {
            return this.db.Database.BeginTransaction();
        }

        public void SaveChanges() {
            this.db.SaveChanges();
        }

    }

}