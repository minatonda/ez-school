using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories {
    public class ProfessorRepository : IRepository<Professor> {
        private BaseContext db;

        public ProfessorRepository (BaseContext db) {
            this.db = db;
        }
        public Professor Add (Professor model) {
            if (model.Usuario != null) {
                this.db.Attach (model.Usuario);
            }
            this.db.Professores.Add(model);
            this.db.SaveChanges ();
            return model;
        }
        public Professor Update (Professor model) {
            if (model.Usuario != null) {
                this.db.Attach (model.Usuario);
                this.db.Professores.Find (model.ID).ID = model.Usuario.ID;
                this.db.Professores.Find (model.ID).Usuario = model.Usuario;
            }
            this.db.Professores.Update (this.db.Professores.Find (model.ID));
            this.db.SaveChanges ();
            return model;
        }
        public void Delete (long ID) {
            this.db.Professores.Find (ID).Ativo = false;
            this.db.Professores.Update (this.db.Professores.Find (ID));
            this.db.SaveChanges ();
        }
        public Professor Get (long ID) => this.db.Professores.Find (ID);
        public List<Professor> GetAll (bool? ativo) => this.db.Professores.Where (x => x.Ativo == (ativo.HasValue ? ativo.Value : false)).ToList ();
        public IEnumerable<Professor> Query (Expression<Func<Professor, bool>> predicate, params Expression<Func<Professor, object>>[] includeExpressions) {
            return includeExpressions.Aggregate<Expression<Func<Professor, object>>, IQueryable<Professor>> (db.Professores, (current, expression) => current.Include (expression)).Where (predicate.Compile ());
        }

    }

}