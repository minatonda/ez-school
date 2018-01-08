using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Domain.Repositories {
    public class AreaInteresseRepository : IRepository<AreaInteresse> {

        private BaseContext db;

        public AreaInteresseRepository(BaseContext db) {
            this.db = db;
        }

        public void Add(AreaInteresse model) {
            this.db.AreaInteresse.Add(model);
        }

        public void AddHistoryAreaInteresse(long id) {
            var history = this.Get(id);

            history.ID = 0;
            history.Ativo = DateTime.Now;

            this.Add(history);
        }

        public void Update(AreaInteresse categoriaProfissional) {
            var model = this.db.AreaInteresse.Find(categoriaProfissional.ID);

            model.CategoriaProfissional = categoriaProfissional.CategoriaProfissional;
            model.Aluno = categoriaProfissional.Aluno;
            model.Professor = categoriaProfissional.Professor;
            model.Descricao = categoriaProfissional.Descricao;

            this.db.AreaInteresse.Update(model);
        }

        public void Disable(long ID) {
            var model = this.db.CategoriaProfissionais.Find(ID);

            model.Ativo = DateTime.Now;

            this.db.CategoriaProfissionais.Update(model);
        }

        public AreaInteresse Get(long id) {
            return this.db.AreaInteresse.Include(x => x.Aluno).Include(x => x.Professor).Include(x => x.CategoriaProfissional).SingleOrDefault(x => x.ID == id);
        }

        public List<AreaInteresse> GetAll(bool ativo) {
            return this.db.AreaInteresse.AsNoTracking().Where(x => x.Ativo.HasValue == !ativo).ToList();
        }

        public List<AreaInteresse> GetAllByProfessor(string idProfessor, bool ativo) {
            return this.db.AreaInteresse.AsNoTracking().Include(x => x.Professor).Where(x => x.Professor.ID == idProfessor && x.Ativo.HasValue == !ativo).ToList();
        }

        public List<AreaInteresse> GetAllByAluno(string idAluno, bool ativo) {
            return this.db.AreaInteresse.AsNoTracking().Include(x => x.Aluno).Where(x => x.Aluno.ID == idAluno && x.Ativo.HasValue == !ativo).ToList();
        }

        public IDbContextTransaction BeginTransaction() {
            return this.db.Database.BeginTransaction();
        }

        public void SaveChanges() {
            this.db.SaveChanges();
        }
    }

}
