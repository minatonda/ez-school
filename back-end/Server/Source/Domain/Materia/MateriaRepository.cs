using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Domain.MateriaDomain {

    public class MateriaRepository : IRepository<Materia> {

        private BaseContext db;

        public MateriaRepository(BaseContext db) {
            this.db = db;
        }
        public void Add(Materia model) {
            this.db.Mtr.Add(model);
            this.db.SaveChanges();
        }

        public void AddMateriaRelacionada(MateriaRelacionamento materia) {
            var model = new MateriaRelacionamento();
            model.MateriaPai = this.db.Mtr.Find(materia.MateriaPai.ID);
            model.MateriaPrincipal = this.db.Mtr.Find(materia.MateriaPrincipal.ID); ;
            this.db.MtrRlc.AddRange(model);
        }

        public void AddHistoryMateria(long id) {
            var history = this.Get(id);
            history.ID = 0;
            history.Ativo = DateTime.Now;
            this.Add(history);
        }
        public void Update(Materia materia) {
            var model = this.db.Mtr.Find(materia.ID);

            model.Nome = materia.Nome;
            model.Descricao = materia.Descricao;

            this.db.Mtr.Update(model);
        }
        public void Update(Materia model, List<Materia> Materias) {
            this.db.Mtr.Find(model.ID).Nome = model.Nome;
            this.db.Mtr.Find(model.ID).Descricao = model.Descricao;
            this.db.Mtr.Update(this.db.Mtr.Find(model.ID));
            this.db.MtrRlc.AddRange(Materias.Select(x => new MateriaRelacionamento() { MateriaPai = this.db.Mtr.Find(x.ID), MateriaPrincipal = model }));
            this.db.SaveChanges();
        }
        public void Disable(long id) {
            var model = this.db.Mtr.Find(id);
            model.Ativo = DateTime.Now;
            this.db.Mtr.Update(model);
        }

        public void DisableMateriaRelacionada(long id) {
            var model = this.db.MtrRlc.Find(id);
            model.Ativo = DateTime.Now;
            this.db.MtrRlc.Update(model);
        }
        public Materia Get(long ID) => this.db.Mtr.Find(ID);
        public List<Materia> GetAll(bool ativo) {
            if (ativo) {
                return this.db.Mtr.Where(x => !x.Ativo.HasValue).ToList();
            } else {
                return this.db.Mtr.ToList();
            }
        }
        public List<MateriaRelacionamento> GetRelacionadas(long ID, bool ativo) {
            return this.db.MtrRlc
            .Include(x => x.MateriaPai)
            .Include(x => x.MateriaPrincipal)
            .Where(x => x.MateriaPrincipal.ID == ID && x.Ativo.HasValue == !ativo)
            .ToList();
        }
        public IEnumerable<Materia> Query(Expression<Func<Materia, bool>> predicate, params Expression<Func<Materia, object>>[] includeExpressions) {
            return includeExpressions.Aggregate<Expression<Func<Materia, object>>, IQueryable<Materia>>(db.Mtr, (current, expression) => current.Include(expression)).Where(predicate.Compile());
        }
        public IDbContextTransaction BeginTransaction() {
            return this.db.Database.BeginTransaction();
        }
        public void SaveChanges() {
            this.db.SaveChanges();
        }
    }

}