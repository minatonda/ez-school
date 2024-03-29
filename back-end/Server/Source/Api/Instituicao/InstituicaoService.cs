using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using Api.Common.Base;
using Api.CursoApi;
using Api.UsuarioApi;
using Domain.CursoDomain;
using Domain.InstituicaoDomain;
using NCalc;

namespace Api.InstituicaoApi {
    public class InstituicaoService {

        private InstituicaoRepository _instituicaoRepository;

        private CursoRepository _cursoRespository;

        public InstituicaoService(InstituicaoRepository instituicaoRepository, CursoRepository cursoRespository) {
            this._instituicaoRepository = instituicaoRepository;
            this._cursoRespository = cursoRespository;
        }

        public void Add(InstituicaoVM viewModel) {
            var model = InstituicaoAdapter.ToModel(viewModel, true);
            this._instituicaoRepository.Add(model);

            this._instituicaoRepository.SaveChanges();
        }

        public void AddInstituicaoCurso(long id, InstituicaoCursoVM instituicaoCurso) {
            var model = InstituicaoCursoAdapter.ToModel(instituicaoCurso, true);
            model.Instituicao = this._instituicaoRepository.Get(id);

            if (this._cursoRespository.GetCursoGrade(model.CursoGrade.ID) == null) {
                var cursoGrade = model.CursoGrade;
                cursoGrade.Curso = model.Curso;
                cursoGrade.Instituicao = model.Instituicao;
                this._cursoRespository.AddCursoGrade(model.CursoGrade);

                instituicaoCurso.CursoGrade.Materias.ForEach(y => {
                    var modelCursoGradeMateria = CursoGradeMateriaAdapter.ToModel(y, true);
                    modelCursoGradeMateria.CursoGrade = cursoGrade;
                    this._cursoRespository.AddCursoGradeMateria(modelCursoGradeMateria);
                });
            }

            this._instituicaoRepository.AddInstituicaoCurso(model);

            var periodos = InstituicaoCursoAdapter.InstituicaoCursoPeriodoFromVM(instituicaoCurso);
            var turmas = InstituicaoCursoAdapter.InstituicaoCursoTurmaFromVM(instituicaoCurso);

            periodos.ForEach(x => {
                x.InstituicaoCurso = model;
                this._instituicaoRepository.AddInstituicaoCursoPeriodo(x);
            });

            turmas.ForEach(x => {
                x.InstituicaoCurso = model;
                this._instituicaoRepository.AddInstituicaoCursoTurma(x);
            });

            this._instituicaoRepository.SaveChanges();
        }

        public void AddInstituicaoCursoOcorrencia(long id, InstituicaoCursoOcorrenciaVM instituicaoCursoOcorrencia) {
            var model = InstituicaoCursoOcorrenciaAdapter.ToModel(instituicaoCursoOcorrencia, true);
            model.InstituicaoCurso = this._instituicaoRepository.GetInstituicaoCurso(id);
            model.DataInicio = model.DataInicio.HasValue ? model.DataInicio : DateTime.Now;
            this._instituicaoRepository.AddInstituicaoCursoOcorrencia(model);

            instituicaoCursoOcorrencia.InstituicaoCursoOcorrenciaPeriodos.ForEach(x => {
                var modelInstituicaoCursoOcorrenciaPeriodo = InstituicaoCursoOcorrenciaPeriodoAdapter.ToModel(x, true);
                modelInstituicaoCursoOcorrenciaPeriodo.InstituicaoCursoOcorrencia = model;

                this._instituicaoRepository.AddInstituicaoCursoOcorrenciaPeriodo(modelInstituicaoCursoOcorrenciaPeriodo);

                x.InstituicaoCursoOcorrenciaPeriodoAlunos.ForEach(y => {
                    var modelInstituicaoCursoOcorrenciaPeriodoAluno = InstituicaoCursoOcorrenciaPeriodoAlunoAdapter.ToModel(y, true);
                    modelInstituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoOcorrenciaAluno = InstituicaoCursoOcorrenciaAlunoAdapter.ToModel(instituicaoCursoOcorrencia, y.Aluno);
                    modelInstituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoOcorrenciaPeriodo = modelInstituicaoCursoOcorrenciaPeriodo;

                    this._instituicaoRepository.AddInstituicaoCursoOcorrenciaPeriodoAluno(modelInstituicaoCursoOcorrenciaPeriodoAluno);
                });

                x.InstituicaoCursoOcorrenciaPeriodoProfessores.ForEach(y => {
                    var modelInstituicaoCursoOcorrenciaPeriodoProfessor = InstituicaoCursoOcorrenciaPeriodoProfessorAdapter.ToModel(y, true);
                    modelInstituicaoCursoOcorrenciaPeriodoProfessor.InstituicaoCursoOcorrenciaPeriodo = modelInstituicaoCursoOcorrenciaPeriodo;

                    this._instituicaoRepository.AddInstituicaoCursoOcorrenciaPeriodoProfessor(modelInstituicaoCursoOcorrenciaPeriodoProfessor);

                    y.InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas.ForEach(z => {
                        var modelInstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula = InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaAdapter.ToModel(z, true);
                        modelInstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula.InstituicaoCursoOcorrenciaPeriodoProfessor = modelInstituicaoCursoOcorrenciaPeriodoProfessor;

                        this._instituicaoRepository.AddInstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula(modelInstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula);
                    });
                });
            });

            this._instituicaoRepository.SaveChanges();
        }

        public void AddInstituicaoColaborador(long id, InstituicaoColaboradorVM instituicaoColaborador) {
            var model = InstituicaoColaboradorAdapter.ToModel(instituicaoColaborador, true);
            model.Instituicao = this._instituicaoRepository.Get(id);
            this._instituicaoRepository.AddInstituicaoColaborador(model);
            this._instituicaoRepository.SaveChanges();
        }

        public void AddInstituicaoColaboradorPerfil(long id, InstituicaoColaboradorPerfilVM instituicaoColaboradorPerfil) {
            var model = InstituicaoColaboradorPerfilAdapter.ToModel(instituicaoColaboradorPerfil, true);
            model.Instituicao = this._instituicaoRepository.Get(id);
            this._instituicaoRepository.AddInstituicaoColaboradorPerfil(model);
            this._instituicaoRepository.SaveChanges();
        }

        public void Update(InstituicaoVM viewModel) {
            var model = InstituicaoAdapter.ToModel(viewModel, true);
            this._instituicaoRepository.Update(model);

            this._instituicaoRepository.SaveChanges();
        }

        public void UpdateInstituicaoCurso(long id, InstituicaoCursoVM instituicaoCurso) {
            var model = InstituicaoCursoAdapter.ToModel(instituicaoCurso, true);
            model.Instituicao = this._instituicaoRepository.Get(id);
            this._instituicaoRepository.UpdateInstituicaoCurso(model);

            var turmas = InstituicaoCursoAdapter.InstituicaoCursoTurmaFromVM(instituicaoCurso);
            var turmasAttached = this._instituicaoRepository.GetAllInstituicaoCursoTurmaByInstituicaoCurso(model.ID, true);
            turmas.Where(x => !turmasAttached.Select(y => y.ID).Contains(x.ID)).ToList().ForEach(x => {
                x.InstituicaoCurso = model;

                this._instituicaoRepository.AddInstituicaoCursoTurma(x);
            });
            turmasAttached.Where(x => !turmas.Select(y => y.ID).Contains(x.ID)).ToList().ForEach(x => {
                x.InstituicaoCurso = model;

                this._instituicaoRepository.DisableInstituicaoCursoTurma(x.ID);
            });

            var periodos = InstituicaoCursoAdapter.InstituicaoCursoPeriodoFromVM(instituicaoCurso);
            var periodosAttached = this._instituicaoRepository.GetAllInstituicaoCursoPeriodoByInstituicaoCurso(model.ID, true);
            periodos.Where(x => !periodosAttached.Select(y => y.ID).Contains(x.ID)).ToList().ForEach(x => {
                x.InstituicaoCurso = model;

                this._instituicaoRepository.AddInstituicaoCursoPeriodo(x);
            });
            periodosAttached.Where(x => !periodos.Select(y => y.ID).Contains(x.ID)).ToList().ForEach(x => {
                x.InstituicaoCurso = model;

                this._instituicaoRepository.DisableInstituicaoCursoPeriodo(x.ID);
            });

            this._instituicaoRepository.SaveChanges();
        }

