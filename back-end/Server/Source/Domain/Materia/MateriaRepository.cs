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
            this.db.Materias.Add(model);
            this.db.SaveChanges();
        }

        public void AddMateriaRelacionada(MateriaRelacionamento materia) {
            var model = new MateriaRelacionamento();
            model.MateriaPai = this.db.Materias.Find(materia.MateriaPai.ID);
            model.MateriaPrincipal = this.db.Materias.Find(materia.MateriaPrincipal.ID); ;
            this.db.MateriasRelacionamentos.AddRange(model);
        }

        public void AddHistoryMateria(long id) {
            var history = this.Get(id);
            history.ID = 0;
            history.Ativo = DateTime.Now;
            this.Add(history);
        }
        public void Update(Materia materia) {
            var model = this.db.Materias.Find(materia.ID);
            model.Nome = materia.Nome;
            model.Descricao = materia.Descricao;
            this.db.Materias.Update(model);
        }

        public void Disable(long id) {
            var model = this.db.Materias.Find(id);
            model.Ativo = DateTime.Now;
            this.db.Materias.Update(model);
        }

        public void DisableMateriaRelacionada(long id) {
            var model = this.db.MateriasRelacionamentos.Find(id);
            model.Ativo = DateTime.Now;
            this.db.MateriasRelacionamentos.Update(model);
        }

        public Materia Get(long id) {
            return this.db.Materias
            .AsNoTracking()
            .SingleOrDefault(x => x.ID == id);
        }

        public Materia GetByNome(string nome) {
            return this.db.Materias
            .AsNoTracking()
            .FirstOrDefault(x => x.Nome.ToLower() == nome.ToLower() && !x.Ativo.HasValue);
        }

        public List<Materia> GetAll(bool ativo) {
            return this.db.Materias
            .AsNoTracking()
            .Where(x => x.Ativo.HasValue == !ativo)
            .ToList();
        }
        public List<MateriaRelacionamento> GetRelacionadas(long ID, bool ativo) {
            return this.db.MateriasRelacionamentos
            .AsNoTracking()
            .Include(x => x.MateriaPai)
            .Include(x => x.MateriaPrincipal)
            .Where(x => x.MateriaPrincipal.ID == ID && x.Ativo.HasValue == !ativo)
            .ToList();
        }
        public IEnumerable<Materia> Query(Expression<Func<Materia, bool>> predicate, params Expression<Func<Materia, object>>[] includeExpressions) {
            return includeExpressions.Aggregate<Expression<Func<Materia, object>>, IQueryable<Materia>>(db.Materias, (current, expression) => current.Include(expression)).Where(predicate.Compile());
        }
        public IDbContextTransaction BeginTransaction() {
            return this.db.Database.BeginTransaction();
        }
        public void SaveChanges() {
            this.db.SaveChanges();
        }

    }

}