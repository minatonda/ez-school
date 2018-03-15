using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain;
using Domain.AreaInteresseDomain;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Domain.UsuarioDomain {

    public class UsuarioRepository : IRepository<Usuario> {

        private BaseContext db;

        public UsuarioRepository(BaseContext db) {
            this.db = db;
        }

        public void Add(Usuario model) {
            model.UsuarioInfo.ID = model.ID;
            this.db.UsuariosInfo.Add(model.UsuarioInfo);

            var aluno = new Aluno();
            aluno.ID = model.ID;
            aluno.UsuarioInfo = model.UsuarioInfo;
            this.db.Alunos.Add(aluno);

            var professor = new Professor();
            professor.ID = model.ID;
            aluno.UsuarioInfo = model.UsuarioInfo;
            this.db.Professores.Add(professor);

            this.db.Usuarios.Add(model);
        }

        public void Update(Usuario model) {
            if (model.UsuarioInfo != null) {
                this.db.Attach(model.UsuarioInfo);
            }

            var attachedUsuario = this.db.Usuarios.Find(model.ID);
            attachedUsuario.Username = model.Username;
            attachedUsuario.Password = model.Password;

            this.UpdateUsuarioInfo(model.UsuarioInfo);
            this.db.Usuarios.Update(attachedUsuario);
        }

        public void UpdateUsuarioInfo(UsuarioInfo model) {
            var attachedUsuarioInfo = this.db.UsuariosInfo.Find(model.ID);
            attachedUsuarioInfo.Nome = model.Nome;
            attachedUsuarioInfo.DataNascimento = model.DataNascimento;
            attachedUsuarioInfo.CPF = model.CPF;
            attachedUsuarioInfo.RG = model.RG;
            attachedUsuarioInfo.Roles = model.Roles;

            this.db.UsuariosInfo.Update(attachedUsuarioInfo);
        }

        public void UpdateAluno(Aluno model) {
            var aluno = this.db.Alunos.Include(x => x.UsuarioInfo).SingleOrDefault(x => x.UsuarioInfo.ID == model.ID);
            this.db.Alunos.Update(aluno);
        }

        public void UpdateProfessor(Professor model) {
            var professor = this.db.Professores.Include(x => x.UsuarioInfo).SingleOrDefault(x => x.UsuarioInfo.ID == model.ID);
            this.db.Professores.Update(professor);
        }

        public void Disable(string ID) {
            var usuario = this.db.Usuarios.Find(ID);
            usuario.Ativo = DateTime.Now;
            this.db.Usuarios.Update(usuario);
        }

        public Usuario Get(string ID) {
            return this.db.Usuarios
            .AsNoTracking()
            .Include(i => i.UsuarioInfo)
            .SingleOrDefault(x => x.ID == ID);
        }

        public UsuarioInfo GetUsuarioInfo(string ID) {
            return this.db.UsuariosInfo
            .AsNoTracking()
            .SingleOrDefault(x => x.ID == ID);
        }

        public Usuario GetByRG(string rg) {
            return this.db.Usuarios
            .AsNoTracking()
            .Include(i => i.UsuarioInfo)
            .SingleOrDefault(x => x.UsuarioInfo.RG == rg);
        }

        public UsuarioInfo GetInfoByRG(string rg) {
            return this.db.UsuariosInfo
            .AsNoTracking()
            .SingleOrDefault(x => x.RG == rg);
        }

        public List<Usuario> GetAll(bool ativo) {
            return this.db.Usuarios
            .AsNoTracking()
            .Include(i => i.UsuarioInfo)
            .Where(x => x.Ativo.HasValue == !ativo)
            .ToList();
        }

        public List<UsuarioInfo> GetAllByTermo(string termo, bool onlyAluno, bool onlyProfessor) {
            var query = this.db.UsuariosInfo
            .AsNoTracking()
            .Where(x => x.Nome.ToLower().Contains(termo.ToLower()) || x.RG.ToLower().Contains(termo.ToLower()) || x.CPF.ToLower().Contains(termo.ToLower()));

            if (onlyAluno) {
                var idAlunosAtivos = this.db.Alunos.Where(x => !x.Ativo.HasValue).Select(x => x.UsuarioInfo.ID);
                query = query.Where(x => idAlunosAtivos.Contains(x.ID));
            }

            if (onlyProfessor) {
                var idProfessoresAtivos = this.db.Professores.Where(x => !x.Ativo.HasValue).Select(x => x.UsuarioInfo.ID);
                query = query.Where(x => idProfessoresAtivos.Contains(x.ID));
            }

            return query.ToList();
        }

        public Aluno GetAluno(string ID) {
            return this.db.Alunos
            .AsNoTracking()
            .Include(i => i.UsuarioInfo)
            .SingleOrDefault(x => x.UsuarioInfo.ID == ID);
        }

        public Professor GetProfessor(string ID) {
            return this.db.Professores
            .AsNoTracking()
            .Include(i => i.UsuarioInfo)
            .SingleOrDefault(x => x.UsuarioInfo.ID == ID);
        }

        public IEnumerable<Usuario> Query(Expression<Func<Usuario, bool>> predicate, params Expression<Func<Usuario, object>>[] includeExpressions) {
            return includeExpressions.Aggregate<Expression<Func<Usuario, object>>, IQueryable<Usuario>>(db.Usuarios, (current, expression) => current.Include(expression)).Where(predicate.Compile());
        }

        public IDbContextTransaction BeginTransaction() {
            return this.db.Database.BeginTransaction();
        }

        public void SaveChanges() {
            this.db.SaveChanges();
        }

    }

}