        public void UpdateInstituicaoCursoOcorrencia(long id, InstituicaoCursoOcorrenciaVM instituicaoCurso) {
            var model = InstituicaoCursoOcorrenciaAdapter.ToModel(instituicaoCurso, true);
            model.InstituicaoCurso = this._instituicaoRepository.GetInstituicaoCurso(id);
            this._instituicaoRepository.UpdateInstituicaoCursoOcorrencia(model);

            var instituicaoCursoOcorrenciaPeriodosAttached = this._instituicaoRepository.GetAllInstituicaoCursoOcorrenciaPeriodoByInstituicaoCursoOcorrencia(model.ID, true);
            var instituicaoCursoOcorrenciaPeriodosAttachedToRemove = instituicaoCursoOcorrenciaPeriodosAttached.Where(x => !instituicaoCurso.InstituicaoCursoOcorrenciaPeriodos.Select(y => InstituicaoCursoOcorrenciaPeriodoAdapter.ToModel(y, true).ID).Contains(x.ID)).ToList();

            instituicaoCursoOcorrenciaPeriodosAttachedToRemove.ForEach(x => {
                this._instituicaoRepository.DisableInstituicaoCursoOcorrenciaPeriodo(x.ID);
            });

            instituicaoCurso.InstituicaoCursoOcorrenciaPeriodos.ForEach(x => {
                var modelInstituicaoCursoOcorrenciaPeriodo = InstituicaoCursoOcorrenciaPeriodoAdapter.ToModel(x, true);
                modelInstituicaoCursoOcorrenciaPeriodo.InstituicaoCursoOcorrencia = model;

                if (!instituicaoCursoOcorrenciaPeriodosAttached.Select(y => y.ID).Contains(modelInstituicaoCursoOcorrenciaPeriodo.ID)) {
                    this._instituicaoRepository.AddInstituicaoCursoOcorrenciaPeriodo(modelInstituicaoCursoOcorrenciaPeriodo);
                } else {
                    this._instituicaoRepository.UpdateInstituicaoCursoOcorrenciaPeriodo(modelInstituicaoCursoOcorrenciaPeriodo);
                }

                var instituicaoCursoOcorrenciaPeriodoAlunosAttached = this._instituicaoRepository.GetAllInstituicaoCursoOcorrenciaPeriodoAlunoByInstituicaoCursoOcorrenciaPeriodo(modelInstituicaoCursoOcorrenciaPeriodo.ID, true);
                var instituicaoCursoOcorrenciaPeriodoAlunosAttachedToRemove = instituicaoCursoOcorrenciaPeriodoAlunosAttached.Where(y => !x.InstituicaoCursoOcorrenciaPeriodoAlunos.Select(z => InstituicaoCursoOcorrenciaPeriodoAlunoAdapter.ToModel(z, true).ID).Contains(y.ID)).ToList();

                instituicaoCursoOcorrenciaPeriodoAlunosAttachedToRemove.ForEach(y => {
                    this._instituicaoRepository.DisableInstituicaoCursoOcorrenciaPeriodoAluno(y.ID);
                });

                x.InstituicaoCursoOcorrenciaPeriodoAlunos.ForEach(y => {
                    var modelInstituicaoCursoOcorrenciaPeriodoAluno = InstituicaoCursoOcorrenciaPeriodoAlunoAdapter.ToModel(y, true);
                    modelInstituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoOcorrenciaAluno = InstituicaoCursoOcorrenciaAlunoAdapter.ToModel(instituicaoCurso, y.Aluno);
                    modelInstituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoOcorrenciaPeriodo = modelInstituicaoCursoOcorrenciaPeriodo;

                    if (instituicaoCursoOcorrenciaPeriodoAlunosAttached.FirstOrDefault(z => z.ID == modelInstituicaoCursoOcorrenciaPeriodoAluno.ID) == null) {
                        this._instituicaoRepository.AddInstituicaoCursoOcorrenciaPeriodoAluno(modelInstituicaoCursoOcorrenciaPeriodoAluno);
                    } else {
                        this._instituicaoRepository.UpdateInstituicaoCursoOcorrenciaPeriodoAluno(modelInstituicaoCursoOcorrenciaPeriodoAluno);
                    }

                });

                var instituicaoCursoOcorrenciaPeriodoProfessoresAttached = this._instituicaoRepository.GetAllInstituicaoCursoOcorrenciaPeriodoProfessorByInstituicaoCursoOcorrenciaPeriodo(modelInstituicaoCursoOcorrenciaPeriodo.ID, true);
                var instituicaoCursoOcorrenciaPeriodoProfessoresAttachedToRemove = instituicaoCursoOcorrenciaPeriodoProfessoresAttached.Where(y => !x.InstituicaoCursoOcorrenciaPeriodoProfessores.Select(z => InstituicaoCursoOcorrenciaPeriodoProfessorAdapter.ToModel(z, true).ID).Contains(y.ID)).ToList();

                instituicaoCursoOcorrenciaPeriodoProfessoresAttachedToRemove.ForEach(y => {
                    this._instituicaoRepository.DisableInstituicaoCursoOcorrenciaPeriodoProfessor(y.ID);
                });

                x.InstituicaoCursoOcorrenciaPeriodoProfessores.ForEach(y => {
                    var modelInstituicaoCursoOcorrenciaPeriodoProfessor = InstituicaoCursoOcorrenciaPeriodoProfessorAdapter.ToModel(y, true);
                    modelInstituicaoCursoOcorrenciaPeriodoProfessor.InstituicaoCursoOcorrenciaPeriodo = modelInstituicaoCursoOcorrenciaPeriodo;

                    if (!instituicaoCursoOcorrenciaPeriodoProfessoresAttached.Select(z => z.ID).Contains(modelInstituicaoCursoOcorrenciaPeriodoProfessor.ID)) {
                        this._instituicaoRepository.AddInstituicaoCursoOcorrenciaPeriodoProfessor(modelInstituicaoCursoOcorrenciaPeriodoProfessor);
                    } else {
                        this._instituicaoRepository.UpdateInstituicaoCursoOcorrenciaPeriodoProfessor(modelInstituicaoCursoOcorrenciaPeriodoProfessor);
                    }

                    var instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulasAttached = this._instituicaoRepository.GetAllInstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaByInstituicaoCursoOcorrenciaPeriodoProfessor(modelInstituicaoCursoOcorrenciaPeriodoProfessor.ID, true);
                    var instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulasToRemove = instituicaoCursoOcorrenciaPeriodoProfessoresAttached.Where(z => !y.InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas.Select(a => InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaAdapter.ToModel(a, true).ID).Contains(z.ID)).ToList();

                    instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulasToRemove.ForEach(z => {
                        this._instituicaoRepository.DisableInstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula(z.ID);
                    });

                    y.InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas.ForEach(z => {
                        var modelInstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula = InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaAdapter.ToModel(z, true);
                        modelInstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula.InstituicaoCursoOcorrenciaPeriodoProfessor = modelInstituicaoCursoOcorrenciaPeriodoProfessor;

                        if (!instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulasAttached.Select(a => a.ID).Contains(modelInstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula.ID)) {
                            this._instituicaoRepository.AddInstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula(modelInstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula);
                        }
                    });
                });
            });

            this._instituicaoRepository.SaveChanges();
        }

        public void UpdateInstituicaoColaborador(long id, InstituicaoColaboradorVM instituicaoColaborador) {
            var model = InstituicaoColaboradorAdapter.ToModel(instituicaoColaborador, true);
            model.Instituicao = this._instituicaoRepository.Get(id);
            this._instituicaoRepository.UpdateInstituicaoColaborador(model);
            this._instituicaoRepository.SaveChanges();
        }

