using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories {
    public class CursoRepository : IRepository<Curso> {
        private BaseContext db;

        public CursoRepository (BaseContext db) {
            this.db = db;
        }
        public Curso Add (Curso model) {
            this.db.Cursos.Add (model);
            this.db.SaveChanges ();
            return model;
        }
        public Curso Update (Curso model) {
            this.db.Cursos.Find (model.ID).Nome = model.Nome;
            this.db.Cursos.Find (model.ID).Descricao = model.Descricao;
            this.db.Cursos.Update (this.db.Cursos.Find (model.ID));
            this.db.SaveChanges ();
            return model;
        }
        public void Delete (long ID) {
            this.db.Cursos.Find (ID).Ativo = false;
            this.db.Cursos.Update (this.db.Cursos.Find (ID));
            this.db.SaveChanges ();
        }
        public Curso Get (long ID) => this.db.Cursos.Find (ID);
        public List<Curso> GetAll (bool? ativo) => this.db.Cursos.Where (x => x.Ativo == (ativo.HasValue ? ativo.Value : false)).ToList ();
        public IEnumerable<Curso> Query (Expression<Func<Curso, bool>> predicate, params Expression<Func<Curso, object>>[] includeExpressions) {
            return includeExpressions.Aggregate<Expression<Func<Curso, object>>, IQueryable<Curso>> (db.Cursos, (current, expression) => current.Include (expression)).Where (predicate.Compile ());
        }

    }

}