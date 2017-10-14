using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain;
using Domain.Dto;
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

        public InstituicaoCursoDto GetCurso(long ID, long IDCurso, DateTime? DataInicio) {
            var model = this._GetCurso(ID, IDCurso, DataInicio);
            if (model != null) {
                var materias = this.db.CursoGradeMaterias.Include(i => i.Materia).Include(i => i.CursoGrade).Where(x => x.CursoGrade.ID == model.CursoGrade.ID).ToList();
                var periodos = this.db.InstituicaoCursoPeriodos.Include(i => i.InstituicaoCurso).Where(x => x.InstituicaoCurso.ID == model.ID).ToList();
                return new InstituicaoCursoDto(model, periodos, materias);
            } else {
                throw new Exception("Registro não encontrado");
            }
        }
        public List<InstituicaoCursoDto> GetCursos(long ID) {
            var listInstituicaoCurso = this.db.InstituicaoCursos.Include(i => i.Curso).Include(i => i.Instituicao).Include(i => i.CursoGrade).Where(x => x.Instituicao.ID == ID && !x.DataExpiracao.HasValue && x.Ativo).ToList();
            var listInstituicaoCursoDto = new List<InstituicaoCursoDto>();
            return listInstituicaoCurso.Select(x => new InstituicaoCursoDto(x, null, null)).ToList();
        }
        public void AddCurso(long ID, InstituicaoCursoDto model) {
            if (this.db.InstituicaoCursos.Include(i => i.Curso).Include(i => i.Instituicao).SingleOrDefault(x => x.Curso.ID == model.Curso.ID && x.Instituicao.ID == ID && x.DataExpiracao != null && x.Ativo) != null) {
                throw new Exception("O Curso já existe, as operações permitidas são renovar e desativar.");
            } else {
                var instituicaoCurso = new InstituicaoCurso() {
                    Instituicao = this.Get(ID),
                    Curso = this.db.Cursos.Find(model.Curso.ID),
                    CursoGrade = this.db.CursoGrades.Find(model.CursoGrade.ID),
                    DataInicio = DateTime.Now,
                };
                this.db.InstituicaoCursoPeriodos.AddRange(model.Periodos.Select(x => new InstituicaoCursoPeriodo() {
                    InstituicaoCurso = instituicaoCurso,
                    Inicio = x.Inicio,
                    Fim = x.Fim,
                    Seg = x.Seg,
                    Ter = x.Ter,
                    Qua = x.Qua,
                    Qui = x.Qui,
                    Sex = x.Sex,
                    Sab = x.Sab,
                    Dom = x.Dom
                }));
                this.db.InstituicaoCursos.Add(instituicaoCurso);
                this.db.SaveChanges();
            }
        }
        public void RenewCurso(long ID, InstituicaoCursoDto model) {
            this.db.InstituicaoCursos.SingleOrDefault(x => x.Curso.ID == model.Curso.ID && x.Instituicao.ID == ID && x.DataExpiracao == null && x.Ativo).DataExpiracao = DateTime.Now;
            this.AddCurso(ID, model);
        }
        public void DisableCurso(long ID, long IDCurso) {
            var instituicaoCurso = this.db.InstituicaoCursos.Include(i => i.Instituicao).Include(i => i.Curso).SingleOrDefault(x => x.Instituicao.ID == ID && x.Curso.ID == IDCurso && x.DataExpiracao == null && x.Ativo);
            instituicaoCurso.Ativo = false;
            instituicaoCurso.DataExpiracao = DateTime.Now;
            this.db.SaveChanges();
        }

        public InstituicaoCursoOcorrenciaDto AddCursoOcorrencia(long ID, long IDCurso, DateTime? InstituicaoCursoDataInicio, InstituicaoCursoOcorrenciaDto model) {
            var instituicaoCursoOcorrencia = new InstituicaoCursoOcorrencia() {
                InstituicaoCurso = this._GetCurso(ID, IDCurso, InstituicaoCursoDataInicio),
                DataInicio = model.DataInicio,
                DataExpiracao = model.DataExpiracao
            };
            var instituicaoCursoOcorrenciaCoordenador = new InstituicaoCursoOcorrenciaCoordenador() {
                Professor = model.Coordenador,
                DataInicio = model.DataInicio,
                DataExpiracao = model.DataExpiracao,
                InstituicaoCursoOcorrencia = instituicaoCursoOcorrencia,
            };
            var materias = model.Periodos.Select(x => x.Professores).SelectMany(x => x.Select(y => y.Materia)).GroupBy(i => i.ID).Select(i => i.First()).ToList();
            foreach (var materia in materias) {
                var attachedMateria = this.db.Materias.Find(materia.ID);
                var instituicaoCursoOcorrenciaMateria = new InstituicaoCursoOcorrenciaMateria(instituicaoCursoOcorrencia, attachedMateria);
                foreach (var periodo in model.Periodos) {
                    var attachedPeriodo = this.db.InstituicaoCursoPeriodos.Find(periodo.Periodo.ID);
                    var registroProfessorMateria = periodo.Professores.SingleOrDefault(x => x.Materia.ID == materia.ID);
                    if (registroProfessorMateria != null) {
                        var instituicaoCursoOcorrenciaMateriaProfessor = new InstituicaoCursoOcorrenciaMateriaProfessor() {
                            InstituicaoCursoOcorrenciaMateria = instituicaoCursoOcorrenciaMateria,
                            DataInicio = DateTime.Now,
                            Professor = this.db.Professores.Find(registroProfessorMateria.Professor.ID),
                            Periodo = attachedPeriodo
                        };
                        this.db.InstituicaoCursoOcorrenciaMateriaProfessores.Add(instituicaoCursoOcorrenciaMateriaProfessor);
                    }
                    var instituicaoCursoOcorrenciaAlunos = periodo.Alunos.Select(x => new InstituicaoCursoOcorrenciaAluno() {
                        Aluno = x,
                        InstituicaoCursoOcorrencia = instituicaoCursoOcorrencia,
                        DataInicio = instituicaoCursoOcorrencia.DataInicio,
                        Periodo = attachedPeriodo
                    });
                    this.db.InstituicaoCursoOcorrenciaAlunos.AddRange(instituicaoCursoOcorrenciaAlunos);
                }
            }
            this.db.InstituicaoCursoOcorrencias.Add(instituicaoCursoOcorrencia);
            this.db.InstituicaoCursoOcorrenciaCoordenadores.Add(instituicaoCursoOcorrenciaCoordenador);
            this.db.SaveChanges();
            model.ID = instituicaoCursoOcorrencia.ID;
            return model;
        }

        public InstituicaoCursoOcorrenciaDto GetCursoOcorrencia(long ID, long IDCurso, DateTime InstituicaoCursoDataInicio, DateTime InstituicaoCursoOcorrenciaDataInicio) {
            var instituicaoCursoOcorrencia = this._GetCursoOcorrencia(ID, IDCurso, InstituicaoCursoDataInicio, InstituicaoCursoOcorrenciaDataInicio);
            var coordenador = this._GetInstituicaoCursoOcorrenciaCoordenador(instituicaoCursoOcorrencia);
            var alunos = this._GetInstituicaoCursoOcorrenciaAluno(instituicaoCursoOcorrencia);
            var professores = this._GetInstituicaoCursoOcorrenciaMateriaProfessor(instituicaoCursoOcorrencia);
            return new InstituicaoCursoOcorrenciaDto(instituicaoCursoOcorrencia, coordenador.Professor, alunos, professores);
        }

        public List<InstituicaoCursoOcorrenciaDto> GetCursoOcorrencias(long ID, long IDCurso, DateTime InstituicaoCursoDataInicio) {
            return this.db.InstituicaoCursoOcorrencias
           .Include(i => i.InstituicaoCurso).Where(x => x.InstituicaoCurso.ID == this._GetCurso(ID, IDCurso, InstituicaoCursoDataInicio).ID).Select(x => new InstituicaoCursoOcorrenciaDto(x, this._GetInstituicaoCursoOcorrenciaCoordenador(x).Professor)).ToList();
        }

        private InstituicaoCurso _GetCurso(long ID, long IDCurso, DateTime? dataInicio) {
            var where = this.db.InstituicaoCursos
            .Include(i => i.Curso)
            .Include(i => i.Instituicao)
            .Include(i => i.CursoGrade)
            .Where(x => x.Instituicao.ID == ID && x.Curso.ID == IDCurso);
            return where.SingleOrDefault(x => x.DataInicio.Date == dataInicio.Value.Date);
        }

        private InstituicaoCursoOcorrencia _GetCursoOcorrencia(long ID, long IDCurso, DateTime? dataInicio, DateTime? instituicaoCursoOcorrenciaDataInicio) {
            return this.db.InstituicaoCursoOcorrencias
            .Include(i => i.InstituicaoCurso)
            .SingleOrDefault(x => x.InstituicaoCurso.ID == this._GetCurso(ID, IDCurso, dataInicio).ID && x.DataInicio.HasValue && x.DataInicio.Value.Date.Equals(dataInicio.Value.Date));
        }

        private List<InstituicaoCursoOcorrenciaMateria> _GetInstituicaoCursoOcorrenciaMaterias(InstituicaoCursoOcorrencia instituicaoCursoOcorrencia) {
            return this.db.InstituicaoCursoOcorrenciaMaterias
            .Include(x => x.InstituicaoCursoOcorrencia)
            .Include(x => x.Materia)
            .Where(x => x.InstituicaoCursoOcorrencia.ID == instituicaoCursoOcorrencia.ID).ToList();
        }

        private List<InstituicaoCursoOcorrenciaAluno> _GetInstituicaoCursoOcorrenciaAluno(InstituicaoCursoOcorrencia instituicaoCursoOcorrencia) {
            return this.db.InstituicaoCursoOcorrenciaAlunos
            .Include(i => i.InstituicaoCursoOcorrencia)
            .Include(i => i.Periodo)
            .Include(i => i.Aluno)
            .Where(x => x.InstituicaoCursoOcorrencia.ID == instituicaoCursoOcorrencia.ID)
            .ToList();
        }

        private List<InstituicaoCursoOcorrenciaMateriaProfessor> _GetInstituicaoCursoOcorrenciaMateriaProfessor(InstituicaoCursoOcorrencia instituicaoCursoOcorrencia) {
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

        private InstituicaoCursoOcorrenciaCoordenador _GetInstituicaoCursoOcorrenciaCoordenador(InstituicaoCursoOcorrencia instituicaoCursoOcorrencia) {
            return this.db.InstituicaoCursoOcorrenciaCoordenadores
            .SingleOrDefault(x => x.InstituicaoCursoOcorrencia.ID == instituicaoCursoOcorrencia.ID && x.DataExpiracao == null);
        }

    }

}