using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories {
    public class AlunoRepository : IRepository<Aluno> {
        private BaseContext db;

        public AlunoRepository (BaseContext db) {
            this.db = db;
        }
        public Aluno Add (Aluno model) {
            if (model.Usuario != null) {
                this.db.Attach (model.Usuario);
            }
            this.db.Alunos.Add(model);
            this.db.SaveChanges ();
            return model;
        }
        public Aluno Update (Aluno model) {
            if (model.Usuario != null) {
                this.db.Attach (model.Usuario);
                this.db.Alunos.Find (model.ID).ID = model.Usuario.ID;
                this.db.Alunos.Find (model.ID).Usuario = model.Usuario;
            }
            this.db.Alunos.Update (this.db.Alunos.Find (model.ID));
            this.db.SaveChanges ();
            return model;
        }
        public void Delete (long ID) {
            this.db.Alunos.Find (ID).Ativo = false;
            this.db.Alunos.Update (this.db.Alunos.Find (ID));
            this.db.SaveChanges ();
        }
        public Aluno Get (long ID) => this.db.Alunos.Find (ID);
        public List<Aluno> GetAll (bool? ativo) => this.db.Alunos.Where (x => x.Ativo == (ativo.HasValue ? ativo.Value : false)).ToList ();
        public IEnumerable<Aluno> Query (Expression<Func<Aluno, bool>> predicate, params Expression<Func<Aluno, object>>[] includeExpressions) {
            return includeExpressions.Aggregate<Expression<Func<Aluno, object>>, IQueryable<Aluno>> (db.Alunos, (current, expression) => current.Include (expression)).Where (predicate.Compile ());
        }

    }

}