using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories {
    public class MateriaRepository : IRepository<Materia> {
        private BaseContext db;

        public MateriaRepository(BaseContext db) {
            this.db = db;
        }
        public Materia Add(Materia model) {
            this.db.Materias.Add(model);
            this.db.SaveChanges();
            return model;
        }

        public Materia Add(Materia model, List<Materia> Materias) {
            this.db.Materias.Add(model);
            this.db.MateriaRelacionamento.AddRange(Materias.Select(x => new MateriaRelacionamento() { MateriaPai = this.db.Materias.Find(x.ID), MateriaPrincipal = model }));
            this.db.SaveChanges();
            return model;
        }

        public Materia Update(Materia model) {
            this.db.Materias.Find(model.ID).Nome = model.Nome;
            this.db.Materias.Find(model.ID).Descricao = model.Descricao;
            this.db.Materias.Update(this.db.Materias.Find(model.ID));
            this.db.SaveChanges();
            return model;
        }
        public Materia Update(Materia model, List<Materia> Materias) {
            this.db.Materias.Find(model.ID).Nome = model.Nome;
            this.db.Materias.Find(model.ID).Descricao = model.Descricao;
            this.db.Materias.Update(this.db.Materias.Find(model.ID));
            this.db.MateriaRelacionamento.AddRange(Materias.Select(x=> new MateriaRelacionamento() { MateriaPai = this.db.Materias.Find(x.ID), MateriaPrincipal = model }));
            this.db.SaveChanges();
            return model;
        }
        public void Disable(long ID) {
            this.db.Materias.Find(ID).Ativo = false;
            this.db.Materias.Update(this.db.Materias.Find(ID));
            this.db.SaveChanges();
        }
        public Materia Get(long ID) => this.db.Materias.Find(ID);
        public List<Materia> GetAll(bool? ativo) => this.db.Materias.Where(x => x.Ativo == (ativo.HasValue ? ativo.Value : false)).ToList();
        public List<Materia> GetRelacionadas(long ID) => this.db.MateriaRelacionamento.Where(x => x.MateriaPrincipal.ID == ID).Select(x => x.MateriaPai).ToList();
        public IEnumerable<Materia> Query(Expression<Func<Materia, bool>> predicate, params Expression<Func<Materia, object>>[] includeExpressions) {
            return includeExpressions.Aggregate<Expression<Func<Materia, object>>, IQueryable<Materia>>(db.Materias, (current, expression) => current.Include(expression)).Where(predicate.Compile());
        }
    }

}