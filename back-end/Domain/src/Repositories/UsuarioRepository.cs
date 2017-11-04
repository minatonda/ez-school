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
        public List<AreaInteresse> GetAlunoAreaInteresse(string ID) => this.db.AreaInteresse.Include(i => i.Aluno).Include(x => x.CategoriaProfissional).Where(x => x.Aluno.ID == ID).ToList();

        public Aluno UpdateAluno(Aluno model, List<AreaInteresse> AreaInteresses)
        {
            var aluno = this.db.Alunos.Include(x => x.UsuarioInfo).SingleOrDefault(x => x.UsuarioInfo.ID == model.ID);

            var areaInteresse = this.GetAlunoAreaInteresse(model.ID);

            var areaInteressesRemove = areaInteresse.Where(x => !AreaInteresses.Select(y => y.ID).Contains(x.ID)).ToList();
            
            var areaInteressesAdd = AreaInteresses.Where(x => !areaInteresse.Select(y => y.ID).Contains(x.ID)).ToList();
            areaInteressesAdd.ForEach(x => x.CategoriaProfissional = this.db.CategoriaProfissionais.Find(x.CategoriaProfissional.ID));
            areaInteressesAdd.ForEach(x => x.Aluno = model);

            this.db.AreaInteresse.AddRange(areaInteressesAdd);
            this.db.AreaInteresse.RemoveRange(areaInteressesRemove);

            this.db.Alunos.Update(aluno);
            this.db.SaveChanges();
            return aluno;
        }

        //public List<CategoriaProfissional> GetCategoriaProfissional(long ID) {
        //    return this.db.CategoriaProfissionais.Include(i => i.ID).Include(i => i.Materia).Where(x => x.CursoGrade.ID == IDCursoGrade && x.CursoGrade.Curso.ID == ID).ToList();
        //}

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