        public void UpdateInstituicaoColaboradorPerfil(long id, InstituicaoColaboradorPerfilVM instituicaoColaboradorPerfil) {
            var model = InstituicaoColaboradorPerfilAdapter.ToModel(instituicaoColaboradorPerfil, true);
            model.Instituicao = this._instituicaoRepository.Get(id);
            this._instituicaoRepository.UpdateInstituicaoColaboradorPerfil(model);
            this._instituicaoRepository.SaveChanges();
        }

        public void SaveFormulaNotaFinal(string formulaNotaFinal, long idInstituicaoCursoOcorrenciaPeriodoProfessor) {
            var instituicaoCursoOcorrenciaPeriodoProfessor = this._instituicaoRepository.GetInstituicaoCursoOcorrenciaPeriodoProfessor(idInstituicaoCursoOcorrenciaPeriodoProfessor);
            instituicaoCursoOcorrenciaPeriodoProfessor.FormulaNotaFinal = formulaNotaFinal; instituicaoCursoOcorrenciaPeriodoProfessor.FormulaNotaFinal = formulaNotaFinal;
            this._instituicaoRepository.UpdateInstituicaoCursoOcorrenciaPeriodoProfessor(instituicaoCursoOcorrenciaPeriodoProfessor);
            this._instituicaoRepository.SaveChanges();
        }

        public void SaveInstituicaoCursoOcorrenciaNotas(List<InstituicaoCursoOcorrenciaNotaVM> notas, long idInstituicaoCursoOcorrenciaPeriodoProfessor) {

            var attachedNotas = this._instituicaoRepository.GetAllInstituicaoCursoOcorrenciaNotaByInstituicaoCursoOcorrenciaProfessorPeriodo(idInstituicaoCursoOcorrenciaPeriodoProfessor);
            var attachedInstituicaoCursoOcorrenciaPeriodoProfessor = this._instituicaoRepository.GetInstituicaoCursoOcorrenciaPeriodoProfessor(idInstituicaoCursoOcorrenciaPeriodoProfessor);

            var attachedNotasRemove = attachedNotas.Where(y => !notas.Select(x => x.ID).Contains(y.ID)).ToList();

            attachedNotasRemove.ForEach((x) => {
                this._instituicaoRepository.DisableInstituicaoCursoOcorrenciaNota(x.ID);
            });

            notas.ForEach(x => {
                var nota = InstituicaoCursoOcorrenciaNotaAdapter.ToModel(x, true);
                nota.DataLancamento = DateTime.Now;
                nota.InstituicaoCursoOcorrenciaPeriodoProfessor = attachedInstituicaoCursoOcorrenciaPeriodoProfessor;
                if (!attachedNotas.Select(y => y.ID).Contains(x.ID)) {
                    this._instituicaoRepository.AddInstituicaoCursoOcorrenciaNota(nota);
                } else {
                    this._instituicaoRepository.UpdateInstituicaoCursoOcorrenciaNota(nota);
                }
            });

            this._instituicaoRepository.SaveChanges();

        }

        public void SaveInstituicaoCursoOcorrenciaAusencias(List<InstituicaoCursoOcorrenciaAusenciaVM> ausencias, long idInstituicaoCursoOcorrenciaPeriodoProfessor, DateTime dataAusencia) {

            var attachedInstituicaoCursoOcorrenciaPeriodoProfessor = this._instituicaoRepository.GetInstituicaoCursoOcorrenciaPeriodoProfessor(idInstituicaoCursoOcorrenciaPeriodoProfessor);

            var attachedAusencias = this._instituicaoRepository.GetAllInstituicaoCursoOcorrenciaAusenciaByInstituicaoCursoOcorrenciaProfessorPeriodoAndDataAusencia(idInstituicaoCursoOcorrenciaPeriodoProfessor, dataAusencia);
            var attachedAusenciasRemove = attachedAusencias.Where(y => !ausencias.Select(x => x.ID).Contains(y.ID)).ToList();

            attachedAusenciasRemove.ForEach((x) => {
                this._instituicaoRepository.DisableInstituicaoCursoOcorrenciaAusencia(x.ID);
            });

            ausencias.ForEach(x => {
                var ausencia = InstituicaoCursoOcorrenciaAusenciaAdapter.ToModel(x, true);
                ausencia.InstituicaoCursoOcorrenciaPeriodoProfessor = attachedInstituicaoCursoOcorrenciaPeriodoProfessor;
                if (!attachedAusencias.Select(y => y.ID).Contains(x.ID)) {
                    this._instituicaoRepository.AddInstituicaoCursoOcorrenciaAusencia(ausencia);
                }
            });

            this._instituicaoRepository.SaveChanges();

        }

        public void Disable(long id) {
            this._instituicaoRepository.Disable(id);
            this._instituicaoRepository.SaveChanges();
        }

        public void DisableInstituicaoCurso(long id) {
            this._instituicaoRepository.DisableInstituicaoCurso(id);
            this._instituicaoRepository.SaveChanges();
        }

        public void DisableInstituicaoColaborador(long id) {
            this._instituicaoRepository.DisableInstituicaoColaborador(id);
            this._instituicaoRepository.SaveChanges();
        }

        public void DisableInstituicaoColaboradorPerfil(long id) {
            this._instituicaoRepository.DisableInstituicaoColaboradorPerfil(id);
            this._instituicaoRepository.SaveChanges();
        }

        public void DisableInstituicaoCursoOcorrencia(long id) {
            this._instituicaoRepository.DisableInstituicaoCursoOcorrencia(id);
            this._instituicaoRepository.SaveChanges();
        }

        public List<InstituicaoVM> All() {
            return this._instituicaoRepository.GetAll(true).Select(x => InstituicaoAdapter.ToViewModel(x, true)).ToList();
        }

        public List<InstituicaoCursoVM> AllInstituicaoCurso(long id) {
            return this._instituicaoRepository
            .GetAllInstituicaoCursoByInstituicao(id, true)
            .Select(x => InstituicaoCursoAdapter.ToViewModel(x, true))
            .ToList();
        }

        public List<InstituicaoColaboradorVM> AllInstituicaoColaborador(long id) {
            return this._instituicaoRepository
            .GetAllInstituicaoColaboradorByInstituicao(id, true)
            .Select(x => InstituicaoColaboradorAdapter.ToViewModel(x, true))
            .ToList();
        }

        public List<InstituicaoColaboradorPerfilVM> AllInstituicaoColaboradorPerfil(long id) {
            return this._instituicaoRepository
            .GetAllInstituicaoColaboradorPerfilByInstituicao(id, true)
            .Select(x => InstituicaoColaboradorPerfilAdapter.ToViewModel(x, true))
            .ToList();
        }

        public List<InstituicaoColaboradorPerfilVM> AllInstituicaoColaboradorPerfilByUsuario(string id, long idInstituicao) {
            return this._instituicaoRepository
            .GetAllInstituicaoColaboradorPerfilByUsuario(id, idInstituicao, true)
            .Select(x => InstituicaoColaboradorPerfilAdapter.ToViewModel(x, true))
            .ToList();
        }


        public List<InstituicaoColaboradorPerfilVM> AllInstituicaoColaboradorPerfilByUsuario(string id) {
            return this._instituicaoRepository
            .GetAllInstituicaoColaboradorPerfilByUsuario(id, true)
            .Select(x => InstituicaoColaboradorPerfilAdapter.ToViewModel(x, true))
            .ToList();
        }

        public List<InstituicaoVM> AllInstituicaoByUsuario(string id) {
            return this._instituicaoRepository
            .AllInstituicaoByUsuario(id, true)
            .Select(x => InstituicaoAdapter.ToViewModel(x, true))
            .ToList();
        }

        public List<CursoGradeMateriaVM> AllCursoGradeMateria(long id) {
            return this._instituicaoRepository
            .GetAllInstituicaoCursoGradeMateriaByInstituicaoCurso(id, true)
            .Select(x => CursoGradeMateriaAdapter.ToViewModel(x, true))
            .ToList();
        }

        public List<InstituicaoCursoPeriodoVM> AllInstituicaoCursoPeriodo(long id) {
            return this._instituicaoRepository
            .GetAllInstituicaoCursoPeriodoByInstituicaoCurso(id, true)
            .Select(x => InstituicaoCursoPeriodoAdapter.ToViewModel(x, true))
            .ToList();
        }

