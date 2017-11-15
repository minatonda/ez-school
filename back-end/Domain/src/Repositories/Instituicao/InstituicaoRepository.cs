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
        private UsuarioRepository usuarioRepository;

        public InstituicaoRepository(BaseContext db, CursoRepository cursoRepository, UsuarioRepository usuarioRepository) {
            this.db = db;
            this.cursoRepository = cursoRepository;
            this.usuarioRepository = usuarioRepository;
        }

        public Instituicao Add(Instituicao model) {
            this.db.Instituicoes.Add(model);
            this.db.SaveChanges();
            return model;
        }

        public Instituicao AddHistory(long id) {
            var history = this.Get(id);
            history.Ativo = DateTime.Now;
            //falta adicionar historico de dependências.

            this.db.Entry(history).State = EntityState.Detached;
            this.Add(history);
            return history;
        }

        public Instituicao Update(Instituicao model) {
            this.db.Instituicoes.Find(model.ID).Nome = model.Nome;
            this.db.Instituicoes.Find(model.ID).CNPJ = model.CNPJ;
            this.db.Instituicoes.Update(this.db.Instituicoes.Find(model.ID));
            this.db.SaveChanges();
            return model;
        }

        public void Disable(long id) {
            this.db.Instituicoes.Find(id).Ativo = DateTime.Now;
            this.db.Instituicoes.Update(this.db.Instituicoes.Find(id));
            this.db.SaveChanges();
        }

        public Instituicao Get(long id) {
            return this.db.Instituicoes.Find(id);
        }

        public List<Instituicao> GetAll(bool ativo) {
            if (ativo) {
                return this.db.Instituicoes.Where(x => !x.Ativo.HasValue).ToList();
            } else {
                return this.db.Instituicoes.ToList();
            }
        }

        public IEnumerable<Instituicao> Query(Expression<Func<Instituicao, bool>> predicate, params Expression<Func<Instituicao, object>>[] includeExpressions) {
            return includeExpressions.Aggregate<Expression<Func<Instituicao, object>>, IQueryable<Instituicao>>(db.Instituicoes, (current, expression) => current.Include(expression)).Where(predicate.Compile());
        }

        public InstituicaoCurso GetCurso(long id, long idCurso) {
            var where = this.db.InstituicaoCursos
            .Include(i => i.Curso)
            .Include(i => i.Instituicao)
            .Include(i => i.CursoGrade)
            .Where(x => x.Instituicao.ID == id && x.Curso.ID == idCurso);
            return where.SingleOrDefault(x => !x.DataExpiracao.HasValue);
        }

        public List<InstituicaoCurso> GetCursos(long id) {
            return this.db.InstituicaoCursos.Include(i => i.Curso).Include(i => i.Instituicao).Include(i => i.CursoGrade).Where(x => x.Instituicao.ID == id && !x.Ativo.HasValue).ToList();
        }

        public List<InstituicaoCursoTurma> GetCursoTurmas(long id, long idCurso) {
            var curso = this.GetCurso(id, idCurso);
            return this.db.InstituicaoCursoTurmas
             .Include(i => i.InstituicaoCurso)
             .Where(x => x.InstituicaoCurso.ID == curso.ID).ToList();
        }

        public List<InstituicaoCursoPeriodo> GetCursoPeriodos(long id, long idCurso) {
            var curso = this.GetCurso(id, idCurso);
            return this.db.InstituicaoCursoPeriodos
             .Include(i => i.InstituicaoCurso)
             .Where(x => x.InstituicaoCurso.ID == curso.ID).ToList();
        }

        public List<CursoGradeMateria> GetCursoGradeMaterias(long id, long idCurso) {
            var curso = this.GetCurso(id, idCurso);
            return this.db.CursoGradeMaterias
             .Include(i => i.CursoGrade)
             .Include(i => i.Materia)
             .Where(x => x.CursoGrade.ID == curso.CursoGrade.ID).ToList();
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



        public InstituicaoCurso AddCurso(long id, InstituicaoCurso model, List<InstituicaoCursoPeriodo> periodos, List<InstituicaoCursoTurma> turmas) {
            model.Instituicao = this.Get(id);
            model.Curso = this.db.Cursos.Find(model.Curso.ID);
            model.CursoGrade = this.db.CursoGrades.Find(model.CursoGrade.ID);

            periodos.ForEach(x => x.InstituicaoCurso = model);
            this.db.InstituicaoCursoPeriodos.AddRange(periodos);

            turmas.ForEach(x => x.InstituicaoCurso = model);
            this.db.InstituicaoCursoTurmas.AddRange(turmas);

            this.db.InstituicaoCursos.Add(model);
            this.db.SaveChanges();
            return model;
        }

        public InstituicaoCurso AddCursoHistory(long id, long idCurso) {
            var history = this.GetCurso(id, idCurso);
            history.Ativo = DateTime.Now;
            //falta adicionar historico de dependências.

            this.db.Entry(history).State = EntityState.Detached;
            this.AddCurso(id, history, null, null);
            return history;
        }

        public InstituicaoCurso UpdateCurso(long id, InstituicaoCurso model, List<InstituicaoCursoPeriodo> periodos, List<InstituicaoCursoTurma> turmas) {
            this.AddCursoHistory(id, model.ID);

            var entity = this.GetCurso(id, model.ID);
            entity.Curso = this.db.Cursos.Find(model.Curso.ID);
            entity.CursoGrade = this.db.CursoGrades.Find(model.CursoGrade.ID);

            periodos.ForEach(x => x.InstituicaoCurso = model);
            turmas.ForEach(x => x.InstituicaoCurso = model);

            var periodosCurso = this.GetCursoPeriodos(id, model.ID);
            var periodosRemove = periodosCurso.Where(x => !periodos.Select(y => y.ID).Contains(x.ID));
            var periodosAdd = periodos.Where(x => !periodosCurso.Select(y => y.ID).Contains(x.ID));

            this.db.InstituicaoCursoPeriodos.AddRange(periodosAdd);
            this.db.InstituicaoCursoPeriodos.RemoveRange(periodosRemove);

            var turmasCurso = this.GetCursoTurmas(id, model.ID);
            var turmasRemove = turmasCurso.Where(x => !turmas.Select(y => y.ID).Contains(x.ID));
            var turmasAdd = turmas.Where(x => !turmasCurso.Select(y => y.ID).Contains(x.ID));

            this.db.InstituicaoCursoTurmas.AddRange(turmasAdd);
            this.db.InstituicaoCursoTurmas.RemoveRange(turmasRemove);

            this.db.InstituicaoCursos.Update(model);
            this.db.SaveChanges();
            return model;

        }

        public void DisableCurso(long id, long idCurso) {
            var instituicaoCurso = this.GetCurso(id, idCurso);
            instituicaoCurso.Ativo = DateTime.Now;
            instituicaoCurso.DataExpiracao = DateTime.Now;
            this.db.SaveChanges();
        }

        public InstituicaoCursoOcorrencia AddCursoOcorrencia(long id, long idCurso, InstituicaoCursoOcorrencia model, List<InstituicaoCursoOcorrenciaAluno> alunos) {
            var instituicaoCurso = this.GetCurso(id, idCurso);
            model.InstituicaoCurso = instituicaoCurso;
            model.Coordenador = this.usuarioRepository.GetProfessor(model.Coordenador.ID);
            alunos.ForEach(x => x.Aluno = this.usuarioRepository.GetAluno(x.Aluno.ID));
            alunos.ForEach(x => x.InstituicaoCursoOcorrencia = model);
            this.db.InstituicaoCursoOcorrenciaAlunos.AddRange(alunos);
            this.db.InstituicaoCursoOcorrencias.Add(model);
            this.db.SaveChanges();
            return model;
        }

        public List<InstituicaoCursoOcorrencia> GetCursoOcorrencias(long id, long idCurso) {
            return this.db.InstituicaoCursoOcorrencias
            .Include(i => i.InstituicaoCurso)
            .Where(x => x.InstituicaoCurso.ID == this.GetCurso(id, idCurso).ID).ToList();
        }

        public InstituicaoCursoOcorrencia GetCursoOcorrencia(long id, long idCurso, DateTime? dataInicio) {
            return this.db.InstituicaoCursoOcorrencias
            .Include(i => i.InstituicaoCurso)
            .SingleOrDefault(x => x.InstituicaoCurso.ID == this.GetCurso(id, idCurso).ID && x.DataInicio.HasValue && x.DataInicio.Value.Date.Equals(dataInicio.Value.Date));
        }

        public List<InstituicaoCursoOcorrenciaAluno> GetCursoOcorrenciaAlunos(long idCursoOcorrencia) {
            return this.db.InstituicaoCursoOcorrenciaAlunos
            .Include(i => i.InstituicaoCursoOcorrencia)
            .Include(i => i.Aluno)
            .Where(x => x.InstituicaoCursoOcorrencia.ID == idCursoOcorrencia)
            .ToList();
        }

    }

}