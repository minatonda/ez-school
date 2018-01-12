using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Domain.Repositories {

    public class UsuarioRepository : IRepository<Usuario> {
        private BaseContext db;

        public UsuarioRepository(BaseContext db) {
            this.db = db;
        }

        public void Add(Usuario model) {

            var usuarioInfo = model.UsuarioInfo != null ? model.UsuarioInfo : new UsuarioInfo();
            usuarioInfo.ID = model.ID;
            this.AddUsuarioInfo(usuarioInfo);

            var aluno = new Aluno();
            aluno.ID = model.ID;
            aluno.UsuarioInfo = model.UsuarioInfo;
            this.AddAluno(aluno);

            var professor = new Professor();
            professor.ID = model.ID;
            aluno.UsuarioInfo = model.UsuarioInfo;
            this.AddProfessor(professor);

            this.db.Usuarios.Add(model);
        }

        public void AddUsuarioInfo(UsuarioInfo model) {
            this.db.UsuariosInfo.Add(model);
        }

        public void AddAluno(Aluno model) {
            this.db.Alunos.Add(model);
        }

        public void AddAluno(UsuarioInfo usuarioInfo) {
            var aluno = new Aluno();
            aluno.ID = usuarioInfo.ID;
            aluno.UsuarioInfo = usuarioInfo;
            aluno.Ativo = null;
            this.db.Alunos.Add(aluno);
        }

        public void AddProfessor(Professor model) {
            this.db.Professores.Add(model);
        }

        public void AddProfessor(UsuarioInfo usuarioInfo) {
            var professor = new Professor();
            professor.ID = usuarioInfo.ID;
            professor.UsuarioInfo = usuarioInfo;
            professor.Ativo = null;
            this.db.Professores.Add(professor);
        }

        public void Update(Usuario model) {
            var usuario = this.db.Usuarios.SingleOrDefault(x => x.ID == model.ID);

            usuario.Username = model.Username;
            usuario.Password = model.Password;

            this.db.Usuarios.Update(usuario);
        }

        public void UpdateUsuarioInfo(UsuarioInfo model) {
            var usuarioInfo = this.db.UsuariosInfo.SingleOrDefault(x => x.ID == model.ID);

            usuarioInfo.Nome = model.Nome;
            usuarioInfo.DataNascimento = model.DataNascimento;
            usuarioInfo.CPF = model.CPF;
            usuarioInfo.RG = model.RG;

            this.db.UsuariosInfo.Update(usuarioInfo);
        }

        public void UpdateAluno(Aluno model) {
            var aluno = this.db.Alunos.Include(x => x.UsuarioInfo).SingleOrDefault(x => x.UsuarioInfo.ID == model.ID);
            this.db.Alunos.Update(aluno);
        }

        public void UpdateProfessor(Professor model) {
            var professor = this.db.Professores.Include(x => x.UsuarioInfo).SingleOrDefault(x => x.UsuarioInfo.ID == model.ID);
            this.db.Professores.Update(professor);
        }

        public void Disable(long ID) {
            throw new NotImplementedException();
        }

        public void Disable(string ID) {
            this.db.Usuarios.Find(ID).Ativo = DateTime.Now;
            this.db.Usuarios.Update(this.db.Usuarios.Find(ID));
        }

        public Usuario Get(long id) {
            throw new NotImplementedException();
        }

        public Usuario Get(string ID) {
            return this.db.Usuarios
            .AsNoTracking()
            .Include(i => i.UsuarioInfo)
            .SingleOrDefault(x => x.ID == ID);
        }

        public Usuario GetByRG(string rg) {
            return this.db.Usuarios
            .AsNoTracking()
            .Include(i => i.UsuarioInfo)
            .SingleOrDefault(x => x.UsuarioInfo.RG == rg);
        }

        public Usuario GetByUsernameAndPassword(string username, string password) {
            return this.db.Usuarios
            .AsNoTracking()
            .Include(i => i.UsuarioInfo)
            .SingleOrDefault(x => x.Username == username && x.Password == password);
        }

        public UsuarioInfo GetInfo(string id) {
            return this.db.UsuariosInfo
            .AsNoTracking()
            .SingleOrDefault(x => x.ID == id);
        }

        public UsuarioInfo GetInfoByRG(string rg) {
            return this.db.UsuariosInfo
            .AsNoTracking()
            .SingleOrDefault(x => x.RG == rg);
        }

        public Aluno GetAluno(string id) {
            return this.db.Alunos
            .AsNoTracking()
            .Include(i => i.UsuarioInfo)
            .SingleOrDefault(x => x.UsuarioInfo.ID == id);
        }

        public Professor GetProfessor(string ID) {
            return this.db.Professores
            .AsNoTracking()
            .Include(i => i.UsuarioInfo)
            .SingleOrDefault(x => x.UsuarioInfo.ID == ID);
        }

        public List<Usuario> GetAll(bool ativo) {
            return this.db.Usuarios
            .AsNoTracking()
            .Include(i => i.UsuarioInfo)
            .Where(x => x.Ativo.HasValue == !ativo).ToList();
        }

        public List<Aluno> GetAllAlunosByTermo(string termo, bool ativo) {
            return this.db.Alunos
            .AsNoTracking()
            .Include(i => i.UsuarioInfo)
            .Where(x => x.UsuarioInfo.Nome.ToLower().Contains(termo.ToLower()) || x.UsuarioInfo.RG.ToLower().Contains(termo.ToLower()) || x.UsuarioInfo.CPF.ToLower().Contains(termo.ToLower()) && x.Ativo.HasValue == !ativo && x.UsuarioInfo.Ativo.HasValue == !ativo)
            .ToList();
        }

        public List<Professor> GetAllProfessoresByTermo(string termo, bool ativo) {
            return this.db.Professores
            .AsNoTracking()
            .Include(i => i.UsuarioInfo)
            .Where(x => x.UsuarioInfo.Nome.ToLower().Contains(termo.ToLower()) || x.UsuarioInfo.RG.ToLower().Contains(termo.ToLower()) || x.UsuarioInfo.CPF.ToLower().Contains(termo.ToLower()) && x.Ativo.HasValue == !ativo && x.UsuarioInfo.Ativo.HasValue == !ativo)
            .ToList();
        }

        public List<AreaInteresse> GetAreaInteressesByProfessor(string id) {
            return this.db.AreaInteresse
            .AsNoTracking()
            .Include(i => i.Professor)
            .Include(x => x.CategoriaProfissional)
            .Where(x => x.Professor.ID == id)
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