        public List<InstituicaoCursoTurmaVM> AllInstituicaoCursoTurma(long id) {
            return this._instituicaoRepository
            .GetAllInstituicaoCursoTurmaByInstituicaoCurso(id, true)
            .Select(x => InstituicaoCursoTurmaAdapter.ToViewModel(x, true))
            .ToList();
        }

        public List<InstituicaoCursoOcorrenciaPeriodoAlunoVM> AllInstituicaoCursoOcorrenciaPeriodoAlunoByInstituicaoCursoOCorrenciaPeriodoProfessor(long id) {
            var instituicaoCursoOcorrenciaPeriodoProfessor = this._instituicaoRepository.GetInstituicaoCursoOcorrenciaPeriodoProfessor(id);
            return this._instituicaoRepository.GetAllInstituicaoCursoOcorrenciaPeriodoAlunoByInstituicaoCursoOcorrenciaPeriodo(instituicaoCursoOcorrenciaPeriodoProfessor.InstituicaoCursoOcorrenciaPeriodo.ID, true).Select(x => InstituicaoCursoOcorrenciaPeriodoAlunoAdapter.ToViewModel(x, true)).ToList();
        }

        public List<InstituicaoCursoOcorrenciaPeriodoAlunoVM> AllInstituicaoCursoOcorrenciaPeriodoAlunoByAluno(string id) {
            var instituicaoCursoOcorrenciaPeriodoAlunos = this._instituicaoRepository.GetAllInstituicaoCursoOcorrenciaPeriodoAlunoByAluno(id, true);
            return instituicaoCursoOcorrenciaPeriodoAlunos.Select(x => InstituicaoCursoOcorrenciaPeriodoAlunoAdapter.ToViewModel(x, true)).ToList();
        }

        public List<InstituicaoCursoOcorrenciaPeriodoProfessorVM> AllInstituicaoCursoOcorrenciaPeriodoProfessorByProfessor(string id) {
            var instituicaoCursoOcorrenciaPeriodoProfessores = this._instituicaoRepository.GetAllInstituicaoCursoOcorrenciaPeriodoProfessorByProfessor(id, true);
            return instituicaoCursoOcorrenciaPeriodoProfessores.Select(x => InstituicaoCursoOcorrenciaPeriodoProfessorAdapter.ToViewModel(x, true)).ToList();
        }

        public List<InstituicaoBusinessAulaVM> AllInstituicaoBusinessAulaByProfessor(string id) {
            return this._instituicaoRepository.GetAllInstituicaoCursoOcorrenciaPeriodoProfessorByProfessor(id, true).Select(x => InstituicaoBusinessAdapter.ToInstituicaoBusinessAulaViewModel(x, this._instituicaoRepository.GetInstituicaoCursoOcorrenciaPeriodoSequence(x.InstituicaoCursoOcorrenciaPeriodo.ID))).ToList();
        }

        public List<InstituicaoBusinessAulaDetalheAlunoVM> AllInstituicaoBusinessAulaByAluno(string id, bool emCurso) {
            return this._instituicaoRepository.GetAllInstituicaoCursoOcorrenciaPeriodoProfessorByAluno(id, emCurso, true).Select(x => {
                var sequence = this._instituicaoRepository.GetInstituicaoCursoOcorrenciaPeriodoSequence(x.InstituicaoCursoOcorrenciaPeriodo.ID);
                var ausencias = this._instituicaoRepository.GetAllInstituicaoCursoOcorrenciaAusenciaByInstituicaoCursoOcorrenciaProfessorPeriodo(x.ID).Count;
                var notaFinal = this.GetNotaFinalByAluno(x.ID, id);
                return InstituicaoBusinessAdapter.ToInstituicaoBusinessAulaDetalheAlunoViewModel(x, sequence, ausencias, notaFinal);
            }).ToList();
        }

        public List<InstituicaoBusinessAulaDetalheAlunoVM> AllInstituicaoBusinessAulaByAlunoAndInstituicaoCursoOcorrencia(string idAluno, long idInstituicaoCursoOcorrencia) {
            return this._instituicaoRepository.GetAllInstituicaoCursoOcorrenciaPeriodoProfessorByAlunoAndInstituicaoCursoOcorrencia(idAluno, idInstituicaoCursoOcorrencia, true).Select(x => {
                var sequence = this._instituicaoRepository.GetInstituicaoCursoOcorrenciaPeriodoSequence(x.InstituicaoCursoOcorrenciaPeriodo.ID);
                var ausencias = this._instituicaoRepository.GetAllInstituicaoCursoOcorrenciaAusenciaByInstituicaoCursoOcorrenciaProfessorPeriodo(x.ID).Count;
                var notaFinal = this.GetNotaFinalByAluno(x.ID, idAluno);
                return InstituicaoBusinessAdapter.ToInstituicaoBusinessAulaDetalheAlunoViewModel(x, sequence, ausencias, notaFinal);
            }).ToList();
        }

        public List<InstituicaoCursoOcorrenciaVM> AllInstituicaoCursoOcorrencia(long id) {
            return this._instituicaoRepository.GetAllInstituicaoCursoOcorrenciaByInstituicaoCurso(id, true).Select(x => InstituicaoCursoOcorrenciaAdapter.ToViewModel(x, false)).ToList();
        }

        public List<InstituicaoCursoOcorrenciaNotaVM> AllInstituicaoCursoOcorrenciaNotaByInstituicaoCursoOcorrenciaPeriodoProfessor(long idInstituicaoCursoOcorrenciaPeriodoProfessor) {
            return this._instituicaoRepository.GetAllInstituicaoCursoOcorrenciaNotaByInstituicaoCursoOcorrenciaProfessorPeriodo(idInstituicaoCursoOcorrenciaPeriodoProfessor).Select(x => InstituicaoCursoOcorrenciaNotaAdapter.ToViewModel(x, true)).ToList();
        }

        public List<InstituicaoCursoOcorrenciaAusenciaVM> AllInstituicaoCursoOcorrenciaAusenciaByInstituicaoCursoOcorrenciaPeriodoProfessorAndDataAusencia(long idInstituicaoCursoOcorrenciaPeriodoProfessor, DateTime dataAusencia) {
            return this._instituicaoRepository.GetAllInstituicaoCursoOcorrenciaAusenciaByInstituicaoCursoOcorrenciaProfessorPeriodoAndDataAusencia(idInstituicaoCursoOcorrenciaPeriodoProfessor, dataAusencia).Select(x => InstituicaoCursoOcorrenciaAusenciaAdapter.ToViewModel(x, true)).ToList();
        }

        public InstituicaoVM Detail(long id) {
            var instituicao = this._instituicaoRepository.Get(id);
            var instituicaoVM = InstituicaoAdapter.ToViewModel(instituicao, true);
            return instituicaoVM;
        }

        public InstituicaoCursoVM DetailInstituicaoCurso(long id) {
            var instituicaoCurso = this._instituicaoRepository.GetInstituicaoCurso(id);
            var turmas = this._instituicaoRepository.GetAllInstituicaoCursoTurmaByInstituicaoCurso(id, true);
            var periodos = this._instituicaoRepository.GetAllInstituicaoCursoPeriodoByInstituicaoCurso(id, true);

            var instituicaoCursoVM = InstituicaoCursoAdapter.ToViewModel(instituicaoCurso, false);
            instituicaoCursoVM.Periodos = periodos.Select(x => InstituicaoCursoPeriodoAdapter.ToViewModel(x, true)).ToList();
            instituicaoCursoVM.Turmas = turmas.Select(x => InstituicaoCursoTurmaAdapter.ToViewModel(x, true)).ToList();

            return instituicaoCursoVM;
        }

