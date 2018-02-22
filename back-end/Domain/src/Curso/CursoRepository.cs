using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Domain.CursoDomain {
    public class CursoRepository : IRepository<Curso> {

        private BaseContext db;

        public CursoRepository(BaseContext db) {
            this.db = db;
        }

        public Curso Add(Curso curso) {
            this.db.Cursos.Add(curso);
            return curso;
        }

        public CursoGrade AddCursoGrade(CursoGrade cursoGrade) {
            cursoGrade.Curso = this.db.Cursos.Find(cursoGrade.Curso.ID);
            this.db.CursoGrades.Add(cursoGrade);
            return cursoGrade;
        }

        public CursoGradeMateria AddCursoGradeMateria(CursoGradeMateria cursoGradeMateria) {
            cursoGradeMateria.CursoGrade = this.db.CursoGrades.Find(cursoGradeMateria.CursoGrade.ID);
            cursoGradeMateria.Materia = this.db.Materias.Find(cursoGradeMateria.Materia.ID);
            this.db.CursoGradeMaterias.Add(cursoGradeMateria);
            return cursoGradeMateria;
        }

        public Curso Update(Curso curso) {
            var model = this.db.Cursos.Find(curso.ID);
            model.Nome = curso.Nome;
            model.Descricao = curso.Descricao;
            this.db.Cursos.Update(model);
            return model;
        }

        public CursoGrade UpdateCursoGrade(CursoGrade cursoGrade) {
            var model = this.db.CursoGrades.Find(cursoGrade.ID);
            model.Curso = this.db.Cursos.Find(cursoGrade.Curso.ID);
            model.Descricao = cursoGrade.Descricao;
            model.DataCriacao = cursoGrade.DataCriacao;
            this.db.CursoGrades.Update(model);
            return model;
        }

        public CursoGradeMateria UpdateCursoGradeMateria(CursoGradeMateria cursoGradeMateria) {
            var model = this.db.CursoGradeMaterias.Find(cursoGradeMateria.ID);
            model.CursoGrade = this.db.CursoGrades.Find(cursoGradeMateria.CursoGrade.ID);
            model.Descricao = cursoGradeMateria.Descricao;
            this.db.CursoGradeMaterias.Update(model);
            return model;
        }

        public void Disable(long id) {
            var model = this.db.Cursos.Find(id);
            model.Ativo = DateTime.Now;
            this.db.Cursos.Update(model);
        }

        public void DisableCursoGrade(long id) {
            var model = this.db.CursoGrades.Find(id);
            model.Ativo = DateTime.Now;
            this.db.CursoGrades.Update(model);
        }

        public void DisableCursoGradeMateria(long id) {
            var model = this.db.CursoGradeMaterias.Find(id);
            model.Ativo = DateTime.Now;
            this.db.CursoGradeMaterias.Update(model);
        }

        public Curso Get(long ID) {
            return this.db.Cursos
            .AsNoTracking()
            .SingleOrDefault(x => x.ID == ID);
        }

        public CursoGrade GetCursoGrade(long id) {
            return this.db.CursoGrades
            .AsNoTracking()
            .Include(i => i.Curso)
            .SingleOrDefault(x => x.ID == id);
        }

        public CursoGradeMateria GetCursoGradeMateria(long id) {
            return this.db.CursoGradeMaterias
            .AsNoTracking()
            .Include(i => i.CursoGrade)
            .Include(i => i.Materia)
            .SingleOrDefault(x => x.ID == id);
        }

        public List<Curso> GetAll(bool ativo) {
            return this.db.Cursos
            .AsNoTracking()
            .Where(x => x.Ativo.HasValue == !ativo)
            .ToList();
        }

        public List<CursoGrade> GetAllCursoGradesByCurso(long id, bool ativo) {
            return this.db.CursoGrades
            .AsNoTracking()
            .Include(i => i.Curso)
            .Where(x => x.Curso.ID == id && x.Ativo.HasValue == !ativo)
            .ToList();
        }

        public List<CursoGradeMateria> GetAllCursoGradeMateriasByCursoGrade(long id, bool ativo) {
            return this.db.CursoGradeMaterias
            .AsNoTracking()
            .Include(i => i.CursoGrade)
            .Include(i => i.Materia)
            .Where(x => x.CursoGrade.ID == id && x.Ativo.HasValue == !ativo)
            .ToList();
        }

        public IEnumerable<Curso> Query(Expression<Func<Curso, bool>> predicate, params Expression<Func<Curso, object>>[] includeExpressions) {
            return includeExpressions.Aggregate<Expression<Func<Curso, object>>, IQueryable<Curso>>(db.Cursos, (current, expression) => current.Include(expression)).Where(predicate.Compile());
        }

        public IDbContextTransaction BeginTransaction() {
            return this.db.Database.BeginTransaction();
        }

        public void SaveChanges() {
            this.db.SaveChanges();
        }

    }

}