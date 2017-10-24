using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories {
    public class InstituicaoRepository : IRepository<Instituicao> {
        private BaseContext db;
        private CursoRepository cursoRepository;

        public InstituicaoRepository(BaseContext db, CursoRepository cursoRepository) {
            this.db = db;
            this.cursoRepository = cursoRepository;
        }
        public Instituicao Add(Instituicao model) {
            this.db.Instituicoes.Add(model);
            this.db.SaveChanges();
            return model;
        }
        public Instituicao Update(Instituicao model) {
            this.db.Instituicoes.Find(model.ID).Nome = model.Nome;
            this.db.Instituicoes.Find(model.ID).CNPJ = model.CNPJ;
            this.db.Instituicoes.Update(this.db.Instituicoes.Find(model.ID));
            this.db.SaveChanges();
            return model;
        }
        public void Disable(long ID) {
            this.db.Instituicoes.Find(ID).Ativo = false;
            this.db.Instituicoes.Update(this.db.Instituicoes.Find(ID));
            this.db.SaveChanges();
        }
        public Instituicao Get(long ID) => this.db.Instituicoes.Find(ID);
        public List<Instituicao> GetAll(bool? ativo) => this.db.Instituicoes.Where(x => x.Ativo == (ativo.HasValue ? ativo.Value : false)).ToList();
        public IEnumerable<Instituicao> Query(Expression<Func<Instituicao, bool>> predicate, params Expression<Func<Instituicao, object>>[] includeExpressions) {
            return includeExpressions.Aggregate<Expression<Func<Instituicao, object>>, IQueryable<Instituicao>>(db.Instituicoes, (current, expression) => current.Include(expression)).Where(predicate.Compile());
        }

        public List<InstituicaoCategoria> GetCategorias(long ID) => this.db.InstituicaoInstituicaoCategorias.Include(i => i.InstituicaoCategoria).Include(i => i.Instituicao).Where(x => x.Instituicao.ID == ID).Select(i => i.InstituicaoCategoria).ToList();
        public void AddCategoria(long ID, InstituicaoCategoria instituicaoCategoria) {
            this.db.Attach(instituicaoCategoria);
            this.db.InstituicaoInstituicaoCategorias.Add(new InstituicaoInstituicaoCategoria() {
                Instituicao = this.Get(ID),
                InstituicaoCategoria = instituicaoCategoria
            });
            this.db.SaveChanges();
        }
        public void DeleteCategoria(long ID, long IDCategoria) {
            var instituicaoCategoria = this.db.InstituicaoInstituicaoCategorias.Include(i => i.Instituicao).Include(i => i.InstituicaoCategoria).SingleOrDefault(x => x.Instituicao.ID == ID && x.InstituicaoCategoria.ID == IDCategoria);
            this.db.InstituicaoInstituicaoCategorias.Remove(instituicaoCategoria);
            this.db.SaveChanges();
        }

        public InstituicaoCurso GetCurso(long ID, long IDCurso, DateTime? dataInicio) {
            var where = this.db.InstituicaoCursos
            .Include(i => i.Curso)
            .Include(i => i.Instituicao)
            .Include(i => i.CursoGrade)
            .Where(x => x.Instituicao.ID == ID && x.Curso.ID == IDCurso);
            return where.SingleOrDefault(x => x.DataInicio.Date == dataInicio.Value.Date);
        }

        public List<InstituicaoCursoTurma> GetCursoTurmas(long ID, long IDCurso, DateTime? dataInicio) {
            var curso = this.GetCurso(ID, IDCurso, dataInicio);
            return this.db.InstituicaoCursoTurmas
             .Include(i => i.InstituicaoCurso)
             .Where(x => x.InstituicaoCurso.ID == curso.ID).ToList();
        }

        public List<InstituicaoCursoPeriodo> GetCursoPeriodos(long ID, long IDCurso, DateTime? dataInicio) {
            var curso = this.GetCurso(ID, IDCurso, dataInicio);
            return this.db.InstituicaoCursoPeriodos
             .Include(i => i.InstituicaoCurso)
             .Where(x => x.InstituicaoCurso.ID == curso.ID).ToList();
        }

        public List<InstituicaoCurso> GetCursos(long ID) {
            return this.db.InstituicaoCursos.Include(i => i.Curso).Include(i => i.Instituicao).Include(i => i.CursoGrade).Where(x => x.Instituicao.ID == ID && !x.DataExpiracao.HasValue && x.Ativo).ToList();
        }
        public void AddCurso(long ID, InstituicaoCurso model, List<InstituicaoCursoPeriodo> periodos, List<InstituicaoCursoTurma> turmas) {
            if (this.db.InstituicaoCursos.Include(i => i.Curso).Include(i => i.Instituicao).SingleOrDefault(x => x.Curso.ID == model.Curso.ID && x.Instituicao.ID == ID && x.DataExpiracao != null && x.Ativo) != null) {
                throw new Exception("O Curso já existe, as operações permitidas são renovar e desativar.");
            } else {
                model.Instituicao = this.Get(ID);
                model.Curso = this.db.Cursos.Find(model.Curso.ID);
                model.CursoGrade = this.db.CursoGrades.Find(model.CursoGrade.ID);

                periodos.ForEach(x => x.InstituicaoCurso = model);
                this.db.InstituicaoCursoPeriodos.AddRange(periodos);

                turmas.ForEach(x => x.InstituicaoCurso = model);
                this.db.InstituicaoCursoTurmas.AddRange(turmas);

                this.db.InstituicaoCursos.Add(model);
                this.db.SaveChanges();
            }
        }
        public void RenewCurso(long ID, InstituicaoCurso model, List<InstituicaoCursoPeriodo> periodos, List<InstituicaoCursoTurma> turmas) {
            this.db.InstituicaoCursos.SingleOrDefault(x => x.Curso.ID == model.Curso.ID && x.Instituicao.ID == ID && x.DataExpiracao == null && x.Ativo).DataExpiracao = DateTime.Now;
            this.AddCurso(ID, model, periodos, turmas);
        }
        public void DisableCurso(long ID, long IDCurso) {
            var instituicaoCurso = this.db.InstituicaoCursos.Include(i => i.Instituicao).Include(i => i.Curso).SingleOrDefault(x => x.Instituicao.ID == ID && x.Curso.ID == IDCurso && x.DataExpiracao == null && x.Ativo);
            instituicaoCurso.Ativo = false;
            instituicaoCurso.DataExpiracao = DateTime.Now;
            this.db.SaveChanges();
        }

        public InstituicaoCursoOcorrencia GetCursoOcorrencia(long ID, long IDCurso, DateTime? dataInicio, DateTime? instituicaoCursoOcorrenciaDataInicio) {
            return this.db.InstituicaoCursoOcorrencias
            .Include(i => i.InstituicaoCurso)
            .SingleOrDefault(x => x.InstituicaoCurso.ID == this.GetCurso(ID, IDCurso, dataInicio).ID && x.DataInicio.HasValue && x.DataInicio.Value.Date.Equals(dataInicio.Value.Date));
        }

        public List<InstituicaoCursoOcorrenciaMateria> GetInstituicaoCursoOcorrenciaMaterias(InstituicaoCursoOcorrencia instituicaoCursoOcorrencia) {
            return this.db.InstituicaoCursoOcorrenciaMaterias
            .Include(x => x.InstituicaoCursoOcorrencia)
            .Include(x => x.Materia)
            .Where(x => x.InstituicaoCursoOcorrencia.ID == instituicaoCursoOcorrencia.ID).ToList();
        }

        public List<InstituicaoCursoOcorrenciaMateriaProfessor> GetInstituicaoCursoOcorrenciaMateriaProfessor(InstituicaoCursoOcorrencia instituicaoCursoOcorrencia) {
            return this.db.InstituicaoCursoOcorrenciaMateriaProfessores
            .Include(i => i.Periodo)
            .Include(i => i.Professor)
            .Include(i => i.InstituicaoCursoOcorrenciaMateria)
            .ThenInclude(i => i.Materia)
            .Include(i => i.InstituicaoCursoOcorrenciaMateria)
            .ThenInclude(i => i.InstituicaoCursoOcorrencia)
            .Where(x => x.InstituicaoCursoOcorrenciaMateria.InstituicaoCursoOcorrencia.ID == instituicaoCursoOcorrencia.ID)
            .ToList();
        }

        public List<InstituicaoCursoOcorrenciaAluno> GetInstituicaoCursoOcorrenciaAluno(InstituicaoCursoOcorrencia instituicaoCursoOcorrencia) {
            return this.db.InstituicaoCursoOcorrenciaAlunos
            .Include(i => i.InstituicaoCursoOcorrencia)
            .Include(i => i.Periodo)
            .Include(i => i.Aluno)
            .Where(x => x.InstituicaoCursoOcorrencia.ID == instituicaoCursoOcorrencia.ID)
            .ToList();
        }

        public InstituicaoCursoOcorrenciaCoordenador GetInstituicaoCursoOcorrenciaCoordenador(InstituicaoCursoOcorrencia instituicaoCursoOcorrencia) {
            return this.db.InstituicaoCursoOcorrenciaCoordenadores
            .SingleOrDefault(x => x.InstituicaoCursoOcorrencia.ID == instituicaoCursoOcorrencia.ID && x.DataExpiracao == null);
        }

    }

}