        public InstituicaoCursoOcorrenciaVM DetailInstituicaoCursoOcorrencia(long id) {
            var instituicaoCursoOcorrencia = this._instituicaoRepository.GetInstituicaoCursoOcorrencia(id);
            var instituicaoCursoOcorrenciaVM = InstituicaoCursoOcorrenciaAdapter.ToViewModel(instituicaoCursoOcorrencia, true);

            var instituicaoCursoOcorrenciaPeriodos = this._instituicaoRepository.GetAllInstituicaoCursoOcorrenciaPeriodoByInstituicaoCursoOcorrencia(id, true);
            instituicaoCursoOcorrenciaVM.InstituicaoCursoOcorrenciaPeriodos = instituicaoCursoOcorrenciaPeriodos.Select(x => {

                var instituicaoCursoOcorrenciaPeriodoVM = InstituicaoCursoOcorrenciaPeriodoAdapter.ToViewModel(x, true);

                var instituicaoCursoOcorrenciaPeriodoAlunos = this._instituicaoRepository.GetAllInstituicaoCursoOcorrenciaPeriodoAlunoByInstituicaoCursoOcorrenciaPeriodo(x.ID, true);
                instituicaoCursoOcorrenciaPeriodoVM.InstituicaoCursoOcorrenciaPeriodoAlunos = instituicaoCursoOcorrenciaPeriodoVM.InstituicaoCursoOcorrenciaPeriodoAlunos = instituicaoCursoOcorrenciaPeriodoAlunos.Select(y => {
                    return InstituicaoCursoOcorrenciaPeriodoAlunoAdapter.ToViewModel(y, true);
                }).ToList();

                var instituicaoCursoOcorrenciaPeriodoProfessores = this._instituicaoRepository.GetAllInstituicaoCursoOcorrenciaPeriodoProfessorByInstituicaoCursoOcorrenciaPeriodo(x.ID, true);
                instituicaoCursoOcorrenciaPeriodoVM.InstituicaoCursoOcorrenciaPeriodoProfessores = instituicaoCursoOcorrenciaPeriodoProfessores.Select(y => {
                    var instituicaoCursoOcorrenciaPeriodoProfessor = InstituicaoCursoOcorrenciaPeriodoProfessorAdapter.ToViewModel(y, true);
                    instituicaoCursoOcorrenciaPeriodoProfessor.InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas = this._instituicaoRepository.GetAllInstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaByInstituicaoCursoOcorrenciaPeriodoProfessor(y.ID, true).Select(z => InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaAdapter.ToViewModel(z, true)).ToList();
                    return instituicaoCursoOcorrenciaPeriodoProfessor;
                }).ToList();

                return instituicaoCursoOcorrenciaPeriodoVM;
            }).ToList();

            return instituicaoCursoOcorrenciaVM;
        }

        public InstituicaoColaboradorVM DetailInstituicaoColaborador(long id) {
            var instituicaoColaborador = this._instituicaoRepository.GetInstituicaoColaborador(id);
            var instituicaoColaboradorVM = InstituicaoColaboradorAdapter.ToViewModel(instituicaoColaborador, true);
            return instituicaoColaboradorVM;
        }

        public InstituicaoColaboradorPerfilVM DetailInstituicaoColaboradorPerfil(long id) {
            var instituicaoColaboradorPerfil = this._instituicaoRepository.GetInstituicaoColaboradorPerfil(id);
            var instituicaoColaboradorPerfilVM = InstituicaoColaboradorPerfilAdapter.ToViewModel(instituicaoColaboradorPerfil, true);
            return instituicaoColaboradorPerfilVM;
        }

        public string GetFormulaNotaFinal(long idInstituicaoCursoOcorrenciaPeriodoProfessor) {
            return this._instituicaoRepository.GetInstituicaoCursoOcorrenciaPeriodoProfessor(idInstituicaoCursoOcorrenciaPeriodoProfessor).FormulaNotaFinal;
        }

        public double? GetNotaFinalByAluno(long idInstituicaoCursoOcorrenciaPeriodoProfessor, string idAluno) {
            var formulaNotaFinalWithValues = this._instituicaoRepository.GetFormulaNotaFinalWithValuesByAluno(idInstituicaoCursoOcorrenciaPeriodoProfessor, idAluno);
            if (formulaNotaFinalWithValues.Length == 0) {
                return null;
            } else {
                return Double.Parse(new Expression(formulaNotaFinalWithValues).Evaluate().ToString());
            }
        }

        public void ValidateInstituicao(InstituicaoVM instituicao) {
            var exception = new BaseException();
            exception.Code = BaseExceptionCode.RESOURCE_REFUSED;
            exception.HttpStatusCode = HttpStatusCode.NotAcceptable;
            exception.Infos = new List<BaseExceptionFieldInfo>();

            if (instituicao.Nome == null || instituicao.Nome.Trim() == "") {
                exception.Infos.Add(new BaseExceptionFieldInfo() {
                    Code = BaseExceptionCode.FIELD_REQUIRED,
                    Field = BaseExceptionField.INSTITUICAO_NOME,
                });
            }

            if (instituicao.CNPJ == null || instituicao.CNPJ.Trim() == "") {
                exception.Infos.Add(new BaseExceptionFieldInfo() {
                    Code = BaseExceptionCode.FIELD_REQUIRED,
                    Field = BaseExceptionField.INSTITUICAO_CNPJ,
                });
            }

            if (instituicao.Nome != null) {
                var instituicaoByCnpj = this._instituicaoRepository.GetByCNPJ(instituicao.CNPJ);
                if (instituicao.CNPJ != null && instituicaoByCnpj != null && instituicaoByCnpj.ID != instituicao.ID) {

                    if (instituicaoByCnpj.CNPJ == instituicao.CNPJ) {
                        exception.Infos.Add(new BaseExceptionFieldInfo() {
                            Code = BaseExceptionCode.REGISTER_WITH_SAME_VALUE_EXISTS,
                            Field = BaseExceptionField.INSTITUICAO_CNPJ,
                            Value = instituicao.CNPJ
                        });
                    }

                }
            }

            if (exception.Infos.Count > 0) {
                throw exception;
            }
        }

