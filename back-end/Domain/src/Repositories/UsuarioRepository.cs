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
            this.db.SaveChanges();

            this.db.Attach(model.UsuarioInfo);
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

        public void Delete(string ID)
        {
            this.db.Usuarios.Find(ID).Ativo = false;

            this.db.Usuarios.Update(this.db.Usuarios.Find(ID));
            this.db.SaveChanges();
        }

        public Usuario Get(string ID) => this.db.Usuarios.Include(i => i.UsuarioInfo).SingleOrDefault(x => x.ID == ID);
        public Usuario GetByRG(string rg) => this.db.Usuarios.Include(i => i.UsuarioInfo).SingleOrDefault(x => x.UsuarioInfo.RG == rg);
        public UsuarioInfo GetInfo(string ID) => this.db.UsuariosInfo.Find(ID);
        public UsuarioInfo GetInfoByRG(string rg) => this.db.UsuariosInfo.SingleOrDefault(x => x.RG == rg);
        public List<Usuario> GetAll(bool? ativo) => this.db.Usuarios.Where(x => x.Ativo == (ativo.HasValue ? ativo.Value : false)).ToList();

        public IEnumerable<Usuario> Query(Expression<Func<Usuario, bool>> predicate, params Expression<Func<Usuario, object>>[] includeExpressions)
        {
            return includeExpressions.Aggregate<Expression<Func<Usuario, object>>, IQueryable<Usuario>>(db.Usuarios, (current, expression) => current.Include(expression)).Where(predicate.Compile());
        }

        public void Delete(long ID)
        {
            throw new NotImplementedException();
        }

        public Usuario Get(long ID)
        {
            throw new NotImplementedException();
        }
    }

}