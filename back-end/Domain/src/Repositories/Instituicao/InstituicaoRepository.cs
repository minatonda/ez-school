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

        public InstituicaoRepository (BaseContext db, CursoRepository cursoRepository) {
            this.db = db;
            this.cursoRepository = cursoRepository;
        }
        public Instituicao Add (Instituicao model) {
            this.db.Instituicoes.Add (model);
            this.db.SaveChanges ();
            return model;
        }
        public Instituicao Update (Instituicao model) {
            this.db.Instituicoes.Find (model.ID).Nome = model.Nome;
            this.db.Instituicoes.Find (model.ID).CNPJ = model.CNPJ;
            this.db.Instituicoes.Update (this.db.Instituicoes.Find (model.ID));
            this.db.SaveChanges ();
            return model;
        }
        public void Disable (long ID) {
            this.db.Instituicoes.Find (ID).Ativo = false;
            this.db.Instituicoes.Update (this.db.Instituicoes.Find (ID));
            this.db.SaveChanges ();
        }
        public Instituicao Get (long ID) => this.db.Instituicoes.Find (ID);
        public List<Instituicao> GetAll (bool? ativo) => this.db.Instituicoes.Where (x => x.Ativo == (ativo.HasValue ? ativo.Value : false)).ToList ();
        public IEnumerable<Instituicao> Query (Expression<Func<Instituicao, bool>> predicate, params Expression<Func<Instituicao, object>>[] includeExpressions) {
            return includeExpressions.Aggregate<Expression<Func<Instituicao, object>>, IQueryable<Instituicao>> (db.Instituicoes, (current, expression) => current.Include (expression)).Where (predicate.Compile ());
        }

        public List<InstituicaoCategoria> GetCategorias (long ID) => this.db.InstituicaoInstituicaoCategorias.Include (i => i.InstituicaoCategoria).Include (i => i.Instituicao).Where (x => x.Instituicao.ID == ID).Select (i => i.InstituicaoCategoria).ToList ();
        public void AddCategoria (long ID, InstituicaoCategoria instituicaoCategoria) {
            this.db.Attach (instituicaoCategoria);
            this.db.InstituicaoInstituicaoCategorias.Add (new InstituicaoInstituicaoCategoria () {
                Instituicao = this.Get (ID),
                    InstituicaoCategoria = instituicaoCategoria
            });
            this.db.SaveChanges ();
        }
        public void DeleteCategoria (long ID, long IDCategoria) {
            var instituicaoCategoria = this.db.InstituicaoInstituicaoCategorias.Include (i => i.Instituicao).Include (i => i.InstituicaoCategoria).SingleOrDefault (x => x.Instituicao.ID == ID && x.InstituicaoCategoria.ID == IDCategoria);
            this.db.InstituicaoInstituicaoCategorias.Remove (instituicaoCategoria);
            this.db.SaveChanges ();
        }

        public InstituicaoCursoDto GetCurso (long ID, long IDCurso) {
            var model = this.db.InstituicaoCursos.Include (i => i.Curso).Include (i => i.Instituicao).Include (i => i.CursoGrade).SingleOrDefault (x => x.Instituicao.ID == ID && x.Curso.ID == IDCurso);
            if (model != null) {
                var materias = this.db.CursoGradeMaterias.Include (i => i.Materia).Include (i => i.CursoGrade).Where (x => x.CursoGrade.ID == model.CursoGrade.ID).ToList ();
                var periodos = this.db.InstituicaoCursoPeriodos.Include (i => i.InstituicaoCurso).Where (x => x.InstituicaoCurso.ID == model.ID).ToList ();
                return new InstituicaoCursoDto (model, periodos, materias);
            }
            else{
                throw new Exception("Registro não encontrado");
            }
        }
        public List<InstituicaoCursoDto> GetCursos (long ID) {
            var listInstituicaoCurso = this.db.InstituicaoCursos.Include (i => i.Curso).Include (i => i.Instituicao).Include (i => i.CursoGrade).Where (x => x.Instituicao.ID == ID && x.DataExpiracao == null && x.Ativo).ToList ();
            var listInstituicaoCursoDto = new List<InstituicaoCursoDto> ();
            return listInstituicaoCurso.Select (x => new InstituicaoCursoDto (x, null, null)).ToList ();
        }
        public void AddCurso (long ID, InstituicaoCursoDto model) {
            if (this.db.InstituicaoCursos.SingleOrDefault (x => x.Curso.ID == model.Curso.ID && x.Instituicao.ID == ID && x.DataExpiracao != null && x.Ativo) != null) {
                throw new Exception ("O Curso já existe, as operações permitidas são renovar e desativar.");
            } else {
                var instituicaoCurso = new InstituicaoCurso () {
                    Instituicao = this.Get (ID),
                    Curso = this.db.Cursos.Find (model.Curso.ID),
                    CursoGrade = this.db.CursoGrades.Find (model.CursoGrade.ID),
                    DataInicio = DateTime.Now,
                };
                this.db.InstituicaoCursoPeriodos.AddRange (model.Periodos.Select (x => new InstituicaoCursoPeriodo () {
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
                this.db.InstituicaoCursos.Add (instituicaoCurso);
                this.db.SaveChanges ();
            }
        }
        public void RenewCurso (long ID, InstituicaoCursoDto model) {
            this.db.InstituicaoCursos.SingleOrDefault (x => x.Curso.ID == model.Curso.ID && x.Instituicao.ID == ID && x.DataExpiracao == null && x.Ativo).DataExpiracao = DateTime.Now;
            this.AddCurso (ID, model);
        }
        public void DisableCurso (long ID, long IDCurso) {
            var instituicaoCurso = this.db.InstituicaoCursos.Include (i => i.Instituicao).Include (i => i.Curso).SingleOrDefault (x => x.Instituicao.ID == ID && x.Curso.ID == IDCurso && x.DataExpiracao == null && x.Ativo);
            instituicaoCurso.Ativo = false;
            instituicaoCurso.DataExpiracao = DateTime.Now;
            this.db.SaveChanges ();
        }

        public InstituicaoCursoOcorrenciaDto AddCursoOcorrencia (long ID, long IDCurso, InstituicaoCursoOcorrenciaDto model) {
            this.db.AttachRange (model.Alunos);
            var instituicaoCursoOcorrencia = new InstituicaoCursoOcorrencia () {
                InstituicaoCurso = this.db.InstituicaoCursos.SingleOrDefault (x => x.Instituicao.ID == ID && x.ID == IDCurso),
                DataInicio = model.DataInicio,
                DataExpiracao = model.DataExpiracao
            };
            var listInstituicaoCursoOcorrenciaAluno = model.Alunos
                .Select (x => new InstituicaoCursoOcorrenciaAluno () {
                    Aluno = x,
                        InstituicaoCursoOcorrencia = instituicaoCursoOcorrencia,
                        DataInicio = model.DataInicio,
                        DataExpiracao = model.DataExpiracao
                }).ToList ();

            this.db.InstituicaoCursoOcorrencias.Add (instituicaoCursoOcorrencia);
            this.db.InstituicaoCursoOcorrenciaAluno.AddRange (listInstituicaoCursoOcorrenciaAluno);
            this.db.SaveChanges ();

            model.ID = instituicaoCursoOcorrencia.ID;
            return model;
        }
        public void DeleteCursoOcorrencia (long ID, long IDCurso, long IDCursoOcorrencia) {
            var instituicaoCursoOcorrencia = this.db.InstituicaoCursoOcorrencias
                .SingleOrDefault (
                    x => x.ID == IDCursoOcorrencia &&
                    x.InstituicaoCurso.Curso.ID == IDCurso &&
                    x.InstituicaoCurso.Instituicao.ID == ID
                );
            this.db.Remove (instituicaoCursoOcorrencia);
            this.db.SaveChanges ();
        }
        public InstituicaoCursoOcorrenciaDto GetCursoOcorrencia (long ID, long IDCurso, long IDCursoOcorrencia) {
            var instituicaoCursoOcorrencia = this.db.InstituicaoCursoOcorrencias
                .Include (i => i.InstituicaoCurso)
                .ThenInclude (i => i.Instituicao)
                .Include (i => i.InstituicaoCurso)
                .ThenInclude (i => i.Curso)
                .Include (i => i.InstituicaoCurso)
                .ThenInclude (i => i.CursoGrade)
                .SingleOrDefault (x =>
                    x.ID == IDCursoOcorrencia &&
                    x.InstituicaoCurso.Curso.ID == IDCurso &&
                    x.InstituicaoCurso.Instituicao.ID == ID
                );
            return new InstituicaoCursoOcorrenciaDto () {
                ID = instituicaoCursoOcorrencia.ID,
                Alunos = this.db.InstituicaoCursoOcorrenciaAluno
                .Include (i => i.InstituicaoCursoOcorrencia)
                .ThenInclude (i => i.InstituicaoCurso)
                .ThenInclude (i => i.Instituicao)
                .Where (x =>
                x.InstituicaoCursoOcorrencia.ID == IDCursoOcorrencia &&
                x.InstituicaoCursoOcorrencia.InstituicaoCurso.ID == IDCurso &&
                x.InstituicaoCursoOcorrencia.InstituicaoCurso.Instituicao.ID == ID)
                .Select (
                x => x.Aluno
                ).ToList (),
                DataInicio = instituicaoCursoOcorrencia.DataInicio,
                DataExpiracao = instituicaoCursoOcorrencia.DataExpiracao,
            };
        }
        public List<InstituicaoCursoOcorrenciaDto> GetCursoOcorrencias (long ID, long IDCurso) {
            return this.db.InstituicaoCursoOcorrencias
                .Include (i => i.InstituicaoCurso)
                .ThenInclude (i => i.Instituicao)
                .Include (i => i.InstituicaoCurso)
                .ThenInclude (i => i.Curso)
                .Where (x =>
                    x.InstituicaoCurso.Curso.ID == IDCurso &&
                    x.InstituicaoCurso.Instituicao.ID == ID
                )
                .Select (x => new InstituicaoCursoOcorrenciaDto () {
                    ID = x.ID,
                        DataInicio = x.DataInicio,
                        DataExpiracao = x.DataExpiracao,
                })
                .ToList ();
        }

        public InstituicaoCursoOcorrenciaMateriaDto AddCursoOcorrenciaMateria (long ID, long IDCurso, long IDCursoOcorrencia, InstituicaoCursoOcorrenciaMateriaDto model) {
            var instituicaoCursoOcorrencia = this.db.InstituicaoCursoOcorrencias.SingleOrDefault (x => x.InstituicaoCurso.Instituicao.ID == ID && x.InstituicaoCurso.Curso.ID == IDCurso && x.ID == IDCursoOcorrencia);
            var instituicaoCursoOcorrenciaMateria = new InstituicaoCursoOcorrenciaMateria () {
                InstituicaoCursoOcorrencia = instituicaoCursoOcorrencia,
                Professor = model.Professor,
                Materia = model.Materia,
                DataInicio = model.DataInicio,
                DataExpiracao = model.DataExpiracao
            };
            this.db.InstituicaoCursoOcorrenciaMaterias.Add (instituicaoCursoOcorrenciaMateria);

            model.ID = instituicaoCursoOcorrenciaMateria.ID;
            return model;
        }
        public void DeleteCursoOcorrenciaMateria (long ID, long IDCurso, long IDCursoOcorrencia, long IDCursoOcorrenciaMateria) {
            var instituicaoCursoOcorrencia = this.db.InstituicaoCursoOcorrenciaMaterias
                .SingleOrDefault (x => x.ID == IDCursoOcorrenciaMateria &&
                    x.InstituicaoCursoOcorrencia.ID == IDCursoOcorrencia &&
                    x.InstituicaoCursoOcorrencia.InstituicaoCurso.ID == IDCurso &&
                    x.InstituicaoCursoOcorrencia.InstituicaoCurso.Instituicao.ID == ID
                );
            this.db.Remove (instituicaoCursoOcorrencia);
            this.db.SaveChanges ();
        }
        public InstituicaoCursoOcorrenciaMateriaDto GetInstituicaoCursoOcorrenciaMateria (long ID, long IDCurso, long IDCursoOcorrencia, long IDCursoOcorrenciaMateria) {
            var instituicaoCursoOcorrencia = this.db.InstituicaoCursoOcorrenciaMaterias
                .Include (i => i.Materia)
                .Include (i => i.Professor)

                .Include (i => i.InstituicaoCursoOcorrencia)
                .ThenInclude (i => i.InstituicaoCurso)
                .ThenInclude (i => i.Instituicao)

                .Include (i => i.InstituicaoCursoOcorrencia)
                .ThenInclude (i => i.InstituicaoCurso)
                .ThenInclude (i => i.Curso)

                .Include (i => i.InstituicaoCursoOcorrencia)
                .ThenInclude (i => i.InstituicaoCurso)
                .ThenInclude (i => i.CursoGrade)

                .SingleOrDefault (x =>
                    x.ID == IDCursoOcorrenciaMateria &&
                    x.InstituicaoCursoOcorrencia.ID == IDCursoOcorrencia &&
                    x.InstituicaoCursoOcorrencia.InstituicaoCurso.Curso.ID == IDCurso &&
                    x.InstituicaoCursoOcorrencia.InstituicaoCurso.Instituicao.ID == ID
                );
            return new InstituicaoCursoOcorrenciaMateriaDto () {
                ID = instituicaoCursoOcorrencia.ID,
                Materia = instituicaoCursoOcorrencia.Materia,
                Professor = instituicaoCursoOcorrencia.Professor,
                DataInicio = instituicaoCursoOcorrencia.DataInicio,
                DataExpiracao = instituicaoCursoOcorrencia.DataExpiracao,
            };
        }
        public List<InstituicaoCursoOcorrenciaMateriaDto> GetInstituicaoCursoOcorrenciaMaterias (long ID, long IDCurso, long IDCursoOcorrencia, long IDCursoOcorrenciaMateria) {
            return this.db.InstituicaoCursoOcorrenciaMaterias
                .Include (i => i.Materia)
                .Include (i => i.Professor)

                .Include (i => i.InstituicaoCursoOcorrencia)
                .ThenInclude (i => i.InstituicaoCurso)
                .ThenInclude (i => i.Instituicao)

                .Include (i => i.InstituicaoCursoOcorrencia)
                .ThenInclude (i => i.InstituicaoCurso)
                .ThenInclude (i => i.Curso)

                .Where (x =>
                    x.InstituicaoCursoOcorrencia.ID == IDCursoOcorrencia &&
                    x.InstituicaoCursoOcorrencia.InstituicaoCurso.Curso.ID == IDCurso &&
                    x.InstituicaoCursoOcorrencia.InstituicaoCurso.Instituicao.ID == ID
                )
                .Select (x =>
                    new InstituicaoCursoOcorrenciaMateriaDto () {
                        ID = x.ID,
                            Materia = x.Materia,
                            Professor = x.Professor,
                            DataInicio = x.DataInicio,
                            DataExpiracao = x.DataExpiracao,
                    }
                )
                .ToList ();
        }

    }

}