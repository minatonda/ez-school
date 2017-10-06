using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain;
using Domain.Dto;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories {
    public class CursoRepository : IRepository<Curso> {
        private BaseContext db;

        public CursoRepository (BaseContext db) {
            this.db = db;
        }
        public Curso Add (Curso model) {
            this.db.Cursos.Add (model);
            this.db.SaveChanges ();
            return model;
        }
        public Curso Update (Curso model) {
            this.db.Cursos.Find (model.ID).Nome = model.Nome;
            this.db.Cursos.Find (model.ID).Descricao = model.Descricao;
            this.db.Cursos.Update (this.db.Cursos.Find (model.ID));
            this.db.SaveChanges ();
            return model;
        }
        public void Disable (long ID) {
            this.db.Cursos.Find (ID).Ativo = false;
            this.db.Cursos.Update (this.db.Cursos.Find (ID));
            this.db.SaveChanges ();
        }
        public Curso Get (long ID) => this.db.Cursos.Find (ID);
        public List<Curso> GetAll (bool? ativo) => this.db.Cursos.Where (x => x.Ativo == (ativo.HasValue ? ativo.Value : false)).ToList ();
        public IEnumerable<Curso> Query (Expression<Func<Curso, bool>> predicate, params Expression<Func<Curso, object>>[] includeExpressions) {
            return includeExpressions.Aggregate<Expression<Func<Curso, object>>, IQueryable<Curso>> (db.Cursos, (current, expression) => current.Include (expression)).Where (predicate.Compile ());
        }

        public CursoGradeDto GetGrade (long ID, long IDCursoGrade) {
            return new CursoGradeDto (this.db.CursoGradeMaterias.Include (i => i.CursoGrade).ThenInclude (i => i.Curso).Include (i => i.Materia).Where (x => x.CursoGrade.Curso.ID == ID && x.CursoGrade.ID == IDCursoGrade).ToList ());
        }
        public List<CursoGradeDto> GetGrades (long ID) {
            var listCursoGrade = new List<CursoGradeDto> ();
            foreach (var item in this.db.CursoGradeMaterias.Include (i => i.CursoGrade).ThenInclude (i => i.Curso).Include (i => i.Materia).Where (x => x.CursoGrade.Curso.ID == ID).GroupBy (x => x.CursoGrade.ID)) {
                listCursoGrade.Add (new CursoGradeDto (item.ToList ()));
            }
            return listCursoGrade;
        }
        public CursoGradeDto AddGrade (long ID, CursoGradeDto model) {
            var curso = this.db.Cursos.Find (ID);
            var cursoGrade = new CursoGrade (model, curso);
            this.db.CursoGrades.Add (cursoGrade);
            this.db.CursoGradeMaterias.AddRange (model.Materias.Select (x => new CursoGradeMateria () {
                ID = x.ID,
                    CursoGrade = cursoGrade,
                    Descricao = x.Descricao,
                    Materia = this.db.Materias.Find (x.Materia.ID)
            }).ToList ());
            this.db.SaveChanges ();
            return model;
        }
        public CursoGradeDto UpdateGrade (long ID, CursoGradeDto model) {
            var curso = this.db.Cursos.Find (ID);
            var cursoGrade = new CursoGrade (model, curso);
            this.db.CursoGrades.Attach (cursoGrade);

            var listMaterias = this.db.CursoGradeMaterias.Where (x => x.CursoGrade.ID == cursoGrade.ID).ToList ();

            var listMateriasRemove = listMaterias.Where (x => !model.Materias.Select (y => y.ID).Contains (x.ID)).ToList ();
            var listMateriasAdd = model.Materias.Where (x => !listMaterias.Select (y => y.ID).Contains (x.ID)).Select (x => new CursoGradeMateria () {
                ID = x.ID,
                    CursoGrade = cursoGrade,
                    Descricao = x.Descricao,
                    Materia = this.db.Materias.Find (x.Materia.ID)
            }).ToList ();

            this.db.CursoGradeMaterias.AddRange (listMateriasAdd);
            this.db.CursoGradeMaterias.RemoveRange (listMateriasRemove);

            this.db.SaveChanges ();
            return model;
        }
        public void DeleteGrade (long ID, long IDCursoGrade) {
            this.db.RemoveRange (this.db.CursoGradeMaterias.Include (i => i.CursoGrade).ThenInclude (i => i.Curso).Where (x => x.CursoGrade.Curso.ID == ID && x.CursoGrade.ID == IDCursoGrade).ToList ());
            this.db.SaveChanges ();
        }

    }

}