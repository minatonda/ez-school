using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories
{
    public class CursoRepository : IRepository<Curso>
    {
        private BaseContext db;

        public CursoRepository(BaseContext db)
        {
            this.db = db;
        }
        public Curso Add(Curso model)
        {
            this.db.Cursos.Add(model);
            this.db.SaveChanges();
            return model;
        }
        public Curso Update(Curso model)
        {
            this.db.Cursos.Find(model.ID).Nome = model.Nome;
            this.db.Cursos.Find(model.ID).Descricao = model.Descricao;
            this.db.Cursos.Update(this.db.Cursos.Find(model.ID));
            this.db.SaveChanges();
            return model;
        }
        public void Delete(long ID)
        {
            this.db.Cursos.Find(ID).Ativo = false;
            this.db.Cursos.Update(this.db.Cursos.Find(ID));
            this.db.SaveChanges();
        }
        public Curso Get(long ID) => this.db.Cursos.Find(ID);
        public List<Curso> GetAll(bool? ativo) => this.db.Cursos.Where(x => x.Ativo == (ativo.HasValue ? ativo.Value : false)).ToList();
        public IEnumerable<Curso> Query(Expression<Func<Curso, bool>> predicate, params Expression<Func<Curso, object>>[] includeExpressions)
        {
            return includeExpressions.Aggregate<Expression<Func<Curso, object>>, IQueryable<Curso>>(db.Cursos, (current, expression) => current.Include(expression)).Where(predicate.Compile());
        }

        public List<CursoGrade> GetGrades(long ID) => this.db.CursoGrades.Include(i => i.Curso).Where(x => x.Curso.ID == ID).ToList();
        public CursoGrade AddGrade(long ID, CursoGrade model, List<Materia> materias)
        {
            if (model.Curso != null)
            {
                this.db.Attach(model.Curso);
            }
            else
            {
                model.Curso = this.Get(ID);
            }
            this.db.CursoGrades.Add(model);
            foreach (var materia in materias)
            {
                this.db.Attach(materia);
                this.db.CursoGradeMaterias.Add(new CursoGradeMateria()
                {
                    CursoGrade = model,
                    Materia = materia
                });
            }
            this.db.SaveChanges();
            return model;
        }
        public void DeleteGrade(long ID, long IDGrade)
        {
            this.db.CursoGrades.Include(i => i.Curso).SingleOrDefault(x => x.ID == IDGrade && x.Curso.ID == ID).Ativo = false;
            this.db.SaveChanges();
        }
        public CursoGrade GetGrade(long ID, long IDGrade)
        {
            return this.GetGrades(ID).SingleOrDefault(x => x.ID == IDGrade);
        }
        public void AddGradeMateria(long ID, long IDGrade, Materia model)
        {
            this.db.Attach(model);
            this.db.CursoGradeMaterias.Add(new CursoGradeMateria()
            {
                CursoGrade = this.GetGrade(ID, IDGrade),
                Materia = model
            });
            this.db.SaveChanges();
        }
        public void DeleteGradeMateria(long ID, long IDGrade, long IDMateria)
        {
            this.db.CursoGradeMaterias.Remove(this.db.CursoGradeMaterias.Include(i => i.CursoGrade).Include(i => i.Materia).SingleOrDefault(x => x.CursoGrade.ID == IDGrade && x.Materia.ID == IDMateria));
            this.db.SaveChanges();
        }
        public List<Materia> GetGradeMaterias(long ID, long IDGrade)
        {
            return this.db.CursoGradeMaterias.Include(i => i.CursoGrade).Include(i => i.Materia).Where(x => x.CursoGrade.ID == IDGrade).Select(x => x.Materia).ToList();
        }

    }

}