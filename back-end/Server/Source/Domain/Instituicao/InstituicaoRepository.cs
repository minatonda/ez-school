using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain.Common;
using Domain.CursoDomain;
using Domain.UsuarioDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Domain.InstituicaoDomain {
    public class InstituicaoRepository : IRepository<Instituicao> {

        private BaseContext db;
        private CursoRepository cursoRepository;
        private UsuarioRepository usuarioRepository;

        public InstituicaoRepository(BaseContext db, CursoRepository cursoRepository, UsuarioRepository usuarioRepository) {
            this.db = db;
            this.cursoRepository = cursoRepository;
            this.usuarioRepository = usuarioRepository;
        }

        public void Add(Instituicao instituicao) {
            this.db.Instituicoes.Add(instituicao);
        }

        public void AddInstituicaoCurso(InstituicaoCurso model) {
            model.Instituicao = this.db.Instituicoes.Find(model.Instituicao.ID);
            model.Curso = this.db.Cursos.Find(model.Curso.ID);
            model.CursoGrade = this.db.CursoGrades.Find(model.CursoGrade.ID);
            this.db.InstituicoesCursos.Add(model);
        }

        public void AddInstituicaoCursoPeriodo(InstituicaoCursoPeriodo instituicaoCursoPeriodo) {
            instituicaoCursoPeriodo.InstituicaoCurso = this.db.InstituicoesCursos.Find(instituicaoCursoPeriodo.InstituicaoCurso.ID);
            this.db.InstituicoesCursosPeriodos.Add(instituicaoCursoPeriodo);
        }

        public void AddInstituicaoCursoTurma(InstituicaoCursoTurma instituicaoCursoTurma) {
            instituicaoCursoTurma.InstituicaoCurso = this.db.InstituicoesCursos.Find(instituicaoCursoTurma.InstituicaoCurso.ID);
            this.db.InstituicoesCursosTurmas.Add(instituicaoCursoTurma);
        }

        public void AddInstituicaoCursoOcorrencia(InstituicaoCursoOcorrencia instituicaoCursoOcorrencia) {
            instituicaoCursoOcorrencia.InstituicaoCurso = this.db.InstituicoesCursos.Find(instituicaoCursoOcorrencia.InstituicaoCurso.ID);
            instituicaoCursoOcorrencia.Coordenador = this.db.UsuarioInfos.Find(instituicaoCursoOcorrencia.Coordenador.ID);
            this.db.InstituicoesCursosOcorrencias.Add(instituicaoCursoOcorrencia);
        }

        public void AddInstituicaoCursoOcorrenciaAluno(InstituicaoCursoOcorrenciaAluno instituicaoCursoOcorrenciaAluno) {
            instituicaoCursoOcorrenciaAluno.Aluno = this.db.UsuarioInfos.Find(instituicaoCursoOcorrenciaAluno.Aluno.ID);
            instituicaoCursoOcorrenciaAluno.InstituicaoCursoOcorrencia = this.db.InstituicoesCursosOcorrencias.Find(instituicaoCursoOcorrenciaAluno.InstituicaoCursoOcorrencia.ID);
            this.db.InstituicoesCursosOcorrenciasAlunos.Add(instituicaoCursoOcorrenciaAluno);
        }

        public void AddInstituicaoCursoOcorrenciaPeriodo(InstituicaoCursoOcorrenciaPeriodo instituicaoCursoOcorrenciaPeriodo) {
            instituicaoCursoOcorrenciaPeriodo.InstituicaoCursoOcorrencia = this.db.InstituicoesCursosOcorrencias.Find(instituicaoCursoOcorrenciaPeriodo.InstituicaoCursoOcorrencia.ID);
            this.db.InstituicoesCursosOcorrenciasPeriodos.Add(instituicaoCursoOcorrenciaPeriodo);
        }

        public void AddInstituicaoCursoOcorrenciaPeriodoAluno(InstituicaoCursoOcorrenciaPeriodoAluno instituicaoCursoOcorrenciaPeriodoAluno) {
            var instituicaoCursoOcorrenciaAluno = this.GetInstituicaoCursoOcorrenciaAlunoByAlunoAndInstituicaoCursoOcorrencia(instituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoOcorrenciaAluno.Aluno.ID, instituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoOcorrenciaAluno.InstituicaoCursoOcorrencia.ID);
            if (instituicaoCursoOcorrenciaAluno != null) {
                instituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoOcorrenciaAluno = instituicaoCursoOcorrenciaAluno;
            } else {
                instituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoOcorrenciaAluno = instituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoOcorrenciaAluno;
                instituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoOcorrenciaAluno.ID = 0;
                this.AddInstituicaoCursoOcorrenciaAluno(instituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoOcorrenciaAluno);
            }

            instituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoOcorrenciaPeriodo = this.db.InstituicoesCursosOcorrenciasPeriodos.Find(instituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoOcorrenciaPeriodo.ID);
            instituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoPeriodo = this.db.InstituicoesCursosPeriodos.Find(instituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoPeriodo.ID);
            instituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoTurma = this.db.InstituicoesCursosTurmas.Find(instituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoTurma.ID);

            this.db.InstituicoesCursosOcorrenciasPeriodosAlunos.Add(instituicaoCursoOcorrenciaPeriodoAluno);
        }

        public void AddInstituicaoCursoOcorrenciaPeriodoProfessor(InstituicaoCursoOcorrenciaPeriodoProfessor instituicaoCursoOcorrenciaPeriodoProfessor) {
            instituicaoCursoOcorrenciaPeriodoProfessor.CursoGradeMateria = this.db.CursoGradeMaterias.Find(instituicaoCursoOcorrenciaPeriodoProfessor.CursoGradeMateria.ID);
            instituicaoCursoOcorrenciaPeriodoProfessor.InstituicaoCursoOcorrenciaPeriodo = this.db.InstituicoesCursosOcorrenciasPeriodos.Find(instituicaoCursoOcorrenciaPeriodoProfessor.InstituicaoCursoOcorrenciaPeriodo.ID);
            instituicaoCursoOcorrenciaPeriodoProfessor.InstituicaoCursoPeriodo = this.db.InstituicoesCursosPeriodos.Find(instituicaoCursoOcorrenciaPeriodoProfessor.InstituicaoCursoPeriodo.ID);
            instituicaoCursoOcorrenciaPeriodoProfessor.InstituicaoCursoTurma = this.db.InstituicoesCursosTurmas.Find(instituicaoCursoOcorrenciaPeriodoProfessor.InstituicaoCursoTurma.ID);
            instituicaoCursoOcorrenciaPeriodoProfessor.Professor = this.db.UsuarioInfos.Find(instituicaoCursoOcorrenciaPeriodoProfessor.Professor.ID);

            this.db.InstituicoesCursosOcorrenciasPeriodosProfessores.Add(instituicaoCursoOcorrenciaPeriodoProfessor);
        }

        public void AddInstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula(InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula) {
            instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula.InstituicaoCursoOcorrenciaPeriodoProfessor = this.db.InstituicoesCursosOcorrenciasPeriodosProfessores.Find(instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula.InstituicaoCursoOcorrenciaPeriodoProfessor.ID);
            this.db.InstituicoesCursosOcorrenciasPeriodosProfessoresPeriodoAulas.Add(instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula);
        }

        public void AddInstituicaoCursoOcorrenciaNota(InstituicaoCursoOcorrenciaNota instituicaoCursoOcorrenciaNota) {
            instituicaoCursoOcorrenciaNota.InstituicaoCursoOcorrenciaPeriodoAluno = this.db.InstituicoesCursosOcorrenciasPeriodosAlunos.Find(instituicaoCursoOcorrenciaNota.InstituicaoCursoOcorrenciaPeriodoAluno.ID);
            instituicaoCursoOcorrenciaNota.InstituicaoCursoOcorrenciaPeriodoProfessor = this.db.InstituicoesCursosOcorrenciasPeriodosProfessores.Find(instituicaoCursoOcorrenciaNota.InstituicaoCursoOcorrenciaPeriodoProfessor.ID);
            this.db.InstituicoesCursosOcorrenciasNotas.Add(instituicaoCursoOcorrenciaNota);
        }

        public void AddInstituicaoCursoOcorrenciaAusencia(InstituicaoCursoOcorrenciaAusencia instituicaoCursoOcorrenciaAusencia) {
            instituicaoCursoOcorrenciaAusencia.InstituicaoCursoOcorrenciaPeriodoAluno = this.db.InstituicoesCursosOcorrenciasPeriodosAlunos.Find(instituicaoCursoOcorrenciaAusencia.InstituicaoCursoOcorrenciaPeriodoAluno.ID);
            instituicaoCursoOcorrenciaAusencia.InstituicaoCursoOcorrenciaPeriodoProfessor = this.db.InstituicoesCursosOcorrenciasPeriodosProfessores.Find(instituicaoCursoOcorrenciaAusencia.InstituicaoCursoOcorrenciaPeriodoProfessor.ID);
            this.db.InstituicoesCursosOcorrenciasAusencias.Add(instituicaoCursoOcorrenciaAusencia);
        }

        public void AddInstituicaoColaborador(InstituicaoColaborador instituicaoColaborador) {
            instituicaoColaborador.Instituicao = this.db.Instituicoes.Find(instituicaoColaborador.Instituicao.ID);
            instituicaoColaborador.Usuario = this.db.UsuarioInfos.Find(instituicaoColaborador.Usuario.ID);
            this.db.InstituicoesColaboradores.Add(instituicaoColaborador);
        }

        public void AddInstituicaoColaboradorPerfil(InstituicaoColaboradorPerfil instituicaoColaboradorPerfil) {
            instituicaoColaboradorPerfil.Instituicao = this.db.Instituicoes.Find(instituicaoColaboradorPerfil.Instituicao.ID);
            this.db.InstituicoesColaboradoresPerfis.Add(instituicaoColaboradorPerfil);
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

        public void Update(Instituicao instituicao) {
            //this.AddHistoryInstituicao(instituicao.ID);

            var model = this.db.Instituicoes.Find(instituicao.ID);

            model.Nome = instituicao.Nome;
            model.CNPJ = instituicao.CNPJ;

            this.db.Instituicoes.Update(model);
        }

        public void UpdateInstituicaoCurso(InstituicaoCurso instituicaoCurso) {
            //this.AddHistoryInstituicaoCurso(instituicaoCurso.ID);

            var model = this.db.InstituicoesCursos.Find(instituicaoCurso.ID);
            model.Curso = this.db.Cursos.Find(instituicaoCurso.Curso.ID);
            model.CursoGrade = this.db.CursoGrades.Find(instituicaoCurso.CursoGrade.ID);

            this.db.InstituicoesCursos.Update(model);
        }

        public void UpdateInstituicaoCursoOcorrencia(InstituicaoCursoOcorrencia instituicaoCursoOcorrencia) {
            //this.AddHistoryInstituicaoCursoOcorrencia(instituicaoCursoOcorrencia.ID);

            var model = this.db.InstituicoesCursosOcorrencias.Find(instituicaoCursoOcorrencia.ID);
            model.InstituicaoCurso = this.db.InstituicoesCursos.Find(instituicaoCursoOcorrencia.InstituicaoCurso.ID);
            model.DataInicio = instituicaoCursoOcorrencia.DataInicio;
            model.DataExpiracao = instituicaoCursoOcorrencia.DataExpiracao;
            model.Coordenador = this.db.UsuarioInfos.Find(instituicaoCursoOcorrencia.Coordenador.ID);

            this.db.InstituicoesCursosOcorrencias.Update(model);
        }

        public void UpdateInstituicaoCursoOcorrenciaPeriodo(InstituicaoCursoOcorrenciaPeriodo instituicaoCursoOcorrenciaPeriodo) {
            //this.AddHistoryInstituicaoCursoOcorrencia(instituicaoCursoOcorrencia.ID);

            var model = this.db.InstituicoesCursosOcorrenciasPeriodos.Find(instituicaoCursoOcorrenciaPeriodo.ID);
            model.InstituicaoCursoOcorrencia = this.db.InstituicoesCursosOcorrencias.Find(instituicaoCursoOcorrenciaPeriodo.InstituicaoCursoOcorrencia.ID);
            model.DataInicio = instituicaoCursoOcorrenciaPeriodo.DataInicio;
            model.DataExpiracao = instituicaoCursoOcorrenciaPeriodo.DataExpiracao;

            this.db.InstituicoesCursosOcorrenciasPeriodos.Update(model);
        }

        public void UpdateInstituicaoCursoOcorrenciaPeriodoAluno(InstituicaoCursoOcorrenciaPeriodoAluno instituicaoCursoOcorrenciaPeriodoAluno) {
            //this.AddHistoryInstituicaoCursoOcorrencia(instituicaoCursoOcorrencia.ID);

            var model = this.db.InstituicoesCursosOcorrenciasPeriodosAlunos
            .Include(i => i.InstituicaoCursoOcorrenciaAluno)
            .ThenInclude(i => i.Aluno)
            .Include(i => i.InstituicaoCursoOcorrenciaPeriodo)
            .SingleOrDefault(x => x.ID == instituicaoCursoOcorrenciaPeriodoAluno.ID);

            var instituicaoCursoOcorrenciaAluno = this.GetInstituicaoCursoOcorrenciaAlunoByAlunoAndInstituicaoCursoOcorrencia(instituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoOcorrenciaAluno.Aluno.ID, instituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoOcorrenciaAluno.InstituicaoCursoOcorrencia.ID);
            if (instituicaoCursoOcorrenciaAluno != null) {
                model.InstituicaoCursoOcorrenciaAluno = instituicaoCursoOcorrenciaAluno;
            } else {
                model.InstituicaoCursoOcorrenciaAluno = instituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoOcorrenciaAluno;
                model.InstituicaoCursoOcorrenciaAluno.ID = 0;
                this.AddInstituicaoCursoOcorrenciaAluno(model.InstituicaoCursoOcorrenciaAluno);
            }

            model.InstituicaoCursoPeriodo = this.db.InstituicoesCursosPeriodos.Find(instituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoPeriodo.ID);
            model.InstituicaoCursoTurma = this.db.InstituicoesCursosTurmas.Find(instituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoTurma.ID);
            this.db.InstituicoesCursosOcorrenciasPeriodosAlunos.Update(model);
        }

        public InstituicaoCursoOcorrenciaAluno GetInstituicaoCursoOcorrenciaAlunoByAlunoAndInstituicaoCursoOcorrencia(string idAluno, long idInstituicaoCursoOcorrencia) {
            return this.db.InstituicoesCursosOcorrenciasAlunos.SingleOrDefault(x => x.Aluno.ID == idAluno && x.InstituicaoCursoOcorrencia.ID == idInstituicaoCursoOcorrencia);
        }

        public void UpdateInstituicaoCursoOcorrenciaPeriodoProfessor(InstituicaoCursoOcorrenciaPeriodoProfessor instituicaoCursoOcorrenciaPeriodoProfessor) {
            //this.AddHistoryInstituicaoCursoOcorrencia(instituicaoCursoOcorrencia.ID);

            var model = this.db.InstituicoesCursosOcorrenciasPeriodosProfessores
                        .Include(i => i.Professor)
                        .Include(i => i.InstituicaoCursoOcorrenciaPeriodo)
                        .Include(i => i.InstituicaoCursoTurma)
                        .SingleOrDefault(x => x.ID == instituicaoCursoOcorrenciaPeriodoProfessor.ID);

            model.Professor = this.db.UsuarioInfos.Find(instituicaoCursoOcorrenciaPeriodoProfessor.Professor.ID);
            model.InstituicaoCursoPeriodo = this.db.InstituicoesCursosPeriodos.Find(instituicaoCursoOcorrenciaPeriodoProfessor.InstituicaoCursoPeriodo.ID);
            model.InstituicaoCursoTurma = this.db.InstituicoesCursosTurmas.Find(instituicaoCursoOcorrenciaPeriodoProfessor.InstituicaoCursoTurma.ID);
            model.FormulaNotaFinal = instituicaoCursoOcorrenciaPeriodoProfessor.FormulaNotaFinal;
            this.db.InstituicoesCursosOcorrenciasPeriodosProfessores.Update(model);
        }

        public void UpdateInstituicaoCursoOcorrenciaNota(InstituicaoCursoOcorrenciaNota instituicaoCursoOcorrenciaNota) {
            var model = this.db.InstituicoesCursosOcorrenciasNotas.Include(i => i.InstituicaoCursoOcorrenciaPeriodoAluno).Include(i => i.InstituicaoCursoOcorrenciaPeriodoProfessor).SingleOrDefault(x => x.ID == instituicaoCursoOcorrenciaNota.ID);
            model.InstituicaoCursoOcorrenciaPeriodoAluno = this.db.InstituicoesCursosOcorrenciasPeriodosAlunos.Find(instituicaoCursoOcorrenciaNota.InstituicaoCursoOcorrenciaPeriodoAluno.ID);
            model.InstituicaoCursoOcorrenciaPeriodoProfessor = this.db.InstituicoesCursosOcorrenciasPeriodosProfessores.Find(instituicaoCursoOcorrenciaNota.InstituicaoCursoOcorrenciaPeriodoProfessor.ID);
            model.Valor = instituicaoCursoOcorrenciaNota.Valor;
            model.DataLancamento = instituicaoCursoOcorrenciaNota.DataLancamento;
            this.db.InstituicoesCursosOcorrenciasNotas.Update(model);
        }

        public void UpdateInstituicaoColaborador(InstituicaoColaborador instituicaoColaborador) {
            var model = this.db.InstituicoesColaboradores.Include(x => x.Instituicao).SingleOrDefault(x => x.ID == instituicaoColaborador.ID);
            model.Perfis = instituicaoColaborador.Perfis;
            model.Instituicao = this.db.Instituicoes.Find(instituicaoColaborador.Instituicao.ID);
            model.Usuario = this.db.UsuarioInfos.Find(instituicaoColaborador.Usuario.ID);
            this.db.InstituicoesColaboradores.Update(model);
        }

        public void UpdateInstituicaoColaboradorPerfil(InstituicaoColaboradorPerfil instituicaoColaboradorPerfil) {
            var model = this.db.InstituicoesColaboradoresPerfis.Include(i => i.Instituicao).SingleOrDefault(x => x.ID == instituicaoColaboradorPerfil.ID);
            model.Instituicao = this.db.Instituicoes.Find(instituicaoColaboradorPerfil.Instituicao.ID);
            model.Nome = instituicaoColaboradorPerfil.Nome;
            model.Roles = instituicaoColaboradorPerfil.Roles;
            this.db.InstituicoesColaboradoresPerfis.Update(model);
        }

        public void Disable(long id) {
            var model = this.db.Instituicoes.Find(id);
            model.Ativo = DateTime.Now;
            this.db.Instituicoes.Update(model);
        }

        public void DisableInstituicaoColaborador(long id) {
            var model = this.db.InstituicoesColaboradores.Find(id);
            model.Ativo = DateTime.Now;
            this.db.InstituicoesColaboradores.Update(model);
        }

        public void DisableInstituicaoColaboradorPerfil(long id) {
            var model = this.db.InstituicoesColaboradores.Find(id);
            model.Ativo = DateTime.Now;
            this.db.InstituicoesColaboradores.Update(model);
        }

        public void DisableInstituicaoCurso(long id) {
            var model = this.db.InstituicoesCursos.Find(id);
            model.Ativo = DateTime.Now;
            model.DataExpiracao = DateTime.Now;
            this.db.InstituicoesCursos.Update(model);
        }

        public void DisableInstituicaoCursoPeriodo(long id) {
            var model = this.db.InstituicoesCursosPeriodos.Find(id);
            model.Ativo = DateTime.Now;
            this.db.InstituicoesCursosPeriodos.Update(model);
        }

        public void DisableInstituicaoCursoTurma(long id) {
            var model = this.db.InstituicoesCursosTurmas.Find(id);
            model.Ativo = DateTime.Now;
            this.db.InstituicoesCursosTurmas.Update(model);
        }

        public void DisableInstituicaoCursoOcorrencia(long id) {
            var model = this.db.InstituicoesCursosOcorrencias.Find(id);
            model.Ativo = DateTime.Now;
            this.db.InstituicoesCursosOcorrencias.Update(model);
        }

        public void DisableInstituicaoCursoOcorrenciaAusencia(long id) {
            var model = this.db.InstituicoesCursosOcorrenciasAusencias.Find(id);
            model.Ativo = DateTime.Now;
            this.db.InstituicoesCursosOcorrenciasAusencias.Update(model);
        }

        public void DisableInstituicaoCursoOcorrenciaNota(long id) {
            var model = this.db.InstituicoesCursosOcorrenciasNotas.Find(id);
            model.Ativo = DateTime.Now;
            this.db.InstituicoesCursosOcorrenciasNotas.Update(model);
        }

        public void DisableInstituicaoCursoOcorrenciaPeriodo(long id) {
            var model = this.db.InstituicoesCursosOcorrenciasPeriodos.Find(id);
            model.Ativo = DateTime.Now;
            this.db.InstituicoesCursosOcorrenciasPeriodos.Update(model);
        }

        public void DisableInstituicaoCursoOcorrenciaPeriodoAluno(long id) {
            var model = this.db.InstituicoesCursosOcorrenciasPeriodosAlunos.Find(id);
            model.Ativo = DateTime.Now;
            this.db.InstituicoesCursosOcorrenciasPeriodosAlunos.Update(model);
        }

        public void DisableInstituicaoCursoOcorrenciaPeriodoProfessor(long id) {
            var model = this.db.InstituicoesCursosOcorrenciasPeriodosProfessores.Find(id);
            model.Ativo = DateTime.Now;
            this.db.InstituicoesCursosOcorrenciasPeriodosProfessores.Update(model);
        }

        public void DisableInstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula(long id) {
            var model = this.db.InstituicoesCursosOcorrenciasPeriodosProfessoresPeriodoAulas.Find(id);
            model.Ativo = DateTime.Now;
            this.db.InstituicoesCursosOcorrenciasPeriodosProfessoresPeriodoAulas.Update(model);
        }

        public Instituicao Get(long id) {
            return this.db.Instituicoes
            .AsNoTracking()
            .SingleOrDefault(x => x.ID == id);
        }

        public Instituicao GetByCNPJ(string CNPJ) {
            return this.db.Instituicoes
            .AsNoTracking()
            .FirstOrDefault(x => x.CNPJ.ToLower() == CNPJ.ToLower() && !x.Ativo.HasValue);
        }

        public InstituicaoCurso GetInstituicaoCurso(long id) {
            return this.db.InstituicoesCursos
            .AsNoTracking()
            .Include(i => i.Curso)
            .Include(i => i.Instituicao)
            .Include(i => i.CursoGrade)
            .SingleOrDefault(x => x.ID == id);
        }

        public InstituicaoCursoOcorrencia GetInstituicaoCursoOcorrencia(long id) {
            return this.db.InstituicoesCursosOcorrencias
            .AsNoTracking()
            .Include(i => i.InstituicaoCurso)
            .Include(i => i.Coordenador)
            .SingleOrDefault(x => x.ID == id);
        }

        public InstituicaoCursoOcorrenciaPeriodo GetInstituicaoCursoOcorrenciaPeriodo(long id) {
            return this.db.InstituicoesCursosOcorrenciasPeriodos
            .AsNoTracking()
            .Include(i => i.InstituicaoCursoOcorrencia)
            .SingleOrDefault(x => x.ID == id);
        }

        public long GetInstituicaoCursoOcorrenciaPeriodoSequence(long id) {
            var instituicaoCursoOcorrencia = this.db.InstituicoesCursosOcorrenciasPeriodos.Include(i => i.InstituicaoCursoOcorrencia).SingleOrDefault(y => y.ID == id).InstituicaoCursoOcorrencia;
            var ocorrencias = this.db.InstituicoesCursosOcorrenciasPeriodos
            .Where(x => x.InstituicaoCursoOcorrencia.ID == instituicaoCursoOcorrencia.ID)
            .OrderBy(x => x.DataInicio)
            .ToList();

            return ocorrencias.IndexOf(ocorrencias.SingleOrDefault(x => x.ID == id)) + 1;
        }

        public InstituicaoCursoOcorrenciaPeriodoAluno GetInstituicaoCursoOcorrenciaPeriodoAlunoByAlunoIdAndInstituicaoCursoOcorrenciaPeriodoId(string alunoId, long instituicaoCursoOcorrenciaPeriodoId) {
            return this.db.InstituicoesCursosOcorrenciasPeriodosAlunos
            .AsNoTracking()
            .Include(i => i.InstituicaoCursoOcorrenciaAluno)
            .ThenInclude(i => i.Aluno)
            .Include(i => i.InstituicaoCursoOcorrenciaPeriodo)
            .SingleOrDefault(x => x.InstituicaoCursoOcorrenciaAluno.Aluno.ID == alunoId && x.InstituicaoCursoOcorrenciaPeriodo.ID == instituicaoCursoOcorrenciaPeriodoId);
        }

        public InstituicaoCursoOcorrenciaPeriodoProfessor GetInstituicaoCursoOcorrenciaPeriodoProfessor(long id) {
            return this.db.InstituicoesCursosOcorrenciasPeriodosProfessores
            .AsNoTracking()
            .Include(i => i.InstituicaoCursoOcorrenciaPeriodo)
            .Include(i => i.InstituicaoCursoTurma)
            .Include(i => i.InstituicaoCursoPeriodo)
            .Include(i => i.Professor)
            .SingleOrDefault(x => x.ID == id);
        }

        public InstituicaoColaborador GetInstituicaoColaborador(long id) {
            return this.db.InstituicoesColaboradores
            .AsNoTracking()
            .Include(i => i.Instituicao)
            .Include(i => i.Usuario)
            .SingleOrDefault(x => x.ID == id);
        }

        public InstituicaoColaboradorPerfil GetInstituicaoColaboradorPerfil(long id) {
            return this.db.InstituicoesColaboradoresPerfis
            .AsNoTracking()
            .Include(i => i.Instituicao)
            .SingleOrDefault(x => x.Instituicao.ID == id);
        }

        public List<Instituicao> GetAll(bool ativo) {
            return this.db.Instituicoes.Where(x => x.Ativo.HasValue == !ativo).ToList();
        }

        public List<InstituicaoCurso> GetAllInstituicaoCursoByInstituicao(long id, bool ativo) {
            return this.db.InstituicoesCursos
            .AsNoTracking()
            .Include(i => i.Curso)
            .Include(i => i.Instituicao)
            .Include(i => i.CursoGrade)
            .Where(x => x.Instituicao.ID == id && x.Ativo.HasValue == !ativo)
            .ToList();
        }

        public List<InstituicaoCursoTurma> GetAllInstituicaoCursoTurmaByInstituicaoCurso(long id, bool ativo) {
            return this.db.InstituicoesCursosTurmas
            .AsNoTracking()
             .Include(i => i.InstituicaoCurso)
             .Where(x => x.InstituicaoCurso.ID == id && x.Ativo.HasValue == !ativo)
             .ToList();
        }

        public List<InstituicaoCursoPeriodo> GetAllInstituicaoCursoPeriodoByInstituicaoCurso(long id, bool ativo) {
            return this.db.InstituicoesCursosPeriodos
            .AsNoTracking()
             .Include(i => i.InstituicaoCurso)
             .Where(x => x.InstituicaoCurso.ID == id && x.Ativo.HasValue == !ativo)
             .ToList();
        }

        public List<CursoGradeMateria> GetAllInstituicaoCursoGradeMateriaByInstituicaoCurso(long id, bool ativo) {
            var cursoGrade = this.db.InstituicoesCursos
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
            return this.db.InstituicoesInstituicoesCategorias
            .AsNoTracking()
            .Include(i => i.InstituicaoCategoria)
            .Include(i => i.Instituicao)
            .Where(x => x.Instituicao.ID == id && x.Ativo.HasValue == !ativo)
            .Select(i => i.InstituicaoCategoria)
            .ToList();
        }

        public List<InstituicaoCursoOcorrencia> GetAllInstituicaoCursoOcorrenciaByInstituicaoCurso(long id, bool ativo) {
            return this.db.InstituicoesCursosOcorrencias
            .AsNoTracking()
            .Include(i => i.InstituicaoCurso)
            .Where(x => x.InstituicaoCurso.ID == id && x.Ativo.HasValue == !ativo)
            .ToList();
        }

        public List<InstituicaoCursoOcorrenciaNota> GetAllInstituicaoCursoOcorrenciaNotaByInstituicaoCursoOcorrenciaProfessorPeriodo(long idInstituicaoCursoOcorrenciaProfessorPeriodo) {
            return this.db.InstituicoesCursosOcorrenciasNotas
            .AsNoTracking()
            .Include(i => i.InstituicaoCursoOcorrenciaPeriodoAluno)
            .Include(i => i.InstituicaoCursoOcorrenciaPeriodoProfessor)
            .Where(x => x.InstituicaoCursoOcorrenciaPeriodoProfessor.ID == idInstituicaoCursoOcorrenciaProfessorPeriodo && !x.Ativo.HasValue)
            .ToList();
        }

        public List<InstituicaoCursoOcorrenciaAusencia> GetAllInstituicaoCursoOcorrenciaAusenciaByInstituicaoCursoOcorrenciaProfessorPeriodo(long idInstituicaoCursoOcorrenciaProfessorPeriodo) {
            return this.db.InstituicoesCursosOcorrenciasAusencias
            .AsNoTracking()
            .Include(i => i.InstituicaoCursoOcorrenciaPeriodoAluno)
            .Include(i => i.InstituicaoCursoOcorrenciaPeriodoProfessor)
            .Where(x => x.InstituicaoCursoOcorrenciaPeriodoProfessor.ID == idInstituicaoCursoOcorrenciaProfessorPeriodo && !x.Ativo.HasValue)
            .ToList();
        }

        public List<InstituicaoCursoOcorrenciaAusencia> GetAllInstituicaoCursoOcorrenciaAusenciaByInstituicaoCursoOcorrenciaProfessorPeriodoAndDataAusencia(long idInstituicaoCursoOcorrenciaProfessorPeriodo, DateTime DataAusencia) {
            return this.db.InstituicoesCursosOcorrenciasAusencias
            .AsNoTracking()
            .Include(i => i.InstituicaoCursoOcorrenciaPeriodoAluno)
            .Include(i => i.InstituicaoCursoOcorrenciaPeriodoProfessor)
            .Where(x => x.InstituicaoCursoOcorrenciaPeriodoProfessor.ID == idInstituicaoCursoOcorrenciaProfessorPeriodo && x.DataAusencia.Date == DataAusencia.Date && !x.Ativo.HasValue)
            .ToList();
        }

        public List<InstituicaoCursoOcorrenciaAluno> GetAllInstituicaoCursoOcorrenciaAlunoByInstituicaoCursoOcorrencia(long id, bool ativo) {
            return this.db.InstituicoesCursosOcorrenciasAlunos
            .AsNoTracking()
            .Include(i => i.InstituicaoCursoOcorrencia)
            .Include(i => i.Aluno)
            .Where(x => x.InstituicaoCursoOcorrencia.ID == id && x.Ativo.HasValue == !ativo)
            .ToList();
        }

        public List<InstituicaoCursoOcorrenciaPeriodo> GetAllInstituicaoCursoOcorrenciaPeriodoByInstituicaoCursoOcorrencia(long id, bool ativo) {
            return this.db.InstituicoesCursosOcorrenciasPeriodos
            .AsNoTracking()
            .Include(i => i.InstituicaoCursoOcorrencia)
            .Where(x => x.InstituicaoCursoOcorrencia.ID == id && x.Ativo.HasValue == !ativo)
            .ToList();
        }

        public List<InstituicaoCursoOcorrenciaPeriodoProfessor> GetAllInstituicaoCursoOcorrenciaPeriodoProfessorByProfessor(string id, bool ativo) {
            return this.db.InstituicoesCursosOcorrenciasPeriodosProfessores
            .AsNoTracking()
            .Include(i => i.CursoGradeMateria)
            .Include(i => i.InstituicaoCursoOcorrenciaPeriodo)
            .ThenInclude(i => i.InstituicaoCursoOcorrencia)
            .ThenInclude(i => i.InstituicaoCurso)
            .ThenInclude(i => i.Instituicao)
            .Include(i => i.InstituicaoCursoOcorrenciaPeriodo)
            .ThenInclude(i => i.InstituicaoCursoOcorrencia)
            .ThenInclude(i => i.InstituicaoCurso)
            .ThenInclude(i => i.Curso)
            .Include(i => i.Professor)
            .Where(x => x.Professor.ID == id && x.Ativo.HasValue == !ativo)
            .ToList();
        }

        public List<InstituicaoCursoOcorrenciaPeriodoProfessor> GetAllInstituicaoCursoOcorrenciaPeriodoProfessorByAluno(string id, bool emCurso, bool ativo) {
            var query = this.db.InstituicoesCursosOcorrenciasPeriodosAlunos
            .AsNoTracking()
            .Where(x => x.InstituicaoCursoOcorrenciaAluno.Aluno.ID == id);

            if (emCurso) {
                query = query.Where(x => !x.InstituicaoCursoOcorrenciaPeriodo.DataExpiracao.HasValue || (x.InstituicaoCursoOcorrenciaPeriodo.DataExpiracao.HasValue && x.InstituicaoCursoOcorrenciaPeriodo.DataExpiracao.Value.Date > DateTime.Now.Date));
            }

            var idsInstituicaoCursoOcorrenciaPeriodo = query
            .Select(x => x.InstituicaoCursoOcorrenciaPeriodo.ID)
            .ToList();

            return this.db.InstituicoesCursosOcorrenciasPeriodosProfessores
            .AsNoTracking()
            .Include(i => i.CursoGradeMateria)
            .Include(i => i.InstituicaoCursoOcorrenciaPeriodo)
            .ThenInclude(i => i.InstituicaoCursoOcorrencia)
            .ThenInclude(i => i.InstituicaoCurso)
            .ThenInclude(i => i.Instituicao)
            .Include(i => i.InstituicaoCursoOcorrenciaPeriodo)
            .ThenInclude(i => i.InstituicaoCursoOcorrencia)
            .ThenInclude(i => i.InstituicaoCurso)
            .ThenInclude(i => i.Curso)
            .Include(i => i.Professor)
            .Where(x => idsInstituicaoCursoOcorrenciaPeriodo.Contains(x.InstituicaoCursoOcorrenciaPeriodo.ID) && x.Ativo.HasValue == !ativo)
            .ToList();
        }

        public List<InstituicaoCursoOcorrenciaPeriodoProfessor> GetAllInstituicaoCursoOcorrenciaPeriodoProfessorByAlunoAndInstituicaoCursoOcorrencia(string idAluno, long idInstituicaoCursoOcorrencia, bool ativo) {
            var idsInstituicaoCursoOcorrenciaPeriodo = this.db.InstituicoesCursosOcorrenciasPeriodosAlunos
                        .AsNoTracking()
                        .Include(x => x.InstituicaoCursoOcorrenciaAluno)
                        .ThenInclude(x => x.InstituicaoCursoOcorrencia)
                        .Where(x => x.InstituicaoCursoOcorrenciaAluno.Aluno.ID == idAluno && x.InstituicaoCursoOcorrenciaAluno.InstituicaoCursoOcorrencia.ID == idInstituicaoCursoOcorrencia)
                        .Select(x => x.InstituicaoCursoOcorrenciaPeriodo.ID)
                        .ToList();

            return this.db.InstituicoesCursosOcorrenciasPeriodosProfessores
            .AsNoTracking()
            .Include(i => i.CursoGradeMateria)
            .Include(i => i.InstituicaoCursoOcorrenciaPeriodo)
            .ThenInclude(i => i.InstituicaoCursoOcorrencia)
            .ThenInclude(i => i.InstituicaoCurso)
            .ThenInclude(i => i.Instituicao)
            .Include(i => i.InstituicaoCursoOcorrenciaPeriodo)
            .ThenInclude(i => i.InstituicaoCursoOcorrencia)
            .ThenInclude(i => i.InstituicaoCurso)
            .ThenInclude(i => i.Curso)
            .Include(i => i.Professor)
            .Where(x => idsInstituicaoCursoOcorrenciaPeriodo.Contains(x.InstituicaoCursoOcorrenciaPeriodo.ID) && x.Ativo.HasValue == !ativo)
            .ToList();
        }

        public List<InstituicaoCursoOcorrenciaPeriodoAluno> GetAllInstituicaoCursoOcorrenciaPeriodoAlunoByAluno(string id, bool ativo) {
            return this.db.InstituicoesCursosOcorrenciasPeriodosAlunos
            .AsNoTracking()
            .Include(i => i.InstituicaoCursoOcorrenciaAluno)
            .ThenInclude(i => i.Aluno)
            .Include(i => i.InstituicaoCursoOcorrenciaPeriodo)
            .Include(i => i.InstituicaoCursoPeriodo)
            .Include(i => i.InstituicaoCursoTurma)
            .Where(x => x.InstituicaoCursoOcorrenciaAluno.Aluno.ID == id && x.Ativo.HasValue == !ativo)
            .ToList();
        }

        public List<InstituicaoCursoOcorrenciaPeriodoAluno> GetAllInstituicaoCursoOcorrenciaPeriodoAlunoByInstituicaoCursoOcorrenciaPeriodo(long id, bool ativo) {
            return this.db.InstituicoesCursosOcorrenciasPeriodosAlunos
            .AsNoTracking()
            .Include(i => i.InstituicaoCursoOcorrenciaAluno)
            .ThenInclude(i => i.Aluno)
            .Include(i => i.InstituicaoCursoOcorrenciaPeriodo)
            .Include(i => i.InstituicaoCursoPeriodo)
            .Include(i => i.InstituicaoCursoTurma)
            .Where(x => x.InstituicaoCursoOcorrenciaPeriodo.ID == id && x.Ativo.HasValue == !ativo)
            .ToList();
        }

        public List<InstituicaoCursoOcorrenciaPeriodoProfessor> GetAllInstituicaoCursoOcorrenciaPeriodoProfessorByInstituicaoCursoOcorrenciaPeriodo(long id, bool ativo) {
            return this.db.InstituicoesCursosOcorrenciasPeriodosProfessores
            .AsNoTracking()
            .Include(i => i.Professor)
            .Include(i => i.InstituicaoCursoOcorrenciaPeriodo)
            .Include(i => i.InstituicaoCursoPeriodo)
            .Include(i => i.InstituicaoCursoTurma)
            .Include(i => i.CursoGradeMateria)
            .ThenInclude(i => i.Materia)
            .Where(x => x.InstituicaoCursoOcorrenciaPeriodo.ID == id && x.Ativo.HasValue == !ativo)
            .ToList();
        }

        public List<InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula> GetAllInstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaByInstituicaoCursoOcorrenciaPeriodoProfessor(long id, bool ativo) {
            return this.db.InstituicoesCursosOcorrenciasPeriodosProfessoresPeriodoAulas
            .AsNoTracking()
            .Include(i => i.InstituicaoCursoOcorrenciaPeriodoProfessor)
            .Where(x => x.InstituicaoCursoOcorrenciaPeriodoProfessor.ID == id && x.Ativo.HasValue == !ativo)
            .ToList();
        }

        public List<InstituicaoColaborador> GetAllInstituicaoColaboradorByInstituicao(long id, bool ativo) {
            return this.db.InstituicoesColaboradores
            .AsNoTracking()
            .Include(i => i.Instituicao)
            .Include(i => i.Usuario)
            .Where(x => x.Instituicao.ID == id && x.Ativo.HasValue == !ativo)
            .ToList();
        }

        public List<InstituicaoColaboradorPerfil> GetAllInstituicaoColaboradorPerfilByInstituicao(long id, bool ativo) {
            return this.db.InstituicoesColaboradoresPerfis
            .AsNoTracking()
            .Include(i => i.Instituicao)
            .Where(x => x.Instituicao.ID == id && x.Ativo.HasValue == !ativo)
            .ToList();
        }

        public List<InstituicaoColaboradorPerfil> GetAllInstituicaoColaboradorPerfilByUsuario(string id, long idInstituicao, bool ativo) {
            var colaboradores = this.db.InstituicoesColaboradores
            .AsNoTracking()
            .Include(i => i.Usuario)
            .Include(i => i.Instituicao)
            .Where(x => x.Usuario.ID == id && x.Instituicao.ID == idInstituicao && x.Ativo.HasValue == !ativo)
            .ToList();

            return this.db.InstituicoesColaboradoresPerfis.AsNoTracking()
            .Where(x => colaboradores.Any(i => i.Perfis.Contains(x.ID.ToString())) && x.Ativo.HasValue == !ativo)
            .ToList();
        }

        public List<InstituicaoColaboradorPerfil> GetAllInstituicaoColaboradorPerfilByUsuario(string id, bool ativo) {
            var colaboradores = this.db.InstituicoesColaboradores
            .AsNoTracking()
            .Include(i => i.Usuario)
            .Include(i => i.Instituicao)
            .Where(x => x.Usuario.ID == id && x.Ativo.HasValue == !ativo)
            .ToList();

            return this.db.InstituicoesColaboradoresPerfis.AsNoTracking()
            .Where(x => colaboradores.Any(i => i.Perfis.Contains(x.ID.ToString())) && x.Ativo.HasValue == !ativo)
            .ToList();
        }

        public List<Instituicao> AllInstituicaoByUsuario(string id, bool ativo) {
            var instituicoes = this.db.InstituicoesColaboradores
            .AsNoTracking()
            .Include(i => i.Usuario)
            .Include(i => i.Instituicao)
            .Where(x => x.Usuario.ID == id && x.Ativo.HasValue == !ativo);
            return instituicoes
            .Select(x => x.Instituicao)
            .GroupBy(x => x.ID)
            .Select(x => x.First())
            .ToList();
        }

        public string GetFormulaNotaFinalWithValuesByAluno(long idInstituicaoCursoOcorrenciaPeriodoProfessor, string idAluno) {
            var instituicaoCursoOcorrenciaPeriodoProfessor = this.GetInstituicaoCursoOcorrenciaPeriodoProfessor(idInstituicaoCursoOcorrenciaPeriodoProfessor);
            var instituicaoCursoOcorrenciaPeriodoAluno = this.db.InstituicoesCursosOcorrenciasPeriodosAlunos
            .Include(x => x.InstituicaoCursoOcorrenciaPeriodo)
            .Include(i => i.InstituicaoCursoOcorrenciaAluno)
            .ThenInclude(i => i.Aluno)
            .SingleOrDefault(x => x.InstituicaoCursoOcorrenciaAluno.Aluno.ID == idAluno && x.InstituicaoCursoOcorrenciaPeriodo.ID == instituicaoCursoOcorrenciaPeriodoProfessor.InstituicaoCursoOcorrenciaPeriodo.ID);
            return this.GetFormulaNotaFinalWithValuesByInstituicaoCursoOcorrenciaPeriodoAluno(idInstituicaoCursoOcorrenciaPeriodoProfessor, instituicaoCursoOcorrenciaPeriodoAluno.ID);
        }

        public string GetFormulaNotaFinalWithValuesByInstituicaoCursoOcorrenciaPeriodoAluno(long idInstituicaoCursoOcorrenciaPeriodoProfessor, long idInstituicaoCursoOcorrenciaPeriodoAluno) {
            var formulaNotaFinal = this.db.InstituicoesCursosOcorrenciasPeriodosProfessores.SingleOrDefault(x => x.ID == idInstituicaoCursoOcorrenciaPeriodoProfessor).FormulaNotaFinal;
            var notas = this.db.InstituicoesCursosOcorrenciasNotas
                .AsNoTracking()
                .Where(x => x.InstituicaoCursoOcorrenciaPeriodoAluno.ID == idInstituicaoCursoOcorrenciaPeriodoAluno && x.InstituicaoCursoOcorrenciaPeriodoProfessor.ID == idInstituicaoCursoOcorrenciaPeriodoProfessor && !x.Ativo.HasValue)
                .ToList();
            if (formulaNotaFinal != null && formulaNotaFinal.Length != 0) {
                formulaNotaFinal = formulaNotaFinal.Replace(",", "");
                notas.ForEach(x => {
                    formulaNotaFinal = formulaNotaFinal.Replace(x.IDTag, x.Valor.ToString());
                });
            } else if (notas.Count > 0) {
                formulaNotaFinal = String.Join("+", notas.Select(x => x.Valor.ToString()).ToArray());
                formulaNotaFinal = "(" + formulaNotaFinal + ")/" + notas.Count;
            } else {
                formulaNotaFinal = "0";
            }
            return formulaNotaFinal;
        }

        public bool HavePermission(long idInstituicao, string[] Roles) {
            return true;
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