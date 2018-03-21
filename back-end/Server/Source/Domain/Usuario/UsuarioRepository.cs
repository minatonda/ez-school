using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain;
using Domain.AreaInteresseDomain;
using Domain.Common;
using Domain.EnderecoDomain;
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

            this.db.Endrc.Add(model.UsuarioInfo.Endereco);

            if (model.UsuarioInfo.Pai != null) {
                model.UsuarioInfo.Pai = this.db.UsrInf.SingleOrDefault(x => x.ID == model.UsuarioInfo.Pai.ID);
            }

            if (model.UsuarioInfo.Mae != null) {
                model.UsuarioInfo.Mae = this.db.UsrInf.SingleOrDefault(x => x.ID == model.UsuarioInfo.Mae.ID);
            }

            this.db.UsrInf.Add(model.UsuarioInfo);

            var aluno = new Aluno();
            aluno.ID = model.ID;
            aluno.UsuarioInfo = model.UsuarioInfo;
            this.db.Aln.Add(aluno);

            var professor = new Professor();
            professor.ID = model.ID;
            aluno.UsuarioInfo = model.UsuarioInfo;
            this.db.Prf.Add(professor);


            this.db.Usr.Add(model);
        }

        public void Update(Usuario model) {
            var attachedUsuario = this.db.Usr.SingleOrDefault(x => x.ID == model.ID);
            attachedUsuario.Username = model.Username;
            attachedUsuario.Password = model.Password;

            this.UpdateEndereco(model.UsuarioInfo.Endereco);
            this.UpdateUsuarioInfo(model.UsuarioInfo);

            this.db.Usr.Update(attachedUsuario);
        }

        public void UpdateUsuarioInfo(UsuarioInfo model) {
            var attachedUsuarioInfo = this.db.UsrInf.SingleOrDefault(x => x.ID == model.ID);
            attachedUsuarioInfo.Nome = model.Nome;
            attachedUsuarioInfo.DataNascimento = model.DataNascimento;
            attachedUsuarioInfo.CPF = model.CPF;
            attachedUsuarioInfo.RG = model.RG;
            attachedUsuarioInfo.Roles = model.Roles;
            attachedUsuarioInfo.Genero = model.Genero;
            attachedUsuarioInfo.EstadoCivil = model.EstadoCivil;

            if (model.Pai != null) {
                attachedUsuarioInfo.Pai = this.db.UsrInf.SingleOrDefault(x => x.ID == model.Pai.ID);
            } else {
                attachedUsuarioInfo.Pai = null;
            }

            if (model.Mae != null) {
                attachedUsuarioInfo.Mae = this.db.UsrInf.SingleOrDefault(x => x.ID == model.Mae.ID);
            } else {
                attachedUsuarioInfo.Mae = null;
            }

            this.db.UsrInf.Update(attachedUsuarioInfo);
        }

        public void UpdateEndereco(Endereco model) {
            var attachedEndereco = this.db.Endrc.SingleOrDefault(x => x.ID == model.ID);
            attachedEndereco.Rua = model.Rua;
            attachedEndereco.Complemento = model.Complemento;
            attachedEndereco.Numero = model.Numero;
            attachedEndereco.Bairro = model.Bairro;
            attachedEndereco.Cidade = model.Cidade;
            attachedEndereco.Estado = model.Estado;
            attachedEndereco.Lat = model.Lat;
            attachedEndereco.Lon = model.Lon;

            this.db.Endrc.Update(attachedEndereco);
        }

        public void UpdateAluno(Aluno model) {
            var aluno = this.db.Aln.Include(x => x.UsuarioInfo).SingleOrDefault(x => x.UsuarioInfo.ID == model.ID);
            this.db.Aln.Update(aluno);
        }

        public void UpdateProfessor(Professor model) {
            var professor = this.db.Prf.Include(x => x.UsuarioInfo).SingleOrDefault(x => x.UsuarioInfo.ID == model.ID);
            this.db.Prf.Update(professor);
        }

        public void Disable(string ID) {
            var usuario = this.db.Usr.Find(ID);
            usuario.Ativo = DateTime.Now;
            this.db.Usr.Update(usuario);
        }

        public Usuario Get(string ID) {
            return this.db.Usr
            .AsNoTracking()
            .Include(i => i.UsuarioInfo)
            .ThenInclude(i => i.Endereco)
            .Include(i => i.UsuarioInfo)
            .ThenInclude(i => i.Pai)
            .Include(i => i.UsuarioInfo)
            .ThenInclude(i => i.Mae)
            .SingleOrDefault(x => x.ID == ID);
        }

        public UsuarioInfo GetUsuarioInfo(string ID) {
            return this.db.UsrInf
            .AsNoTracking()
            .Include(i => i.Endereco)
            .Include(i => i.Pai)
            .Include(i => i.Mae)
            .SingleOrDefault(x => x.ID == ID);
        }

        public Usuario GetByRG(string rg) {
            return this.db.Usr
            .AsNoTracking()
            .Include(i => i.UsuarioInfo)
            .SingleOrDefault(x => x.UsuarioInfo.RG == rg);
        }

        public UsuarioInfo GetInfoByRG(string rg) {
            return this.db.UsrInf
            .AsNoTracking()
            .Include(i => i.Endereco)
            .SingleOrDefault(x => x.RG == rg);
        }

        public List<Usuario> GetAll(bool ativo) {
            return this.db.Usr
            .AsNoTracking()
            .Include(i => i.UsuarioInfo)
            .Where(x => x.Ativo.HasValue == !ativo)
            .ToList();
        }

        public List<UsuarioInfo> GetAllByTermo(string termo, bool onlyAluno, bool onlyProfessor) {
            var query = this.db.UsrInf
            .AsNoTracking()
            .Where(x => x.Nome.ToLower().Contains(termo.ToLower()) || x.RG.ToLower().Contains(termo.ToLower()) || x.CPF.ToLower().Contains(termo.ToLower()));

            if (onlyAluno) {
                var idAlunosAtivos = this.db.Aln.Where(x => !x.Ativo.HasValue).Select(x => x.UsuarioInfo.ID);
                query = query.Where(x => idAlunosAtivos.Contains(x.ID));
            }

            if (onlyProfessor) {
                var idProfessoresAtivos = this.db.Prf.Where(x => !x.Ativo.HasValue).Select(x => x.UsuarioInfo.ID);
                query = query.Where(x => idProfessoresAtivos.Contains(x.ID));
            }

            return query.ToList();
        }

        public Aluno GetAluno(string ID) {
            return this.db.Aln
            .AsNoTracking()
            .Include(i => i.UsuarioInfo)
            .SingleOrDefault(x => x.UsuarioInfo.ID == ID);
        }

        public Professor GetProfessor(string ID) {
            return this.db.Prf
            .AsNoTracking()
            .Include(i => i.UsuarioInfo)
            .SingleOrDefault(x => x.UsuarioInfo.ID == ID);
        }

        public IEnumerable<Usuario> Query(Expression<Func<Usuario, bool>> predicate, params Expression<Func<Usuario, object>>[] includeExpressions) {
            return includeExpressions.Aggregate<Expression<Func<Usuario, object>>, IQueryable<Usuario>>(db.Usr, (current, expression) => current.Include(expression)).Where(predicate.Compile());
        }

        public IDbContextTransaction BeginTransaction() {
            return this.db.Database.BeginTransaction();
        }

        public void SaveChanges() {
            this.db.SaveChanges();
        }

    }

}