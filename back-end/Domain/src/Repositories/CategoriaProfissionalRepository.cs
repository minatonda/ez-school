using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Domain.Repositories {

    public class CategoriaProfissionalRepository : IRepository<CategoriaProfissional> {

        private BaseContext db;

        public CategoriaProfissionalRepository(BaseContext db) {
            this.db = db;
        }

        public void Add(CategoriaProfissional categoriaProfissional) {
            this.db.CategoriaProfissionais.Add(categoriaProfissional);
        }

        public void AddHistoryCategoriaProfissional(long id) {
            var history = this.Get(id);

            history.ID = 0;
            history.Ativo = DateTime.Now;
            
            this.Add(history);
        }

        public void Update(CategoriaProfissional categoriaProfissional) {
            var model = this.db.CategoriaProfissionais.Find(categoriaProfissional.ID);

            model.Nome = categoriaProfissional.Nome;
            model.Descricao = categoriaProfissional.Descricao;

            this.db.CategoriaProfissionais.Update(model);
        }

        public void Disable(long ID) {
            var model = this.db.CategoriaProfissionais.Find(ID);

            model.Ativo = DateTime.Now;
            
            this.db.CategoriaProfissionais.Update(model);
        }

        public CategoriaProfissional Get(long id) {
            return this.db.CategoriaProfissionais
            .AsNoTracking()
            .SingleOrDefault(x => x.ID == id);
        }

        public List<CategoriaProfissional> GetAll(bool ativo) {
            return this.db.CategoriaProfissionais
            .AsNoTracking()
            .Where(x => x.Ativo.HasValue == !ativo)
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
