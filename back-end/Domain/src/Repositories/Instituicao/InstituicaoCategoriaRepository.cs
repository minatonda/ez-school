using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Domain.Repositories {
    public class InstituicaoCategoriaRepository : IRepository<InstituicaoCategoria> {
        private BaseContext db;

        public InstituicaoCategoriaRepository(BaseContext db) {
            this.db = db;
        }

        public void Add(InstituicaoCategoria instituicaoCategoria) {
            this.db.InstituicaoCategorias.Add(instituicaoCategoria);
        }

        public void AddHistoryInstituicaoCategoria(long id) {
            var history = this.Get(id);
            history.ID = 0;
            history.Ativo = DateTime.Now;
            this.Add(history);
        }

        public void Update(InstituicaoCategoria instituicaoCategoria) {
            var model = this.db.InstituicaoCategorias.Find(instituicaoCategoria.ID);

            model.Nome = instituicaoCategoria.Nome;
            model.Descricao = instituicaoCategoria.Descricao;

            this.db.InstituicaoCategorias.Update(model);
        }

        public void Disable(long ID) {
            var model = this.db.InstituicaoCategorias.Find(ID);
            model.Ativo = DateTime.Now;
            this.db.InstituicaoCategorias.Update(model);
        }

        public InstituicaoCategoria Get(long id) {
            return this.db.InstituicaoCategorias.SingleOrDefault(x => x.ID == id);
        }

        public List<InstituicaoCategoria> GetAll(bool ativo) {
            return this.db.InstituicaoCategorias.AsNoTracking().Where(x => x.Ativo.HasValue == !ativo).ToList();
        }

        public IDbContextTransaction BeginTransaction() {
            return this.db.Database.BeginTransaction();
        }

        public void SaveChanges() {
            this.db.SaveChanges();
        }

    }

}