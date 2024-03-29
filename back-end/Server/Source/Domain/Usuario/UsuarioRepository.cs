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

            this.db.Enderecos.Add(model.UsuarioInfo.Endereco);

            if (model.UsuarioInfo.Pai != null) {
                model.UsuarioInfo.Pai = this.db.UsuarioInfos.SingleOrDefault(x => x.ID == model.UsuarioInfo.Pai.ID);
            }

            if (model.UsuarioInfo.Mae != null) {
                model.UsuarioInfo.Mae = this.db.UsuarioInfos.SingleOrDefault(x => x.ID == model.UsuarioInfo.Mae.ID);
            }

            this.db.UsuarioInfos.Add(model.UsuarioInfo);

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
            var attachedUsuario = this.db.Usuarios.SingleOrDefault(x => x.ID == model.ID);
            attachedUsuario.Username = model.Username;
            if (model.Password != null && model.Password.Trim() != "" && model.Password.Trim() != "********") {
                attachedUsuario.Password = model.Password;
            }
            this.UpdateEndereco(model.UsuarioInfo.Endereco);
            this.UpdateUsuarioInfo(model.UsuarioInfo);

            this.db.Usuarios.Update(attachedUsuario);
        }

        public void UpdateUsuarioInfo(UsuarioInfo model) {
            var attachedUsuarioInfo = this.db.UsuarioInfos.SingleOrDefault(x => x.ID == model.ID);
            attachedUsuarioInfo.Nome = model.Nome;
            attachedUsuarioInfo.DataNascimento = model.DataNascimento;
            attachedUsuarioInfo.Email = model.Email;
            attachedUsuarioInfo.Telefone = model.Telefone;
            attachedUsuarioInfo.CPF = model.CPF;
            attachedUsuarioInfo.RG = model.RG;
            attachedUsuarioInfo.Roles = model.Roles;
            attachedUsuarioInfo.Genero = model.Genero;
            attachedUsuarioInfo.EstadoCivil = model.EstadoCivil;

            if (model.Pai != null) {
                attachedUsuarioInfo.Pai = this.db.UsuarioInfos.SingleOrDefault(x => x.ID == model.Pai.ID);
            } else {
                attachedUsuarioInfo.Pai = null;
            }

            if (model.Mae != null) {
                attachedUsuarioInfo.Mae = this.db.UsuarioInfos.SingleOrDefault(x => x.ID == model.Mae.ID);
            } else {
                attachedUsuarioInfo.Mae = null;
            }

            this.db.UsuarioInfos.Update(attachedUsuarioInfo);
        }

        public void UpdateEndereco(Endereco model) {
            var attachedEndereco = this.db.Enderecos.SingleOrDefault(x => x.ID == model.ID);
            attachedEndereco.Rua = model.Rua;
            attachedEndereco.Complemento = model.Complemento;
            attachedEndereco.Numero = model.Numero;
            attachedEndereco.Bairro = model.Bairro;
            attachedEndereco.Cidade = model.Cidade;
            attachedEndereco.Estado = model.Estado;
            attachedEndereco.Lat = model.Lat;
            attachedEndereco.Lon = model.Lon;

            this.db.Enderecos.Update(attachedEndereco);
        }

        public void UpdateAluno(Aluno model) {
            var aluno = this.db.Alunos.Include(x => x.UsuarioInfo).SingleOrDefault(x => x.UsuarioInfo.ID == model.ID);
            this.db.Alunos.Update(aluno);
        }

        public void UpdateProfessor(Professor model) {
            var professor = this.db.Professores.Include(x => x.UsuarioInfo).SingleOrDefault(x => x.UsuarioInfo.ID == model.ID);
            this.db.Professores.Update(professor);
        }

        public void Disable(string id) {
            var usuario = this.db.Usuarios.Find(id);
            usuario.Ativo = DateTime.Now;
            this.db.Usuarios.Update(usuario);
        }

        public Usuario Get(string ID) {
            return this.db.Usuarios
            .AsNoTracking()
            .Include(i => i.UsuarioInfo)
            .ThenInclude(i => i.Endereco)
            .Include(i => i.UsuarioInfo)
            .ThenInclude(i => i.Pai)
            .Include(i => i.UsuarioInfo)
            .ThenInclude(i => i.Mae)
            .SingleOrDefault(x => x.ID == ID);
        }

        public Usuario GetByUsernameOrRgOrCPForEmail(string username, string rg, string cpf, string email) {
            return this.db.Usuarios
            .AsNoTracking()
            .Include(i => i.UsuarioInfo)
            .ThenInclude(i => i.Endereco)
            .Include(i => i.UsuarioInfo)
            .ThenInclude(i => i.Pai)
            .Include(i => i.UsuarioInfo)
            .ThenInclude(i => i.Mae)
            .FirstOrDefault(x => x.Username.ToLower() == username.ToLower() || x.UsuarioInfo.RG.ToLower() == rg.ToLower() || x.UsuarioInfo.CPF.ToLower() == cpf.ToLower() || x.UsuarioInfo.Email.ToLower() == email.ToLower() && !x.Ativo.HasValue);
        }

        public UsuarioInfo GetUsuarioInfo(string id) {
            return this.db.UsuarioInfos
            .AsNoTracking()
            .Include(i => i.Endereco)
            .Include(i => i.Pai)
            .Include(i => i.Mae)
            .SingleOrDefault(x => x.ID == id);
        }

        public Usuario GetByRG(string rg) {
            return this.db.Usuarios
            .AsNoTracking()
            .Include(i => i.UsuarioInfo)
            .SingleOrDefault(x => x.UsuarioInfo.RG == rg);
        }

        public UsuarioInfo GetInfoByRG(string rg) {
            return this.db.UsuarioInfos
            .AsNoTracking()
            .Include(i => i.Endereco)
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
            var query = this.db.UsuarioInfos
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