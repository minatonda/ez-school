using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories
{
    public class UsuarioRepository : IRepository<Usuario>
    {
        private BaseContext db;

        public UsuarioRepository(BaseContext db)
        {
            this.db = db;
        }

        public Usuario Add(Usuario model)
        {
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
            this.db.SaveChanges();
            return model;
        }
        public Usuario Update(Usuario model)
        {
            if (model.UsuarioInfo != null)
            {
                this.db.Attach(model.UsuarioInfo);
            }

            this.db.Usuarios.Find(model.ID).Username = model.Username;
            this.db.Usuarios.Find(model.ID).Password = model.Password;
            this.db.UsuariosInfo.Find(model.UsuarioInfo.ID).Nome = model.UsuarioInfo.Nome;
            this.db.UsuariosInfo.Find(model.UsuarioInfo.ID).DataNascimento = model.UsuarioInfo.DataNascimento;
            this.db.UsuariosInfo.Find(model.UsuarioInfo.ID).CPF = model.UsuarioInfo.CPF;
            this.db.UsuariosInfo.Find(model.UsuarioInfo.ID).RG = model.UsuarioInfo.RG;

            this.db.Usuarios.Update(this.db.Usuarios.Find(model.ID));
            this.db.SaveChanges();
            return model;
        }

        public void Disable(string ID)
        {
            this.db.Usuarios.Find(ID).Ativo = false;

            this.db.Usuarios.Update(this.db.Usuarios.Find(ID));
            this.db.SaveChanges();
        }

        public Usuario Get(string ID) => this.db.Usuarios.Include(i => i.UsuarioInfo).SingleOrDefault(x => x.ID == ID);
        public Usuario GetByRG(string rg) => this.db.Usuarios.Include(i => i.UsuarioInfo).SingleOrDefault(x => x.UsuarioInfo.RG == rg);
        public UsuarioInfo GetInfo(string ID) => this.db.UsuariosInfo.Find(ID);
        public UsuarioInfo GetInfoByRG(string rg) => this.db.UsuariosInfo.SingleOrDefault(x => x.RG == rg);
        public List<Usuario> GetAll(bool? ativo) => this.db.Usuarios.Include(i => i.UsuarioInfo).Where(x => x.Ativo == (ativo.HasValue ? ativo.Value : false)).ToList();
        public Aluno GetAluno(string ID) => this.db.Alunos.Include(i => i.UsuarioInfo).SingleOrDefault(x => x.UsuarioInfo.ID == ID);

        public Aluno UpdateAluno(Aluno model)
        {
            var aluno = this.db.Alunos.Include(x => x.UsuarioInfo).SingleOrDefault(x => x.UsuarioInfo.ID == model.ID);

            this.db.Alunos.Update(aluno);
            return aluno;
        }

        public Professor GetProfessor(string ID) => this.db.Professores.Include(i => i.UsuarioInfo).SingleOrDefault(x => x.UsuarioInfo.ID == ID);

        public Professor UpdateProfessor(Professor model)
        {
            var professor = this.db.Professores.Include(x => x.UsuarioInfo).SingleOrDefault(x => x.UsuarioInfo.ID == model.ID);

            this.db.Professores.Update(professor);
            return professor;
        }


        public IEnumerable<Usuario> Query(Expression<Func<Usuario, bool>> predicate, params Expression<Func<Usuario, object>>[] includeExpressions)
        {
            return includeExpressions.Aggregate<Expression<Func<Usuario, object>>, IQueryable<Usuario>>(db.Usuarios, (current, expression) => current.Include(expression)).Where(predicate.Compile());
        }

        public void Disable(long ID)
        {
            throw new NotImplementedException();
        }

        public Usuario Get(long ID)
        {
            throw new NotImplementedException();
        }
    }

}