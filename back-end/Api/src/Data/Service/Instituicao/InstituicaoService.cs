using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Data.ViewModels;
using Domain.Models;
using Domain.Models.Interface;
using Domain.Repositories;

namespace Api.Data.Service {
    public class InstituicaoService {

        private InstituicaoRepository _instituicaoRepository;

        private CursoRepository _cursoRespository;

        public InstituicaoService(InstituicaoRepository instituicaoRepository, CursoRepository cursoRespository) {
            this._instituicaoRepository = instituicaoRepository;
            this._cursoRespository = cursoRespository;
        }

        public InstituicaoVM Add(InstituicaoVM viewModel) {
            var model = InstituicaoAdapter.ToModel(viewModel, true);
            return InstituicaoAdapter.ToViewModel(this._instituicaoRepository.Add(model), true);
        }

        public InstituicaoVM Update(InstituicaoVM viewModel) {
            var model = InstituicaoAdapter.ToModel(viewModel, true);
            return InstituicaoAdapter.ToViewModel(this._instituicaoRepository.Update(model), true);
        }

        public void Disable(long id) {
            this._instituicaoRepository.Disable(id);
        }

        public InstituicaoVM Detail(long id) {
            return InstituicaoAdapter.ToViewModel(this._instituicaoRepository.Get(id), true);
        }

        public List<InstituicaoVM> All() {
            return this._instituicaoRepository.GetAll(true).Select(x => InstituicaoAdapter.ToViewModel(x, true)).ToList();
        }

        public void AddCurso(long id, InstituicaoCursoVM instituicaoCurso) {
            var model = InstituicaoCursoAdapter.ToModel(instituicaoCurso, true);
            var periodos = InstituicaoCursoAdapter.InstituicaoCursoPeriodoFromVM(instituicaoCurso);
            var turmas = InstituicaoCursoAdapter.InstituicaoCursoTurmaFromVM(instituicaoCurso);
            this._instituicaoRepository.AddCurso(id, model, periodos, turmas);
        }

        public void UpdateCurso(long id, InstituicaoCursoVM instituicaoCurso) {
            var model = InstituicaoCursoAdapter.ToModel(instituicaoCurso, true);
            var periodos = InstituicaoCursoAdapter.InstituicaoCursoPeriodoFromVM(instituicaoCurso);
            var turmas = InstituicaoCursoAdapter.InstituicaoCursoTurmaFromVM(instituicaoCurso);
            this._instituicaoRepository.UpdateCurso(id, model, periodos, turmas);
        }

        public void DisableCurso(long id, long idCurso) {
            this._instituicaoRepository.DisableCurso(id, idCurso);
        }

        public InstituicaoCursoVM DetailCurso(long id, long idCurso) {
            var _turmas = this._instituicaoRepository.GetCursoTurmas(id, idCurso);
            var _periodos = this._instituicaoRepository.GetCursoPeriodos(id, idCurso);
            return InstituicaoCursoAdapter.ToViewModel(this._instituicaoRepository.GetCurso(id, idCurso), _periodos, _turmas, false);
        }

        public List<InstituicaoCursoVM> AllCurso(long id) {
            return this._instituicaoRepository.GetCursos(id).Select(x => InstituicaoCursoAdapter.ToViewModel(x, null, null, false)).ToList();
        }

        public List<CursoGradeMateriaVM> AllCursoGradeMateria(long id, long idCurso) {
            return this._instituicaoRepository.GetCursoGradeMaterias(id, idCurso).Select(x => CursoGradeMateriaAdapter.ToViewModel(x, true)).ToList();
        }

        public List<InstituicaoCursoPeriodoVM> AllPeriodo(long id, long idCurso) {
            return this.DetailCurso(id, idCurso).Periodos;
        }

        public List<InstituicaoCursoTurmaVM> AllTurma(long id, long idCurso) {
            return this.DetailCurso(id, idCurso).Turmas;
        }

        public InstituicaoCursoOcorrenciaVM AddCursoOcorrencia(long id, long idCurso, InstituicaoCursoOcorrenciaVM instituicaoCurso) {
            var _model = InstituicaoCursoOcorrenciaAdapter.ToModel(instituicaoCurso, true);
            var _alunos = InstituicaoCursoOcorrenciaAdapter.InstituicaoCursoOcorrenciaAlunosFrom(instituicaoCurso);
            //var _professores = InstituicaoCursoOcorrenciaAdapter.InstituicaoCursoOcorrenciaProfessoresFrom(instituicaoCurso);
            this._instituicaoRepository.AddCursoOcorrencia(id, idCurso, _model, _alunos);
            return InstituicaoCursoOcorrenciaAdapter.ToViewModel(_model, _alunos, true);
        }

