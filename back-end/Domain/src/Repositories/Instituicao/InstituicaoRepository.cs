using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Domain.Repositories {
    public class InstituicaoRepository : IRepository<Instituicao> {
        private BaseContext db;
        private CursoRepository cursoRepository;
        private UsuarioRepository usuarioRepository;

        public InstituicaoRepository(BaseContext db, CursoRepository cursoRepository, UsuarioRepository usuarioRepository) {
            this.db = db;
            this.cursoRepository = cursoRepository;
            this.usuarioRepository = usuarioRepository;
        }

        public Instituicao Add(Instituicao instituicao) {
            this.db.Instituicoes.Add(instituicao);
            return instituicao;
        }

        public void AddInstituicaoCurso(InstituicaoCurso model) {
            model.Instituicao = this.db.Instituicoes.Find(model.Instituicao.ID);
            model.Curso = this.db.Cursos.Find(model.Curso.ID);
            model.CursoGrade = this.db.CursoGrades.Find(model.CursoGrade.ID);
            this.db.InstituicaoCursos.Add(model);
        }

        public void AddInstituicaoCursoPeriodo(InstituicaoCursoPeriodo instituicaoCursoPeriodo) {
            instituicaoCursoPeriodo.InstituicaoCurso = this.db.InstituicaoCursos.Find(instituicaoCursoPeriodo.InstituicaoCurso.ID);
            this.db.InstituicaoCursoPeriodos.Add(instituicaoCursoPeriodo);
        }

        public void AddInstituicaoCursoTurma(InstituicaoCursoTurma instituicaoCursoTurma) {
            instituicaoCursoTurma.InstituicaoCurso = this.db.InstituicaoCursos.Find(instituicaoCursoTurma.InstituicaoCurso.ID);
            this.db.InstituicaoCursoTurmas.Add(instituicaoCursoTurma);
        }

        public void AddInstituicaoCursoOcorrencia(InstituicaoCursoOcorrencia instituicaoCursoOcorrencia) {
            instituicaoCursoOcorrencia.InstituicaoCurso = this.db.InstituicaoCursos.Find(instituicaoCursoOcorrencia.InstituicaoCurso.ID);
            instituicaoCursoOcorrencia.Coordenador = this.db.Professores.Find(instituicaoCursoOcorrencia.Coordenador.ID);
            this.db.InstituicaoCursoOcorrencias.Add(instituicaoCursoOcorrencia);
        }

        public void AddInstituicaoCursoOcorrenciaAluno(InstituicaoCursoOcorrenciaAluno instituicaoCursoOcorrenciaAluno) {
            instituicaoCursoOcorrenciaAluno.Aluno = this.db.Alunos.Find(instituicaoCursoOcorrenciaAluno.Aluno.ID);
            instituicaoCursoOcorrenciaAluno.InstituicaoCursoOcorrencia = this.db.InstituicaoCursoOcorrencias.Find(instituicaoCursoOcorrenciaAluno.InstituicaoCursoOcorrencia.ID);
            this.db.InstituicaoCursoOcorrenciaAlunos.Add(instituicaoCursoOcorrenciaAluno);
        }

        public void AddInstituicaoCursoOcorrenciaPeriodo(InstituicaoCursoOcorrenciaPeriodo instituicaoCursoOcorrenciaPeriodo) {
            instituicaoCursoOcorrenciaPeriodo.InstituicaoCursoOcorrencia = this.db.InstituicaoCursoOcorrencias.Find(instituicaoCursoOcorrenciaPeriodo.InstituicaoCursoOcorrencia.ID);
            this.db.InstituicaoCursoOcorrenciaPeriodos.Add(instituicaoCursoOcorrenciaPeriodo);
        }

        public void AddInstituicaoCursoOcorrenciaPeriodoAluno(InstituicaoCursoOcorrenciaPeriodoAluno instituicaoCursoOcorrenciaPeriodoAluno) {
            var instituicaoCursoOcorrenciaAluno = this.db.InstituicaoCursoOcorrenciaAlunos.FirstOrDefault(x => x.Aluno.ID == instituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoOcorrenciaAluno.Aluno.ID && x.InstituicaoCursoOcorrencia.ID == instituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoOcorrenciaAluno.InstituicaoCursoOcorrencia.ID);
            if (instituicaoCursoOcorrenciaAluno != null) {
                instituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoOcorrenciaAluno = instituicaoCursoOcorrenciaAluno;
            } else {
                this.AddInstituicaoCursoOcorrenciaAluno(instituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoOcorrenciaAluno);
            }

            instituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoOcorrenciaPeriodo = this.db.InstituicaoCursoOcorrenciaPeriodos.Find(instituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoOcorrenciaPeriodo.ID);
            instituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoPeriodo = this.db.InstituicaoCursoPeriodos.Find(instituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoPeriodo.ID);
            instituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoTurma = this.db.InstituicaoCursoTurmas.Find(instituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoTurma.ID);

            this.db.InstituicaoCursoOcorrenciaPeriodoAlunos.Add(instituicaoCursoOcorrenciaPeriodoAluno);
        }

        public void AddInstituicaoCursoOcorrenciaPeriodoProfessor(InstituicaoCursoOcorrenciaPeriodoProfessor instituicaoCursoOcorrenciaPeriodoProfessor) {
            instituicaoCursoOcorrenciaPeriodoProfessor.CursoGradeMateria = this.db.CursoGradeMaterias.Find(instituicaoCursoOcorrenciaPeriodoProfessor.CursoGradeMateria.ID);
            instituicaoCursoOcorrenciaPeriodoProfessor.InstituicaoCursoOcorrenciaPeriodo = this.db.InstituicaoCursoOcorrenciaPeriodos.Find(instituicaoCursoOcorrenciaPeriodoProfessor.InstituicaoCursoOcorrenciaPeriodo.ID);
            instituicaoCursoOcorrenciaPeriodoProfessor.InstituicaoCursoPeriodo = this.db.InstituicaoCursoPeriodos.Find(instituicaoCursoOcorrenciaPeriodoProfessor.InstituicaoCursoPeriodo.ID);
            instituicaoCursoOcorrenciaPeriodoProfessor.InstituicaoCursoTurma = this.db.InstituicaoCursoTurmas.Find(instituicaoCursoOcorrenciaPeriodoProfessor.InstituicaoCursoTurma.ID);
            instituicaoCursoOcorrenciaPeriodoProfessor.Professor = this.db.Professores.Find(instituicaoCursoOcorrenciaPeriodoProfessor.Professor.ID);

            this.db.InstituicaoCursoOcorrenciaPeriodoProfessores.Add(instituicaoCursoOcorrenciaPeriodoProfessor);
        }

        public void AddInstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula(InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula) {
            instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula.InstituicaoCursoOcorrenciaPeriodoProfessor = this.db.InstituicaoCursoOcorrenciaPeriodoProfessores.Find(instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula.InstituicaoCursoOcorrenciaPeriodoProfessor.ID);
            this.db.InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas.Add(instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula);
        }

        public void AddHistoryInstituicao(long id) {
            var history = this.Get(id);
            history.ID = 0;
            history.Ativo = DateTime.Now;
            this.Add(history);
        }

        public void AddHistoryInstituicaoCurso(long id) {
            var history = this.GetInstituicaoCurso(id);
            history.ID = 0;
            history.Ativo = DateTime.Now;
            this.AddInstituicaoCurso(history);
        }

        public void AddHistoryInstituicaoCursoOcorrencia(long id) {
            var history = this.GetInstituicaoCursoOcorrencia(id);
            history.ID = 0;
            history.Ativo = DateTime.Now;
            this.AddInstituicaoCursoOcorrencia(history);
        }

        public Instituicao Update(Instituicao instituicao) {
            //this.AddHistoryInstituicao(instituicao.ID);

            var model = this.db.Instituicoes.Find(instituicao.ID);

            model.Nome = instituicao.Nome;
            model.CNPJ = instituicao.CNPJ;

            this.db.Instituicoes.Update(model);
            return model;
        }

        public void UpdateInstituicaoCurso(InstituicaoCurso instituicaoCurso) {
            //this.AddHistoryInstituicaoCurso(instituicaoCurso.ID);

            var model = this.db.InstituicaoCursos.Find(instituicaoCurso.ID);
            model.Curso = this.db.Cursos.Find(instituicaoCurso.Curso.ID);
            model.CursoGrade = this.db.CursoGrades.Find(instituicaoCurso.CursoGrade.ID);

            this.db.InstituicaoCursos.Update(model);
        }

        public void UpdateInstituicaoCursoOcorrencia(InstituicaoCursoOcorrencia instituicaoCursoOcorrencia) {
            //this.AddHistoryInstituicaoCursoOcorrencia(instituicaoCursoOcorrencia.ID);

            var model = this.db.InstituicaoCursoOcorrencias.Find(instituicaoCursoOcorrencia.ID);
            model.InstituicaoCurso = this.db.InstituicaoCursos.Find(instituicaoCursoOcorrencia.InstituicaoCurso.ID);
            model.DataInicio = instituicaoCursoOcorrencia.DataInicio;
            model.DataExpiracao = instituicaoCursoOcorrencia.DataExpiracao;
            model.Coordenador = this.db.Professores.Find(instituicaoCursoOcorrencia.Coordenador.ID);

            this.db.InstituicaoCursoOcorrencias.Update(model);
        }

        public void Disable(long id) {
            var model = this.db.Instituicoes.Find(id);
            model.Ativo = DateTime.Now;
            this.db.Instituicoes.Update(model);
        }

        public void DisableInstituicaoCurso(long id) {
            var model = this.db.InstituicaoCursos.Find(id);
            model.Ativo = DateTime.Now;
            model.DataExpiracao = DateTime.Now;
            this.db.InstituicaoCursos.Update(model);
        }

        public void DisableInstituicaoCursoPeriodo(long id) {
            var model = this.db.InstituicaoCursoPeriodos.Find(id);
            model.Ativo = DateTime.Now;
            this.db.InstituicaoCursoPeriodos.Update(model);
        }

        public void DisableInstituicaoCursoTurma(long id) {
            var model = this.db.InstituicaoCursoTurmas.Find(id);
            model.Ativo = DateTime.Now;
            this.db.InstituicaoCursoTurmas.Update(model);
        }

        public Instituicao Get(long id) {
            return this.db.Instituicoes
            .AsNoTracking()
            .SingleOrDefault(x => x.ID == id);
        }

        public InstituicaoCurso GetInstituicaoCurso(long id) {
            return this.db.InstituicaoCursos
            .AsNoTracking()
            .Include(i => i.Curso)
            .Include(i => i.Instituicao)
            .Include(i => i.CursoGrade)
            .SingleOrDefault(x => x.ID == id && !x.DataExpiracao.HasValue);
        }

        public InstituicaoCursoOcorrencia GetInstituicaoCursoOcorrencia(long id) {
            return this.db.InstituicaoCursoOcorrencias
            .AsNoTracking()
            .Include(i => i.InstituicaoCurso)
            .SingleOrDefault(x => x.ID == id);
        }

        public List<Instituicao> GetAll(bool ativo) {
            return this.db.Instituicoes.Where(x => x.Ativo.HasValue == !ativo).ToList();
        }

        public List<InstituicaoCurso> GetAllInstituicaoCursoByInstituicao(long id, bool ativo) {
            return this.db.InstituicaoCursos
            .AsNoTracking()
            .Include(i => i.Curso)
            .Include(i => i.Instituicao)
            .Include(i => i.CursoGrade)
            .Where(x => x.Instituicao.ID == id && x.Ativo.HasValue == !ativo)
            .ToList();
        }

        public List<InstituicaoCursoTurma> GetAllInstituicaoCursoTurmaByInstituicaoCurso(long id, bool ativo) {
            return this.db.InstituicaoCursoTurmas
            .AsNoTracking()
             .Include(i => i.InstituicaoCurso)
             .Where(x => x.InstituicaoCurso.ID == id && x.Ativo.HasValue == !ativo)
             .ToList();
        }

        public List<InstituicaoCursoPeriodo> GetAllInstituicaoCursoPeriodoByInstituicaoCurso(long id, bool ativo) {
            return this.db.InstituicaoCursoPeriodos
            .AsNoTracking()
             .Include(i => i.InstituicaoCurso)
             .Where(x => x.InstituicaoCurso.ID == id && x.Ativo.HasValue == !ativo)
             .ToList();
        }

        public List<CursoGradeMateria> GetAllInstituicaoCursoGradeMateriaByInstituicaoCurso(long id, bool ativo) {
            var cursoGrade = this.db.InstituicaoCursos
            .AsNoTracking()
            .Include(x => x.CursoGrade)
            .SingleOrDefault(y => y.ID == id).CursoGrade;

            return this.db.CursoGradeMaterias
             .Include(i => i.CursoGrade)
             .Include(i => i.Materia)
             .Where(x => x.CursoGrade.ID == cursoGrade.ID)
             .ToList();
        }

        public List<InstituicaoCategoria> GetAllInstituicaoCategoria(long id, bool ativo) {
            return this.db.InstituicaoInstituicaoCategorias
            .AsNoTracking()
            .Include(i => i.InstituicaoCategoria)
            .Include(i => i.Instituicao)
            .Where(x => x.Instituicao.ID == id && x.Ativo.HasValue == !ativo)
            .Select(i => i.InstituicaoCategoria)
            .ToList();
        }

        public List<InstituicaoCursoOcorrencia> GetAllInstituicaoCursoOcorrenciaByInstituicaoCurso(long id, bool ativo) {
            return this.db.InstituicaoCursoOcorrencias
            .AsNoTracking()
            .Include(i => i.InstituicaoCurso)
            .Where(x => x.InstituicaoCurso.ID == id && x.Ativo.HasValue == !ativo)
            .ToList();
        }

        public List<InstituicaoCursoOcorrenciaAluno> GetAllInstituicaoCursoOcorrenciaAlunoByInstituicaoCursoOcorrencia(long id, bool ativo) {
            return this.db.InstituicaoCursoOcorrenciaAlunos
            .AsNoTracking()
            .Include(i => i.InstituicaoCursoOcorrencia)
            .Include(i => i.Aluno)
            .ThenInclude(i => i.UsuarioInfo)
            .Where(x => x.InstituicaoCursoOcorrencia.ID == id && x.Ativo.HasValue == !ativo)
            .ToList();
        }

        public List<InstituicaoCursoOcorrenciaPeriodo> GetAllInstituicaoCursoOcorrenciaPeriodoByInstituicaoCursoOcorrencia(long id, bool ativo) {
            return this.db.InstituicaoCursoOcorrenciaPeriodos
            .AsNoTracking()
            .Include(i => i.InstituicaoCursoOcorrencia)
            .Where(x => x.InstituicaoCursoOcorrencia.ID == id && x.Ativo.HasValue == !ativo)
            .ToList();
        }

        public List<InstituicaoCursoOcorrenciaPeriodoAluno> GetAllInstituicaoCursoOcorrenciaPeriodoAlunoByInstituicaoCursoOcorrenciaPeriodo(long id, bool ativo) {
            return this.db.InstituicaoCursoOcorrenciaPeriodoAlunos
            .AsNoTracking()
            .Include(i => i.InstituicaoCursoOcorrenciaAluno)
            .ThenInclude(i => i.Aluno)
            .ThenInclude(i => i.UsuarioInfo)
            .Include(i => i.InstituicaoCursoOcorrenciaPeriodo)
            .Include(i => i.InstituicaoCursoPeriodo)
            .Include(i => i.InstituicaoCursoTurma)
            .Where(x => x.InstituicaoCursoOcorrenciaPeriodo.ID == id && x.Ativo.HasValue == !ativo)
            .ToList();
        }

        public List<InstituicaoCursoOcorrenciaPeriodoProfessor> GetAllInstituicaoCursoOcorrenciaPeriodoProfessorByInstituicaoCursoOcorrenciaPeriodo(long id, bool ativo) {
            return this.db.InstituicaoCursoOcorrenciaPeriodoProfessores
            .AsNoTracking()
            .Include(i => i.Professor)
            .ThenInclude(i => i.UsuarioInfo)
            .Include(i => i.InstituicaoCursoOcorrenciaPeriodo)
            .Include(i => i.InstituicaoCursoPeriodo)
            .Include(i => i.InstituicaoCursoTurma)
            .Include(i => i.CursoGradeMateria)
            .ThenInclude(i => i.Materia)
            .Where(x => x.InstituicaoCursoOcorrenciaPeriodo.ID == id && x.Ativo.HasValue == !ativo)
            .ToList();
        }

        public List<InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula> GetAllInstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaByInstituicaoCursoOcorrenciaPeriodoProfessor(long id, bool ativo) {
            return this.db.InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas
            .AsNoTracking()
            .Include(i => i.InstituicaoCursoOcorrenciaPeriodoProfessor)
            .Where(x => x.InstituicaoCursoOcorrenciaPeriodoProfessor.ID == id && x.Ativo.HasValue == !ativo)
            .ToList();
        }

        public IEnumerable<Instituicao> Query(Expression<Func<Instituicao, bool>> predicate, params Expression<Func<Instituicao, object>>[] includeExpressions) {
            return includeExpressions.Aggregate<Expression<Func<Instituicao, object>>, IQueryable<Instituicao>>(db.Instituicoes, (current, expression) => current.Include(expression)).Where(predicate.Compile());
        }

        public IDbContextTransaction BeginTransaction() {
            return this.db.Database.BeginTransaction();
        }

        public void SaveChanges() {
            this.db.SaveChanges();
        }

    }

}