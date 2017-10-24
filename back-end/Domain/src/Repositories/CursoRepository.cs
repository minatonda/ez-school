using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories {
    public class CursoRepository : IRepository<Curso> {
        private BaseContext db;

        public CursoRepository(BaseContext db) {
            this.db = db;
        }
        public Curso Add(Curso model) {
            this.db.Cursos.Add(model);
            this.db.SaveChanges();
            return model;
        }
        public Curso Update(Curso model) {
            this.db.Cursos.Find(model.ID).Nome = model.Nome;
            this.db.Cursos.Find(model.ID).Descricao = model.Descricao;
            this.db.Cursos.Update(this.db.Cursos.Find(model.ID));
            this.db.SaveChanges();
            return model;
        }
        public void Disable(long ID) {
            this.db.Cursos.Find(ID).Ativo = false;
            this.db.Cursos.Update(this.db.Cursos.Find(ID));
            this.db.SaveChanges();
        }
        public Curso Get(long ID) => this.db.Cursos.Find(ID);
        public List<Curso> GetAll(bool? ativo) => this.db.Cursos.Where(x => x.Ativo == (ativo.HasValue ? ativo.Value : false)).ToList();
        public IEnumerable<Curso> Query(Expression<Func<Curso, bool>> predicate, params Expression<Func<Curso, object>>[] includeExpressions) {
            return includeExpressions.Aggregate<Expression<Func<Curso, object>>, IQueryable<Curso>>(db.Cursos, (current, expression) => current.Include(expression)).Where(predicate.Compile());
        }

        public CursoGrade GetGrade(long ID, long IDCursoGrade) {
            return this.db.CursoGrades.Include(i => i.Curso).SingleOrDefault(x => x.Curso.ID == ID && x.ID == IDCursoGrade);
        }
        public List<CursoGrade> GetGrades(long ID) {
            return this.db.CursoGrades.Include(i => i.Curso).Where(x => x.Curso.ID == ID).ToList();
        }

        public CursoGrade AddGrade(long ID, CursoGrade model, List<CursoGradeMateria> materias) {
            var curso = this.db.Cursos.Find(ID);
            model.Curso = curso;
            materias.ForEach(x => x.CursoGrade = model);
            materias.ForEach(x => x.Materia = this.db.Materias.Find(x.Materia.ID));
            this.db.CursoGrades.Add(model);
            this.db.CursoGradeMaterias.AddRange(materias);
            this.db.SaveChanges();

            return model;
        }
        public CursoGrade UpdateGrade(long ID, CursoGrade model, List<CursoGradeMateria> materias) {

            var curso = this.db.Cursos.Find(ID);

            var cursoGrade = this.GetGrade(ID, model.ID);

            var materiasCurso = this.GetGradeMaterias(ID, model.ID);

            var materiasRemove = materiasCurso.Where(x => !materias.Select(y => y.ID).Contains(x.ID));
            var materiasAdd = materias.Where(x => !materiasCurso.Select(y => y.ID).Contains(x.ID));

            this.db.CursoGradeMaterias.AddRange(materiasAdd);
            this.db.CursoGradeMaterias.RemoveRange(materiasRemove);

            this.db.SaveChanges();
            return model;
        }

        public List<CursoGradeMateria> GetGradeMaterias(long ID, long IDCursoGrade) {
            return this.db.CursoGradeMaterias.Include(i => i.CursoGrade).Include(i => i.Materia).Where(x => x.CursoGrade.ID == IDCursoGrade && x.CursoGrade.Curso.ID == ID).ToList();
        }
        public void DeleteGrade(long ID, long IDCursoGrade) {
            this.db.RemoveRange(this.db.CursoGradeMaterias.Include(i => i.CursoGrade).ThenInclude(i => i.Curso).Where(x => x.CursoGrade.Curso.ID == ID && x.CursoGrade.ID == IDCursoGrade).ToList());
            this.db.SaveChanges();
        }

    }

}