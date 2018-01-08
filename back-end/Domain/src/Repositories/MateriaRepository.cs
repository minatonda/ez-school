using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Domain.Repositories {
    public class MateriaRepository : IRepository<Materia> {
        private BaseContext db;

        public MateriaRepository(BaseContext db) {
            this.db = db;
        }
        public void Add(Materia model) {
            this.db.Materias.Add(model);
        }

        public void AddMateriaRelacionada(MateriaRelacionamento materia) {
            var model = new MateriaRelacionamento();
            model.MateriaPai = this.db.Materias.Find(materia.MateriaPai.ID);
            model.MateriaPrincipal = this.db.Materias.Find(materia.MateriaPrincipal.ID); ;
            this.db.MateriaRelacionamento.AddRange(model);
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
            var model = this.db.MateriaRelacionamento.Find(id);
            model.Ativo = DateTime.Now;
            this.db.MateriaRelacionamento.Update(model);
        }
        public Materia Get(long id) {
            return this.db.Materias
            .AsNoTracking()
            .SingleOrDefault(x => x.ID == id);
        }

        public List<Materia> GetAll(bool ativo) {
            return this.db.Materias
            .AsNoTracking()
            .Where(x => x.Ativo.HasValue == !ativo)
            .ToList();
        }
        public List<MateriaRelacionamento> GetRelacionadas(long id, bool ativo) {
            return this.db.MateriaRelacionamento
            .AsNoTracking()
            .Include(x => x.MateriaPai)
            .Include(x => x.MateriaPrincipal)
            .Where(x => x.MateriaPrincipal.ID == id && x.Ativo.HasValue == !ativo)
            .ToList();
        }

        public IDbContextTransaction BeginTransaction() {
            return this.db.Database.BeginTransaction();
        }
        public void SaveChanges() {
            this.db.SaveChanges();
        }
    }

}