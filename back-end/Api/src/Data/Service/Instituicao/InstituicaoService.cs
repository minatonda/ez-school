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

        public void RenewCurso(long id, InstituicaoCursoVM instituicaoCurso) {
            var model = InstituicaoCursoAdapter.ToModel(instituicaoCurso, true);
            var periodos = InstituicaoCursoAdapter.InstituicaoCursoPeriodoFromVM(instituicaoCurso);
            var turmas = InstituicaoCursoAdapter.InstituicaoCursoTurmaFromVM(instituicaoCurso);
            this._instituicaoRepository.RenewCurso(id, model, periodos, turmas);
        }

        public void DisableCurso(long id, long idCurso) {
            this._instituicaoRepository.DisableCurso(id, idCurso);
        }

        public InstituicaoCursoVM DetailCurso(long id, long idCurso, string dataInicio) {
            var _dataInicio = DateTime.ParseExact(dataInicio, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var _turmas = this._instituicaoRepository.GetCursoTurmas(id, idCurso, _dataInicio);
            var _periodos = this._instituicaoRepository.GetCursoPeriodos(id, idCurso, _dataInicio);
            return InstituicaoCursoAdapter.ToViewModel(this._instituicaoRepository.GetCurso(id, idCurso, _dataInicio), _periodos, _turmas, false);
        }

        public List<InstituicaoCursoVM> AllCurso(long id) {
            return this._instituicaoRepository.GetCursos(id).Select(x => InstituicaoCursoAdapter.ToViewModel(x, null, null, false)).ToList();
        }

        public List<CursoGradeMateriaVM> AllCursoGradeMateria(long id, long idCurso, string dataInicio) {
            var _dataInicio = DateTime.ParseExact(dataInicio, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            return this._instituicaoRepository.GetCursoGradeMaterias(id, idCurso, _dataInicio).Select(x => CursoGradeMateriaAdapter.ToViewModel(x, true)).ToList();
        }

        public List<InstituicaoCursoPeriodoVM> AllPeriodo(long id, long idCurso, string dataInicio) {
            return this.DetailCurso(id, idCurso, dataInicio).Periodos;
        }

        public List<InstituicaoCursoTurmaVM> AllTurma(long id, long idCurso, string dataInicio) {
            return this.DetailCurso(id, idCurso, dataInicio).Turmas;
        }

        public InstituicaoCursoOcorrenciaVM AddCursoOcorrencia(long id, long idCurso, string dataInicio, InstituicaoCursoOcorrenciaVM instituicaoCurso) {
            var _dataInicio = DateTime.ParseExact(dataInicio, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var _model = InstituicaoCursoOcorrenciaAdapter.ToModel(instituicaoCurso, true);
            var _alunos = InstituicaoCursoOcorrenciaAdapter.InstituicaoCursoOcorrenciaAlunosFrom(instituicaoCurso);
            var _professores = InstituicaoCursoOcorrenciaAdapter.InstituicaoCursoOcorrenciaProfessoresFrom(instituicaoCurso);
            this._instituicaoRepository.AddCursoOcorrencia(id, idCurso, _model, _alunos, _professores);
            return InstituicaoCursoOcorrenciaAdapter.ToViewModel(_model, _alunos, _professores, true);
        }

        public InstituicaoCursoOcorrenciaVM DetailCursoOcorrencia(long id, long idCurso, string dataInicio, string dataInicioOcorrencia) {
            var _dataInicio = DateTime.ParseExact(dataInicio, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var _dataInicioOcorrencia = DateTime.ParseExact(dataInicioOcorrencia, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var _instituicaoCursoOcorrencia = this._instituicaoRepository.GetCursoOcorrencia(id, idCurso, _dataInicio, _dataInicioOcorrencia);
            var _alunos = this._instituicaoRepository.GetCursoOcorrenciaAlunos(_instituicaoCursoOcorrencia.ID);
            var _professores = this._instituicaoRepository.GetCursoOcorrenciaProfessores(_instituicaoCursoOcorrencia.ID);
            return InstituicaoCursoOcorrenciaAdapter.ToViewModel(_instituicaoCursoOcorrencia, _alunos, _professores, true);
        }

        public List<InstituicaoCursoOcorrenciaVM> AllCursoOcorrencia(long id, long idCurso, string dataInicio) {
            var _dataInicio = DateTime.ParseExact(dataInicio, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            return this._instituicaoRepository.GetCursoOcorrencias(id, idCurso, _dataInicio).Select(x => InstituicaoCursoOcorrenciaAdapter.ToViewModel(x, null, null, false)).ToList();
        }

    }
}