        public void ValidateInstituicaoCurso(InstituicaoCursoVM instituicao) {
            var exception = new BaseException();
            exception.Code = BaseExceptionCode.RESOURCE_REFUSED;
            exception.HttpStatusCode = HttpStatusCode.NotAcceptable;
            exception.Infos = new List<BaseExceptionFieldInfo>();

            if (instituicao.Curso == null) {
                exception.Infos.Add(new BaseExceptionFieldInfo() {
                    Code = BaseExceptionCode.FIELD_REQUIRED,
                    Field = BaseExceptionField.INSTITUICAO_CURSO_CURSO,
                });
            }

            if (instituicao.CursoGrade == null) {
                exception.Infos.Add(new BaseExceptionFieldInfo() {
                    Code = BaseExceptionCode.FIELD_REQUIRED,
                    Field = BaseExceptionField.INSTITUICAO_CURSO_CURSO_GRADE,
                });
            }

            if (instituicao.DataInicio == null) {
                exception.Infos.Add(new BaseExceptionFieldInfo() {
                    Code = BaseExceptionCode.FIELD_REQUIRED,
                    Field = BaseExceptionField.INSTITUICAO_CURSO_DATA_INICIO,
                });
            }

            if (instituicao.Periodos != null && instituicao.Periodos.Count > 0) {
                instituicao.Periodos.ForEach(periodo => {

                    if (periodo.DiaSemana.Count == 0) {
                        exception.Infos.Add(new BaseExceptionFieldInfo() {
                            Code = BaseExceptionCode.FIELD_REQUIRED,
                            Field = BaseExceptionField.INSTITUICAO_CURSO_PERIODO_DIA_SEMANA,
                        });
                    }

                    if (periodo.Inicio == null || periodo.Inicio.Trim() == "") {
                        exception.Infos.Add(new BaseExceptionFieldInfo() {
                            Code = BaseExceptionCode.FIELD_REQUIRED,
                            Field = BaseExceptionField.INSTITUICAO_CURSO_PERIODO_INICIO,
                        });
                    } else if (!this.isValidHour(periodo.Inicio)) {
                        exception.Infos.Add(new BaseExceptionFieldInfo() {
                            Code = BaseExceptionCode.FIELD_HOUR_INVALID,
                            Field = BaseExceptionField.INSTITUICAO_CURSO_PERIODO_INICIO,
                            Value = periodo.Inicio
                        });
                    }

                    if (periodo.Fim == null || periodo.Fim.Trim() == "") {
                        exception.Infos.Add(new BaseExceptionFieldInfo() {
                            Code = BaseExceptionCode.FIELD_REQUIRED,
                            Field = BaseExceptionField.INSTITUICAO_CURSO_PERIODO_FIM,
                        });
                    } else if (!this.isValidHour(periodo.Inicio)) {
                        exception.Infos.Add(new BaseExceptionFieldInfo() {
                            Code = BaseExceptionCode.FIELD_HOUR_INVALID,
                            Field = BaseExceptionField.INSTITUICAO_CURSO_PERIODO_FIM,
                            Value = periodo.Inicio
                        });
                    }

                    if (periodo.PausaInicio != null && periodo.PausaInicio.Trim() != "") {
                        if (!this.isValidHour(periodo.PausaInicio)) {
                            exception.Infos.Add(new BaseExceptionFieldInfo() {
                                Code = BaseExceptionCode.FIELD_HOUR_INVALID,
                                Field = BaseExceptionField.INSTITUICAO_CURSO_PERIODO_PAUSA_INICIO,
                                Value = periodo.Inicio
                            });
                        }
                        if (periodo.PausaFim == null || periodo.PausaFim.Trim() == "") {
                            exception.Infos.Add(new BaseExceptionFieldInfo() {
                                Code = BaseExceptionCode.FIELD_REQUIRED,
                                Field = BaseExceptionField.INSTITUICAO_CURSO_PERIODO_PAUSA_FIM,
                            });
                        } else if (!this.isValidHour(periodo.PausaFim)) {
                            exception.Infos.Add(new BaseExceptionFieldInfo() {
                                Code = BaseExceptionCode.FIELD_HOUR_INVALID,
                                Field = BaseExceptionField.INSTITUICAO_CURSO_PERIODO_PAUSA_FIM,
                                Value = periodo.Inicio
                            });
                        }
                    }

                    if ((periodo.Inicio != null && periodo.Inicio.Trim() != "" && this.isValidHour(periodo.Inicio)) &&
                        (periodo.Fim != null && periodo.Fim.Trim() != "" && this.isValidHour(periodo.Fim))) {

                        var baseExceptionFieldInfoInicio = new BaseExceptionFieldInfo() {
                            Code = BaseExceptionCode.FIELD_HOUR_LESS_THAN,
                            Field = BaseExceptionField.INSTITUICAO_CURSO_PERIODO_INICIO,
                            Value = periodo.Inicio,
                            References = new List<BaseExceptionFieldInfo>()
                        };

                        if ((periodo.PausaInicio != null && periodo.PausaInicio.Trim() != "" && this.isValidHour(periodo.PausaInicio)) && (periodo.PausaFim != null && periodo.PausaFim.Trim() != "" && this.isValidHour(periodo.PausaFim))) {

                            if (TimeSpan.Parse(periodo.Inicio) >= TimeSpan.Parse(periodo.PausaInicio)) {
                                baseExceptionFieldInfoInicio.References.Add(new BaseExceptionFieldInfo() {
                                    Field = BaseExceptionField.INSTITUICAO_CURSO_PERIODO_PAUSA_INICIO,
                                    Value = periodo.PausaInicio
                                });

                                if (!exception.Infos.Contains(baseExceptionFieldInfoInicio)) {
                                    exception.Infos.Add(baseExceptionFieldInfoInicio);
                                }
                            }
                            if (TimeSpan.Parse(periodo.Inicio) >= TimeSpan.Parse(periodo.PausaFim)) {
                                baseExceptionFieldInfoInicio.References.Add(new BaseExceptionFieldInfo() {
                                    Field = BaseExceptionField.INSTITUICAO_CURSO_PERIODO_PAUSA_FIM,
                                    Value = periodo.PausaFim
                                });

                                if (!exception.Infos.Contains(baseExceptionFieldInfoInicio)) {
                                    exception.Infos.Add(baseExceptionFieldInfoInicio);
                                }
                            }

                            if ((TimeSpan.Parse(periodo.PausaInicio) >= TimeSpan.Parse(periodo.PausaFim)) ||
                                (TimeSpan.Parse(periodo.PausaInicio) >= TimeSpan.Parse(periodo.Fim))) {

                                var baseExceptionFieldInfo = new BaseExceptionFieldInfo() {
                                    Code = BaseExceptionCode.FIELD_HOUR_LESS_THAN,
                                    Field = BaseExceptionField.INSTITUICAO_CURSO_PERIODO_PAUSA_INICIO,
                                    Value = periodo.PausaInicio,
                                    References = new List<BaseExceptionFieldInfo>()
                                };

                                if (TimeSpan.Parse(periodo.PausaInicio) >= TimeSpan.Parse(periodo.PausaFim)) {
                                    baseExceptionFieldInfo.References.Add(new BaseExceptionFieldInfo() {
                                        Field = BaseExceptionField.INSTITUICAO_CURSO_PERIODO_PAUSA_FIM,
                                        Value = periodo.PausaFim
                                    });
                                }

                                if (TimeSpan.Parse(periodo.PausaInicio) >= TimeSpan.Parse(periodo.Fim)) {
                                    baseExceptionFieldInfo.References.Add(new BaseExceptionFieldInfo() {
                                        Field = BaseExceptionField.INSTITUICAO_CURSO_PERIODO_FIM,
                                        Value = periodo.Fim
                                    });
                                }

                                exception.Infos.Add(baseExceptionFieldInfo);

                            };

                            if (TimeSpan.Parse(periodo.PausaFim) >= TimeSpan.Parse(periodo.Fim)) {

                                var baseExceptionFieldInfo = new BaseExceptionFieldInfo() {
                                    Code = BaseExceptionCode.FIELD_HOUR_LESS_THAN,
                                    Field = BaseExceptionField.INSTITUICAO_CURSO_PERIODO_PAUSA_FIM,
                                    Value = periodo.PausaFim,
                                    References = new List<BaseExceptionFieldInfo>(){
                                        new BaseExceptionFieldInfo() {
                                            Field = BaseExceptionField.INSTITUICAO_CURSO_PERIODO_FIM,
                                            Value = periodo.Fim
                                        }
                                    }
                                };

                                exception.Infos.Add(baseExceptionFieldInfo);

                            };



                        }

                        if (TimeSpan.Parse(periodo.Inicio) >= TimeSpan.Parse(periodo.Fim)) {
                            baseExceptionFieldInfoInicio.References.Add(new BaseExceptionFieldInfo() {
                                Field = BaseExceptionField.INSTITUICAO_CURSO_PERIODO_FIM,
                                Value = periodo.Fim
                            });

                            if (!exception.Infos.Contains(baseExceptionFieldInfoInicio)) {
                                exception.Infos.Add(baseExceptionFieldInfoInicio);
                            }
                        }

                    }

                });
            }

            if (instituicao.Turmas != null && instituicao.Turmas.Count > 0) {
                instituicao.Turmas.ForEach(turma => {

                    if (turma.Nome == null || turma.Nome == "") {
                        exception.Infos.Add(new BaseExceptionFieldInfo() {
                            Code = BaseExceptionCode.FIELD_REQUIRED,
                            Field = BaseExceptionField.INSTITUICAO_CURSO_TURMA_NOME,
                        });
                    }

                });
            }

            if (exception.Infos.Count > 0) {
                throw exception;
            }
        }

