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

    public class AreaInteresseRepository : IRepository<AreaInteresse> {

        private BaseContext db;

        public AreaInteresseRepository(BaseContext db) {
            this.db = db;
        }

        public void Add(AreaInteresse model) {

            if (model.UsuarioInfo != null) {
                model.UsuarioInfo = this.db.UsuarioInfos.Find(model.UsuarioInfo.ID);
            }
            if (model.UsuarioInfo != null) {
                model.CategoriaProfissional = this.db.CategoriasProfissionais.Find(model.CategoriaProfissional.ID);
            }

            this.db.AreasInteresses.Add(model);
        }

        public void Update(AreaInteresse model) {
            var attachedAreaInteresse = this.db.AreasInteresses.Find(model.ID);

            attachedAreaInteresse.UsuarioInfo = model.UsuarioInfo;
            attachedAreaInteresse.CategoriaProfissional = model.CategoriaProfissional;
            attachedAreaInteresse.Descricao = model.Descricao;

            this.db.AreasInteresses.Update(attachedAreaInteresse);
        }

        public void Disable(long ID) {
            var areaInteresse = this.db.AreasInteresses.Find(ID);
            areaInteresse.Ativo = DateTime.Now;
            this.db.AreasInteresses.Update(areaInteresse);
        }

        public AreaInteresse Get(long ID) {
            return this.db.AreasInteresses
            .AsNoTracking()
            .Include(i => i.UsuarioInfo)
            .Include(i => i.CategoriaProfissional)
            .SingleOrDefault(x => x.ID == ID);
        }

        public List<AreaInteresse> GetAll(bool ativo) {
            return this.db.AreasInteresses
            .AsNoTracking()
            .Include(i => i.UsuarioInfo)
            .Include(i => i.CategoriaProfissional)
            .Where(x => x.Ativo.HasValue == !ativo)
            .ToList();
        }

        public List<AreaInteresse> GetAllByUsuario(string usuarioId, bool ativo) {
            return this.db.AreasInteresses
            .AsNoTracking()
            .Include(i => i.UsuarioInfo)
            .Include(i => i.CategoriaProfissional)
            .Where(x => x.Ativo.HasValue == !ativo && x.UsuarioInfo.ID == usuarioId)
            .ToList();
        }

        public IEnumerable<AreaInteresse> Query(Expression<Func<AreaInteresse, bool>> predicate, params Expression<Func<AreaInteresse, object>>[] includeExpressions) {
            return includeExpressions.Aggregate<Expression<Func<AreaInteresse, object>>, IQueryable<AreaInteresse>>(db.AreasInteresses, (current, expression) => current.Include(expression)).Where(predicate.Compile());
        }

        public IDbContextTransaction BeginTransaction() {
            return this.db.Database.BeginTransaction();
        }
        public void SaveChanges() {
            this.db.SaveChanges();
        }

    }

}