using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories {
    public class ProfessorRepository : IRepository<Professor> {
        private BaseContext db;
        private UsuarioRepository usuarioRepository;

        public ProfessorRepository(BaseContext db, UsuarioRepository usuarioRepository) {
            this.db = db;
            this.usuarioRepository = usuarioRepository;
        }
        public Professor Add(Professor model) {
            if (model.UsuarioInfo != null) {
                this.db.Attach(model.UsuarioInfo);
            }
            this.db.Professores.Add(model);
            this.db.SaveChanges();
            return model;
        }
        public Professor AddByRG(Professor model, string rg) {
            var usuario = this.usuarioRepository.GetInfoByRG(rg);
            if (usuario != null) {
                model.UsuarioInfo = usuario;
            }
            this.db.Professores.Add(model);
            this.db.SaveChanges();
            return model;
        }
        public Professor Update(Professor model) {
            if (model.UsuarioInfo != null) {
                this.db.Attach(model.UsuarioInfo);
                this.db.Professores.Find(model.ID).ID = model.UsuarioInfo.ID;
                this.db.Professores.Find(model.ID).UsuarioInfo = model.UsuarioInfo;
            }
            this.db.Professores.Update(this.db.Professores.Find(model.ID));
            this.db.SaveChanges();
            return model;
        }
        public void Disable(long ID) {
            this.db.Professores.Find(ID).Ativo = DateTime.Now;
            this.db.Professores.Update(this.db.Professores.Find(ID));
            this.db.SaveChanges();
        }
        public Professor Get(string ID) => this.db.Professores.Include(i => i.UsuarioInfo).SingleOrDefault(x => x.ID == ID);
        public List<Professor> GetAll(bool ativo) {
            if (ativo) {
                return this.db.Professores.Include(i => i.UsuarioInfo).Where(x => !x.Ativo.HasValue).ToList();
            } else {
                return this.db.Professores.Include(i => i.UsuarioInfo).ToList();
            }
        }
        public IEnumerable<Professor> Query(Expression<Func<Professor, bool>> predicate, params Expression<Func<Professor, object>>[] includeExpressions) {
            return includeExpressions.Aggregate<Expression<Func<Professor, object>>, IQueryable<Professor>>(db.Professores, (current, expression) => current.Include(expression)).Where(predicate.Compile());
        }

    }

}