        public void ValidateInstituicaoCursoOcorrencia(InstituicaoCursoOcorrenciaVM instituicaoCursoOcorrencia) {
            var exception = new BaseException();
            exception.Code = BaseExceptionCode.RESOURCE_REFUSED;
            exception.HttpStatusCode = HttpStatusCode.NotAcceptable;
            exception.Infos = new List<BaseExceptionFieldInfo>();

            if (instituicaoCursoOcorrencia.Coordenador == null) {
                exception.Infos.Add(new BaseExceptionFieldInfo() {
                    Code = BaseExceptionCode.FIELD_REQUIRED,
                    Field = BaseExceptionField.INSTITUICAO_CURSO_OCORRENCIA_COORDENADOR,
                });
            }

            if (instituicaoCursoOcorrencia.DataInicio == null) {
                exception.Infos.Add(new BaseExceptionFieldInfo() {
                    Code = BaseExceptionCode.FIELD_REQUIRED,
                    Field = BaseExceptionField.INSTITUICAO_CURSO_OCORRENCIA_DATA_INICIO,
                });
            }

            if (instituicaoCursoOcorrencia.DataExpiracao == null) {
                exception.Infos.Add(new BaseExceptionFieldInfo() {
                    Code = BaseExceptionCode.FIELD_REQUIRED,
                    Field = BaseExceptionField.INSTITUICAO_CURSO_OCORRENCIA_DATA_EXPIRACAO,
                });
            }

            if (instituicaoCursoOcorrencia.InstituicaoCursoOcorrenciaPeriodos != null && instituicaoCursoOcorrencia.InstituicaoCursoOcorrenciaPeriodos.Count > 0) {
                instituicaoCursoOcorrencia.InstituicaoCursoOcorrenciaPeriodos.ForEach(instituicaoCursoOcorrenciaPeriodo => {

                    if (instituicaoCursoOcorrenciaPeriodo.DataInicio == null) {
                        exception.Infos.Add(new BaseExceptionFieldInfo() {
                            Code = BaseExceptionCode.FIELD_REQUIRED,
                            Field = BaseExceptionField.INSTITUICAO_CURSO_OCORRENCIA_PERIODO_DATA_INICIO,
                        });
                    } else {

                        if (instituicaoCursoOcorrencia.DataInicio != null && (instituicaoCursoOcorrenciaPeriodo.DataInicio < instituicaoCursoOcorrencia.DataInicio)) {
                            exception.Infos.Add(new BaseExceptionFieldInfo() {
                                Code = BaseExceptionCode.FIELD_DATE_LESS_THAN,
                                Field = BaseExceptionField.INSTITUICAO_CURSO_OCORRENCIA_PERIODO_DATA_INICIO,
                                Value = instituicaoCursoOcorrenciaPeriodo.DataInicio.Value.Date.ToString("o"),
                                ValueType = BaseExceptionFieldInfoValueType.DATE,
                                References = new List<BaseExceptionFieldInfo>(){
                                    new BaseExceptionFieldInfo() {
                                        Code = BaseExceptionCode.FIELD_DATE_LESS_THAN,
                                        Field = BaseExceptionField.INSTITUICAO_CURSO_OCORRENCIA_DATA_INICIO,
                                        Value = instituicaoCursoOcorrencia.DataInicio.Value.Date.ToString("o"),
                                        ValueType = BaseExceptionFieldInfoValueType.DATE,
                                    }
                                }
                            });
                        }

                        var baseExceptionFieldInfo = new BaseExceptionFieldInfo() {
                            Code = BaseExceptionCode.FIELD_DATE_GREATER_THAN,
                            Field = BaseExceptionField.INSTITUICAO_CURSO_OCORRENCIA_PERIODO_DATA_INICIO,
                            Value = instituicaoCursoOcorrenciaPeriodo.DataInicio.Value.Date.ToString("o"),
                            ValueType = BaseExceptionFieldInfoValueType.DATE,
                            References = new List<BaseExceptionFieldInfo>()
                        };

                        if (instituicaoCursoOcorrenciaPeriodo.DataExpiracao != null && (instituicaoCursoOcorrenciaPeriodo.DataInicio > instituicaoCursoOcorrenciaPeriodo.DataExpiracao)) {
                            baseExceptionFieldInfo.References.Add(new BaseExceptionFieldInfo() {
                                Code = BaseExceptionCode.FIELD_DATE_GREATER_THAN,
                                Field = BaseExceptionField.INSTITUICAO_CURSO_OCORRENCIA_PERIODO_DATA_EXPIRACAO,
                                Value = instituicaoCursoOcorrenciaPeriodo.DataExpiracao.Value.Date.ToString("o"),
                                ValueType = BaseExceptionFieldInfoValueType.DATE,
                            });

                            if (!exception.Infos.Contains(baseExceptionFieldInfo)) {
                                exception.Infos.Add(baseExceptionFieldInfo);
                            }
                        }

                        if (instituicaoCursoOcorrencia.DataExpiracao != null && (instituicaoCursoOcorrenciaPeriodo.DataInicio > instituicaoCursoOcorrencia.DataExpiracao)) {
                            baseExceptionFieldInfo.References.Add(new BaseExceptionFieldInfo() {
                                Code = BaseExceptionCode.FIELD_DATE_GREATER_THAN,
                                Field = BaseExceptionField.INSTITUICAO_CURSO_OCORRENCIA_DATA_EXPIRACAO,
                                Value = instituicaoCursoOcorrencia.DataExpiracao.Value.Date.ToString("o"),
                                ValueType = BaseExceptionFieldInfoValueType.DATE,
                            });

                            if (!exception.Infos.Contains(baseExceptionFieldInfo)) {
                                exception.Infos.Add(baseExceptionFieldInfo);
                            }
                        }

                    }

                    if (instituicaoCursoOcorrenciaPeriodo.DataExpiracao == null) {
                        exception.Infos.Add(new BaseExceptionFieldInfo() {
                            Code = BaseExceptionCode.FIELD_REQUIRED,
                            Field = BaseExceptionField.INSTITUICAO_CURSO_OCORRENCIA_PERIODO_DATA_EXPIRACAO,
                        });
                    } else {

                        if (instituicaoCursoOcorrencia.DataExpiracao != null && (instituicaoCursoOcorrenciaPeriodo.DataExpiracao > instituicaoCursoOcorrencia.DataExpiracao)) {
                            exception.Infos.Add(new BaseExceptionFieldInfo() {
                                Code = BaseExceptionCode.FIELD_DATE_GREATER_THAN,
                                Field = BaseExceptionField.INSTITUICAO_CURSO_OCORRENCIA_PERIODO_DATA_EXPIRACAO,
                                Value = instituicaoCursoOcorrenciaPeriodo.DataExpiracao.Value.Date.ToString("o"),
                                ValueType = BaseExceptionFieldInfoValueType.DATE,
                                References = new List<BaseExceptionFieldInfo>(){
                                    new BaseExceptionFieldInfo() {
                                        Code = BaseExceptionCode.FIELD_DATE_GREATER_THAN,
                                        Field = BaseExceptionField.INSTITUICAO_CURSO_OCORRENCIA_DATA_EXPIRACAO,
                                        Value = instituicaoCursoOcorrencia.DataExpiracao.Value.Date.ToString("o"),
                                        ValueType = BaseExceptionFieldInfoValueType.DATE,
                                    }
                                }
                            });
                        }

                        var baseExceptionFieldInfo = new BaseExceptionFieldInfo() {
                            Code = BaseExceptionCode.FIELD_DATE_LESS_THAN,
                            Field = BaseExceptionField.INSTITUICAO_CURSO_OCORRENCIA_PERIODO_DATA_EXPIRACAO,
                            Value = instituicaoCursoOcorrenciaPeriodo.DataExpiracao.Value.Date.ToString("o"),
                            ValueType = BaseExceptionFieldInfoValueType.DATE,
                            References = new List<BaseExceptionFieldInfo>()
                        };

                        if (instituicaoCursoOcorrenciaPeriodo.DataInicio != null && (instituicaoCursoOcorrenciaPeriodo.DataExpiracao < instituicaoCursoOcorrenciaPeriodo.DataInicio)) {
                            baseExceptionFieldInfo.References.Add(new BaseExceptionFieldInfo() {
                                Code = BaseExceptionCode.FIELD_DATE_LESS_THAN,
                                Field = BaseExceptionField.INSTITUICAO_CURSO_OCORRENCIA_PERIODO_DATA_INICIO,
                                Value = instituicaoCursoOcorrenciaPeriodo.DataInicio.Value.Date.ToString("o"),
                                ValueType = BaseExceptionFieldInfoValueType.DATE,
                            });

                            if (!exception.Infos.Contains(baseExceptionFieldInfo)) {
                                exception.Infos.Add(baseExceptionFieldInfo);
                            }
                        }

                        if (instituicaoCursoOcorrencia.DataInicio != null && (instituicaoCursoOcorrenciaPeriodo.DataExpiracao < instituicaoCursoOcorrencia.DataInicio)) {
                            baseExceptionFieldInfo.References.Add(new BaseExceptionFieldInfo() {
                                Code = BaseExceptionCode.FIELD_DATE_LESS_THAN,
                                Field = BaseExceptionField.INSTITUICAO_CURSO_OCORRENCIA_DATA_INICIO,
                                Value = instituicaoCursoOcorrencia.DataInicio.Value.Date.ToString("o"),
                                ValueType = BaseExceptionFieldInfoValueType.DATE,
                            });

                            if (!exception.Infos.Contains(baseExceptionFieldInfo)) {
                                exception.Infos.Add(baseExceptionFieldInfo);
                            }
                        }

                    }

                    if (instituicaoCursoOcorrenciaPeriodo.InstituicaoCursoOcorrenciaPeriodoAlunos != null && instituicaoCursoOcorrenciaPeriodo.InstituicaoCursoOcorrenciaPeriodoAlunos.Count > 0) {
                        instituicaoCursoOcorrenciaPeriodo.InstituicaoCursoOcorrenciaPeriodoAlunos.ForEach(instituicaoCursoOcorrenciaPeriodoAluno => {

                            if (instituicaoCursoOcorrenciaPeriodoAluno.Aluno == null) {
                                exception.Infos.Add(new BaseExceptionFieldInfo() {
                                    Code = BaseExceptionCode.FIELD_REQUIRED,
                                    Field = BaseExceptionField.INSTITUICAO_CURSO_OCORRENCIA_PERIODO_ALUNO_ALUNO,
                                });
                            }

                            if (instituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoPeriodo == null) {
                                exception.Infos.Add(new BaseExceptionFieldInfo() {
                                    Code = BaseExceptionCode.FIELD_REQUIRED,
                                    Field = BaseExceptionField.INSTITUICAO_CURSO_OCORRENCIA_PERIODO_ALUNO_INSTITUICAO_CURSO_PERIODO,
                                });
                            }

                            if (instituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoTurma == null) {
                                exception.Infos.Add(new BaseExceptionFieldInfo() {
                                    Code = BaseExceptionCode.FIELD_REQUIRED,
                                    Field = BaseExceptionField.INSTITUICAO_CURSO_OCORRENCIA_PERIODO_ALUNO_INSTITUICAO_CURSO_TURMA,
                                });
                            }

                        });
                    }

                    if (instituicaoCursoOcorrenciaPeriodo.InstituicaoCursoOcorrenciaPeriodoProfessores != null && instituicaoCursoOcorrenciaPeriodo.InstituicaoCursoOcorrenciaPeriodoProfessores.Count > 0) {
                        instituicaoCursoOcorrenciaPeriodo.InstituicaoCursoOcorrenciaPeriodoProfessores.ForEach(instituicaoCursoOcorrenciaPeriodoProfessor => {

                            if (instituicaoCursoOcorrenciaPeriodoProfessor.Professor == null) {
                                exception.Infos.Add(new BaseExceptionFieldInfo() {
                                    Code = BaseExceptionCode.FIELD_REQUIRED,
                                    Field = BaseExceptionField.INSTITUICAO_CURSO_OCORRENCIA_PERIODO_PROFESSOR_PROFESSOR,
                                });
                            }

                            if (instituicaoCursoOcorrenciaPeriodoProfessor.InstituicaoCursoPeriodo == null) {
                                exception.Infos.Add(new BaseExceptionFieldInfo() {
                                    Code = BaseExceptionCode.FIELD_REQUIRED,
                                    Field = BaseExceptionField.INSTITUICAO_CURSO_OCORRENCIA_PERIODO_PROFESSOR_INSTITUICAO_CURSO_PERIODO,
                                });
                            }

                            if (instituicaoCursoOcorrenciaPeriodoProfessor.InstituicaoCursoTurma == null) {
                                exception.Infos.Add(new BaseExceptionFieldInfo() {
                                    Code = BaseExceptionCode.FIELD_REQUIRED,
                                    Field = BaseExceptionField.INSTITUICAO_CURSO_OCORRENCIA_PERIODO_PROFESSOR_INSTITUICAO_CURSO_TURMA,
                                });
                            }

                            if (instituicaoCursoOcorrenciaPeriodoProfessor.CursoGradeMateria == null) {
                                exception.Infos.Add(new BaseExceptionFieldInfo() {
                                    Code = BaseExceptionCode.FIELD_REQUIRED,
                                    Field = BaseExceptionField.INSTITUICAO_CURSO_OCORRENCIA_PERIODO_PROFESSOR_CURSO_GRADE_MATERIA,
                                });
                            }

                            if (instituicaoCursoOcorrenciaPeriodoProfessor.InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas == null || instituicaoCursoOcorrenciaPeriodoProfessor.InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas.Count > 0) {

                                instituicaoCursoOcorrenciaPeriodoProfessor.InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulas.ForEach(instituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula => {

                                });

                            }

                        });
                    }

                });
            }

            if (exception.Infos.Count > 0) {
                throw exception;
            }
        }

