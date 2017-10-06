using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories {
    public class InstituicaoCategoriaRepository : IRepository<InstituicaoCategoria> {
        private BaseContext db;

        public InstituicaoCategoriaRepository (BaseContext db) {
            this.db = db;
        }
        public InstituicaoCategoria Add (InstituicaoCategoria model) {
            this.db.InstituicaoCategorias.Add (model);
            this.db.SaveChanges ();
            return model;
        }
        public InstituicaoCategoria Update (InstituicaoCategoria model) {
            this.db.InstituicaoCategorias.Find (model.ID).Nome = model.Nome;
            this.db.InstituicaoCategorias.Find (model.ID).Descricao = model.Descricao;
            this.db.InstituicaoCategorias.Update (this.db.InstituicaoCategorias.Find (model.ID));
            this.db.SaveChanges ();
            return model;
        }
        public void Disable (long ID) {
            this.db.InstituicaoCategorias.Find (ID).Ativo = false;
            this.db.InstituicaoCategorias.Update (this.db.InstituicaoCategorias.Find (ID));
            this.db.SaveChanges ();
        }
        public InstituicaoCategoria Get (long ID) => this.db.InstituicaoCategorias.Find (ID);
        public List<InstituicaoCategoria> GetAll (bool? ativo) => this.db.InstituicaoCategorias.Where (x => x.Ativo == (ativo.HasValue ? ativo.Value : false)).ToList ();
        public IEnumerable<InstituicaoCategoria> Query (Expression<Func<InstituicaoCategoria, bool>> predicate, params Expression<Func<InstituicaoCategoria, object>>[] includeExpressions) {
            return includeExpressions.Aggregate<Expression<Func<InstituicaoCategoria, object>>, IQueryable<InstituicaoCategoria>> (db.InstituicaoCategorias, (current, expression) => current.Include (expression)).Where (predicate.Compile ());
        }

    }

}