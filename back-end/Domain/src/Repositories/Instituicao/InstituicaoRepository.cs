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
        public void Disable(long id) {
            this.db.Instituicoes.Find(id).Ativo = false;
            this.db.Instituicoes.Update(this.db.Instituicoes.Find(id));
            this.db.SaveChanges();
        }
        public Instituicao Get(long id) => this.db.Instituicoes.Find(id);
        public List<Instituicao> GetAll(bool? ativo) => this.db.Instituicoes.Where(x => x.Ativo == (ativo.HasValue ? ativo.Value : false)).ToList();
        public IEnumerable<Instituicao> Query(Expression<Func<Instituicao, bool>> predicate, params Expression<Func<Instituicao, object>>[] includeExpressions) {
            return includeExpressions.Aggregate<Expression<Func<Instituicao, object>>, IQueryable<Instituicao>>(db.Instituicoes, (current, expression) => current.Include(expression)).Where(predicate.Compile());
        }

        public List<InstituicaoCategoria> GetCategorias(long id) => this.db.InstituicaoInstituicaoCategorias.Include(i => i.InstituicaoCategoria).Include(i => i.Instituicao).Where(x => x.Instituicao.ID == id).Select(i => i.InstituicaoCategoria).ToList();
        public void AddCategoria(long id, InstituicaoCategoria instituicaoCategoria) {
            this.db.Attach(instituicaoCategoria);
            this.db.InstituicaoInstituicaoCategorias.Add(new InstituicaoInstituicaoCategoria() {
                Instituicao = this.Get(id),
                InstituicaoCategoria = instituicaoCategoria
            });
            this.db.SaveChanges();
        }
        public void DeleteCategoria(long id, long idCategoria) {
            var instituicaoCategoria = this.db.InstituicaoInstituicaoCategorias.Include(i => i.Instituicao).Include(i => i.InstituicaoCategoria).SingleOrDefault(x => x.Instituicao.ID == id && x.InstituicaoCategoria.ID == idCategoria);
            this.db.InstituicaoInstituicaoCategorias.Remove(instituicaoCategoria);
            this.db.SaveChanges();
        }

        public InstituicaoCurso GetCurso(long id, long idCurso, DateTime? dataInicio) {
            var where = this.db.InstituicaoCursos
            .Include(i => i.Curso)
            .Include(i => i.Instituicao)
            .Include(i => i.CursoGrade)
            .Where(x => x.Instituicao.ID == id && x.Curso.ID == idCurso);
            return where.SingleOrDefault(x => x.DataInicio.Date == dataInicio.Value.Date);
        }

        public List<InstituicaoCursoTurma> GetCursoTurmas(long id, long idCurso, DateTime? dataInicio) {
            var curso = this.GetCurso(id, idCurso, dataInicio);
            return this.db.InstituicaoCursoTurmas
             .Include(i => i.InstituicaoCurso)
             .Where(x => x.InstituicaoCurso.ID == curso.ID).ToList();
        }
        public List<InstituicaoCursoPeriodo> GetCursoPeriodos(long id, long idCurso, DateTime? dataInicio) {
            var curso = this.GetCurso(id, idCurso, dataInicio);
            return this.db.InstituicaoCursoPeriodos
             .Include(i => i.InstituicaoCurso)
             .Where(x => x.InstituicaoCurso.ID == curso.ID).ToList();
        }

        public List<CursoGradeMateria> GetCursoGradeMaterias(long id, long idCurso, DateTime? dataInicio) {
            var curso = this.GetCurso(id, idCurso, dataInicio);
            return this.db.CursoGradeMaterias
             .Include(i => i.CursoGrade)
             .Include(i => i.Materia)
             .Where(x => x.CursoGrade.ID == curso.CursoGrade.ID).ToList();
        }
        public List<InstituicaoCurso> GetCursos(long id) {
            return this.db.InstituicaoCursos.Include(i => i.Curso).Include(i => i.Instituicao).Include(i => i.CursoGrade).Where(x => x.Instituicao.ID == id && !x.DataExpiracao.HasValue && x.Ativo).ToList();
        }
        public void AddCurso(long id, InstituicaoCurso model, List<InstituicaoCursoPeriodo> periodos, List<InstituicaoCursoTurma> turmas) {
            if (this.db.InstituicaoCursos.Include(i => i.Curso).Include(i => i.Instituicao).SingleOrDefault(x => x.Curso.ID == model.Curso.ID && x.Instituicao.ID == id && x.DataExpiracao != null && x.Ativo) != null) {
                throw new Exception("O Curso já existe, as operações permitidas são renovar e desativar.");
            } else {
                model.Instituicao = this.Get(id);
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
        public void RenewCurso(long id, InstituicaoCurso model, List<InstituicaoCursoPeriodo> periodos, List<InstituicaoCursoTurma> turmas) {
            this.db.InstituicaoCursos.SingleOrDefault(x => x.Curso.ID == model.Curso.ID && x.Instituicao.ID == id && x.DataExpiracao == null && x.Ativo).DataExpiracao = DateTime.Now;
            this.AddCurso(id, model, periodos, turmas);
        }
        public void DisableCurso(long id, long idCurso) {
            var instituicaoCurso = this.db.InstituicaoCursos.Include(i => i.Instituicao).Include(i => i.Curso).SingleOrDefault(x => x.Instituicao.ID == id && x.Curso.ID == idCurso && x.DataExpiracao == null && x.Ativo);
            instituicaoCurso.Ativo = false;
            instituicaoCurso.DataExpiracao = DateTime.Now;
            this.db.SaveChanges();
        }

        public InstituicaoCursoOcorrencia AddCursoOcorrencia(long id, long idCurso, InstituicaoCursoOcorrencia instituicaoCursoOcorrencia, List<InstituicaoCursoOcorrenciaAluno> alunos, List<InstituicaoCursoOcorrenciaProfessor> professores) {
            var instituicaoCurso = this.db.InstituicaoCursos.Include(i => i.Instituicao).Include(i => i.Curso).SingleOrDefault(x => x.Instituicao.ID == id && x.Curso.ID == idCurso && x.DataExpiracao == null && x.Ativo);
            instituicaoCursoOcorrencia.InstituicaoCurso = instituicaoCurso;

            alunos.ForEach(x => x.Aluno = this.db.Alunos.Find(x.Aluno.ID));
            alunos.ForEach(x => x.Periodo = this.db.InstituicaoCursoPeriodos.Find(x.Periodo.ID));
            alunos.ForEach(x => x.Turma = this.db.InstituicaoCursoTurmas.Find(x.Turma.ID));
            alunos.ForEach(x => x.InstituicaoCursoOcorrencia = instituicaoCursoOcorrencia);
            this.db.InstituicaoCursoOcorrenciaAlunos.AddRange(alunos);

            professores.ForEach(x => x.Professor = this.db.Professores.Find(x.Professor.ID));
            professores.ForEach(x => x.Periodo = this.db.InstituicaoCursoPeriodos.Find(x.Periodo.ID));
            professores.ForEach(x => x.Turma = this.db.InstituicaoCursoTurmas.Find(x.Turma.ID));
            professores.ForEach(x => x.InstituicaoCursoOcorrencia = instituicaoCursoOcorrencia);
            this.db.InstituicaoCursoOcorrenciaProfessores.AddRange(professores);

            this.db.SaveChanges();

            return instituicaoCursoOcorrencia;
        }

        public List<InstituicaoCursoOcorrencia> GetCursoOcorrencias(long id, long idCurso, DateTime? dataInicio) {
            var where = this.db.InstituicaoCursoOcorrencias
            .Include(i => i.InstituicaoCurso)
            .Where(x => x.InstituicaoCurso.ID == this.GetCurso(id, idCurso, dataInicio).ID);
            if (dataInicio.HasValue) {
                return where.Where(x => x.DataInicio.Value.Date.Equals(dataInicio.Value.Date)).ToList();
            } else {
                return where.ToList();
            }
        }

        public InstituicaoCursoOcorrencia GetCursoOcorrencia(long id, long idCurso, DateTime? dataInicio, DateTime? dataInicioOcorrencia) {
            return this.db.InstituicaoCursoOcorrencias
            .Include(i => i.InstituicaoCurso)
            .SingleOrDefault(x => x.InstituicaoCurso.ID == this.GetCurso(id, idCurso, dataInicio).ID && x.DataInicio.HasValue && x.DataInicio.Value.Date.Equals(dataInicio.Value.Date));
        }
        public List<InstituicaoCursoOcorrenciaProfessor> GetCursoOcorrenciaProfessores(long idCursoOcorrencia) {
            return this.db.InstituicaoCursoOcorrenciaProfessores
            .Include(i => i.InstituicaoCursoOcorrencia)
            .Include(i => i.Periodo)
            .Include(i => i.Turma)
            .Include(i => i.Professor)
            .Include(i => i.Materia)
            .Where(x => x.InstituicaoCursoOcorrencia.ID == idCursoOcorrencia)
            .ToList();
        }
        public List<InstituicaoCursoOcorrenciaAluno> GetCursoOcorrenciaAlunos(long idCursoOcorrencia) {
            return this.db.InstituicaoCursoOcorrenciaAlunos
            .Include(i => i.InstituicaoCursoOcorrencia)
            .Include(i => i.Periodo)
            .Include(i => i.Turma)
            .Include(i => i.Aluno)
            .Where(x => x.InstituicaoCursoOcorrencia.ID == idCursoOcorrencia)
            .ToList();
        }

    }

}