        public void ValidateInstituicaoColaboradorPerfil(InstituicaoColaboradorPerfilVM instituicaoColaboradorPerfil) {
            var exception = new BaseException();
            exception.Code = BaseExceptionCode.RESOURCE_REFUSED;
            exception.HttpStatusCode = HttpStatusCode.NotAcceptable;
            exception.Infos = new List<BaseExceptionFieldInfo>();

            if (instituicaoColaboradorPerfil.Nome == null || instituicaoColaboradorPerfil.Nome.Trim() == "") {
                exception.Infos.Add(new BaseExceptionFieldInfo() {
                    Code = BaseExceptionCode.FIELD_REQUIRED,
                    Field = BaseExceptionField.INSTITUICAO_COLABORADOR_PERFIL_NOME,
                });
            }

            if (instituicaoColaboradorPerfil.Roles == null || instituicaoColaboradorPerfil.Roles.Count == 0) {
                exception.Infos.Add(new BaseExceptionFieldInfo() {
                    Code = BaseExceptionCode.FIELD_LIST_LESS_THAN,
                    Field = BaseExceptionField.INSTITUICAO_COLABORADOR_PERFIL_ROLES,
                    References = new List<BaseExceptionFieldInfo>(){
                        new BaseExceptionFieldInfo(){
                            Value="1"
                        }
                    }
                });
            }

            if (exception.Infos.Count > 0) {
                throw exception;
            }
        }

        public void ValidateInstituicaoColaborador(InstituicaoColaboradorVM instituicaoColaborador) {
            var exception = new BaseException();
            exception.Code = BaseExceptionCode.RESOURCE_REFUSED;
            exception.HttpStatusCode = HttpStatusCode.NotAcceptable;
            exception.Infos = new List<BaseExceptionFieldInfo>();

            if (instituicaoColaborador.Usuario == null) {
                exception.Infos.Add(new BaseExceptionFieldInfo() {
                    Code = BaseExceptionCode.FIELD_REQUIRED,
                    Field = BaseExceptionField.INSTITUICAO_COLABORADOR_USUARIO,
                });
            }

            if (instituicaoColaborador.Perfis == null || instituicaoColaborador.Perfis.Count == 0) {
                exception.Infos.Add(new BaseExceptionFieldInfo() {
                    Code = BaseExceptionCode.FIELD_LIST_LESS_THAN,
                    Field = BaseExceptionField.INSTITUICAO_COLABORADOR_PERFIS,
                    References = new List<BaseExceptionFieldInfo>(){
                        new BaseExceptionFieldInfo(){
                            Value="1"
                        }
                    }
                });
            }

            if (exception.Infos.Count > 0) {
                throw exception;
            }
        }

        public bool isValidHour(string time) {
            var timeSpanResult = new TimeSpan();
            return TimeSpan.TryParse(time, System.Globalization.CultureInfo.InvariantCulture, out timeSpanResult);
        }

    }
}