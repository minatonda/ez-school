using System;
using System.Collections.Generic;
using System.Linq;
using Api.CursoApi;
using Domain.CursoDomain;
using Domain.InstituicaoDomain;

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

        public void AddInstituicaoCursoOcorrencia(long id, InstituicaoCursoOcorrenciaVM instituicaoCurso) {
            var model = InstituicaoCursoOcorrenciaAdapter.ToModel(instituicaoCurso, true);
            model.InstituicaoCurso = this._instituicaoRepository.GetInstituicaoCurso(id);
            model.DataInicio = model.DataInicio.HasValue ? model.DataInicio : DateTime.Now;
            this._instituicaoRepository.AddInstituicaoCursoOcorrencia(model);

            instituicaoCurso.InstituicaoCursoOcorrenciaPeriodos.ForEach(x => {
                var modelInstituicaoCursoOcorrenciaPeriodo = InstituicaoCursoOcorrenciaPeriodoAdapter.ToModel(x, true);
                modelInstituicaoCursoOcorrenciaPeriodo.InstituicaoCursoOcorrencia = model;

                this._instituicaoRepository.AddInstituicaoCursoOcorrenciaPeriodo(modelInstituicaoCursoOcorrenciaPeriodo);

                x.InstituicaoCursoOcorrenciaPeriodoAlunos.ForEach(y => {
                    var modelInstituicaoCursoOcorrenciaPeriodoAluno = InstituicaoCursoOcorrenciaPeriodoAlunoAdapter.ToModel(y, true);
                    modelInstituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoOcorrenciaAluno = InstituicaoCursoOcorrenciaAlunoAdapter.ToModel(instituicaoCurso, y.Aluno);
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

                    if (instituicaoCursoOcorrenciaPeriodoAlunosAttached.FirstOrDefault(z => z.InstituicaoCursoOcorrenciaAluno.Aluno.ID == modelInstituicaoCursoOcorrenciaPeriodoAluno.InstituicaoCursoOcorrenciaAluno.Aluno.ID) == null) {
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

        public void Disable(long id) {
            this._instituicaoRepository.Disable(id);
            this._instituicaoRepository.SaveChanges();
        }

        public void DisableInstituicaoCurso(long id) {
            this._instituicaoRepository.DisableInstituicaoCurso(id);
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
            .Select(x => InstituicaoCursoAdapter.ToViewModel(x, false))
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

        public InstituicaoVM Detail(long id) {
            var instituicao = this._instituicaoRepository.Get(id);
            var instituicaoVM = InstituicaoAdapter.ToViewModel(instituicao, true);
            return instituicaoVM;
        }

        public InstituicaoCursoVM DetailInstituicaoCurso(long id) {
            var instituicaoCurso = this._instituicaoRepository.GetInstituicaoCurso(id);
            var turmas = this._instituicaoRepository.GetAllInstituicaoCursoTurmaByInstituicaoCurso(id, true);
            var periodos = this._instituicaoRepository.GetAllInstituicaoCursoPeriodoByInstituicaoCurso(id, true);

            var instituicaoCursoVM = InstituicaoCursoAdapter.ToViewModel(this._instituicaoRepository.GetInstituicaoCurso(id), false);
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

        public List<InstituicaoCursoOcorrenciaVM> AllInstituicaoCursoOcorrencia(long id) {
            return this._instituicaoRepository.GetAllInstituicaoCursoOcorrenciaByInstituicaoCurso(id, true).Select(x => InstituicaoCursoOcorrenciaAdapter.ToViewModel(x, false)).ToList();
        }


    }
}