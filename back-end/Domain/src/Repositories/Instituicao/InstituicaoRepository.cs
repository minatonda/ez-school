using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories {
    public class InstituicaoRepository : IRepository<Instituicao> {
        private BaseContext db;

        public InstituicaoRepository (BaseContext db) {
            this.db = db;
        }
        public Instituicao Add (Instituicao model) {
            this.db.Instituicoes.Add (model);
            this.db.SaveChanges ();
            return model;
        }
        public Instituicao Update (Instituicao model) {
            this.db.Instituicoes.Find (model.ID).Nome = model.Nome;
            this.db.Instituicoes.Find (model.ID).CNPJ = model.CNPJ;
            this.db.Instituicoes.Update (this.db.Instituicoes.Find (model.ID));
            this.db.SaveChanges ();
            return model;
        }
        public void Delete (long ID) {
            this.db.Instituicoes.Find (ID).Ativo = false;
            this.db.Instituicoes.Update (this.db.Instituicoes.Find (ID));
            this.db.SaveChanges ();
        }
        public Instituicao Get (long ID) => this.db.Instituicoes.Find (ID);
        public List<Instituicao> GetAll (bool? ativo) => this.db.Instituicoes.Where (x => x.Ativo == (ativo.HasValue ? ativo.Value : false)).ToList ();
        public IEnumerable<Instituicao> Query (Expression<Func<Instituicao, bool>> predicate, params Expression<Func<Instituicao, object>>[] includeExpressions) {
            return includeExpressions.Aggregate<Expression<Func<Instituicao, object>>, IQueryable<Instituicao>> (db.Instituicoes, (current, expression) => current.Include (expression)).Where (predicate.Compile ());
        }

    }

}