        public InstituicaoCursoOcorrenciaVM DetailCursoOcorrencia(long id, long idCurso, string dataInicio) {
            var _dataInicio = DateTime.ParseExact(dataInicio, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var _instituicaoCursoOcorrencia = this._instituicaoRepository.GetCursoOcorrencia(id, idCurso, _dataInicio);
            var _alunos = this._instituicaoRepository.GetCursoOcorrenciaAlunos(_instituicaoCursoOcorrencia.ID);
            //var _professores = this._instituicaoRepository.GetCursoOcorrenciaProfessores(_instituicaoCursoOcorrencia.ID).ToDictionary(x => x, x => this._instituicaoRepository.GetCursoOcorrenciaProfessorPeriodoAula(x.ID));
            return InstituicaoCursoOcorrenciaAdapter.ToViewModel(_instituicaoCursoOcorrencia, _alunos, true);
        }

        public List<InstituicaoCursoOcorrenciaVM> AllCursoOcorrencia(long id, long idCurso) {
            return this._instituicaoRepository.GetCursoOcorrencias(id, idCurso).Select(x => InstituicaoCursoOcorrenciaAdapter.ToViewModel(x, null, false)).ToList();
        }

        public List<InstituicaoCursoOcorrenciaProfessorPeriodoAulaVM> AllPeriodoAulaDisponivel(long id, long idCurso, long idPeriodo) {
            var _list = new List<InstituicaoCursoOcorrenciaProfessorPeriodoAulaVM>();

            var _periodo = this._instituicaoRepository.GetCursoPeriodos(id, idCurso).SingleOrDefault(x => x.ID == idPeriodo);
            var _dateTimeInicio = DateTime.ParseExact(_periodo.Inicio, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            var _dateTimeFim = DateTime.ParseExact(_periodo.Fim, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            var _minutos = (_dateTimeFim - _dateTimeInicio).TotalMinutes;

            var _dateTimePausaInicio = DateTime.ParseExact(_periodo.PausaInicio, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            var _dateTimePausaFim = DateTime.ParseExact(_periodo.PausaFim, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            var _minutosPausa = (_dateTimePausaFim - _dateTimePausaInicio).TotalMinutes;
            _minutos -= _minutosPausa;

            while (_dateTimeInicio <= _dateTimePausaInicio) {
                var _inicio = _dateTimeInicio.ToString("HH:mm");
                _dateTimeInicio = _dateTimeInicio.AddMinutes((_minutos / _periodo.Quebras));
                var _fim = _dateTimeInicio.ToString("HH:mm");
                if (_periodo.Dom) {
                    _list.Add(new InstituicaoCursoOcorrenciaProfessorPeriodoAulaVM() { Inicio = _inicio, Fim = _fim, Dia = DayOfWeek.Sunday, Label = (_inicio + " - " + _fim) });
                }
                if (_periodo.Seg) {
                    _list.Add(new InstituicaoCursoOcorrenciaProfessorPeriodoAulaVM() { Inicio = _inicio, Fim = _fim, Dia = DayOfWeek.Monday, Label = (_inicio + " - " + _fim) });
                }
                if (_periodo.Ter) {
                    _list.Add(new InstituicaoCursoOcorrenciaProfessorPeriodoAulaVM() { Inicio = _inicio, Fim = _fim, Dia = DayOfWeek.Tuesday, Label = (_inicio + " - " + _fim) });
                }
                if (_periodo.Qua) {
                    _list.Add(new InstituicaoCursoOcorrenciaProfessorPeriodoAulaVM() { Inicio = _inicio, Fim = _fim, Dia = DayOfWeek.Wednesday, Label = (_inicio + " - " + _fim) });
                }
                if (_periodo.Qui) {
                    _list.Add(new InstituicaoCursoOcorrenciaProfessorPeriodoAulaVM() { Inicio = _inicio, Fim = _fim, Dia = DayOfWeek.Thursday, Label = (_inicio + " - " + _fim) });
                }
                if (_periodo.Sex) {
                    _list.Add(new InstituicaoCursoOcorrenciaProfessorPeriodoAulaVM() { Inicio = _inicio, Fim = _fim, Dia = DayOfWeek.Friday, Label = (_inicio + " - " + _fim) });
                }
                if (_periodo.Sab) {
                    _list.Add(new InstituicaoCursoOcorrenciaProfessorPeriodoAulaVM() { Inicio = _inicio, Fim = _fim, Dia = DayOfWeek.Saturday, Label = (_inicio + " - " + _fim) });
                }
            }

            while (_dateTimePausaFim <= _dateTimeFim) {
                var _inicio = _dateTimePausaFim.ToString("HH:mm");
                _dateTimePausaFim = _dateTimePausaFim.AddMinutes((_minutos / _periodo.Quebras));
                var _fim = _dateTimePausaFim.ToString("HH:mm");
                if (_periodo.Dom) {
                    _list.Add(new InstituicaoCursoOcorrenciaProfessorPeriodoAulaVM() { Inicio = _inicio, Fim = _fim, Dia = DayOfWeek.Sunday, Label = (_inicio + " - " + _fim) });
                }
                if (_periodo.Seg) {
                    _list.Add(new InstituicaoCursoOcorrenciaProfessorPeriodoAulaVM() { Inicio = _inicio, Fim = _fim, Dia = DayOfWeek.Monday, Label = (_inicio + " - " + _fim) });
                }
                if (_periodo.Ter) {
                    _list.Add(new InstituicaoCursoOcorrenciaProfessorPeriodoAulaVM() { Inicio = _inicio, Fim = _fim, Dia = DayOfWeek.Tuesday, Label = (_inicio + " - " + _fim) });
                }
                if (_periodo.Qua) {
                    _list.Add(new InstituicaoCursoOcorrenciaProfessorPeriodoAulaVM() { Inicio = _inicio, Fim = _fim, Dia = DayOfWeek.Wednesday, Label = (_inicio + " - " + _fim) });
                }
                if (_periodo.Qui) {
                    _list.Add(new InstituicaoCursoOcorrenciaProfessorPeriodoAulaVM() { Inicio = _inicio, Fim = _fim, Dia = DayOfWeek.Thursday, Label = (_inicio + " - " + _fim) });
                }
                if (_periodo.Sex) {
                    _list.Add(new InstituicaoCursoOcorrenciaProfessorPeriodoAulaVM() { Inicio = _inicio, Fim = _fim, Dia = DayOfWeek.Friday, Label = (_inicio + " - " + _fim) });
                }
                if (_periodo.Sab) {
                    _list.Add(new InstituicaoCursoOcorrenciaProfessorPeriodoAulaVM() { Inicio = _inicio, Fim = _fim, Dia = DayOfWeek.Saturday, Label = (_inicio + " - " + _fim) });
                }
            }

            return _list;
        }

    }
}