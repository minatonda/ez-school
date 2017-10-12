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
        public InstituicaoService (InstituicaoRepository instituicaoRepository, CursoRepository cursoRespository) {
            this._instituicaoRepository = instituicaoRepository;
            this._cursoRespository = cursoRespository;

        }

        public InstituicaoVM Add (InstituicaoVM viewModel) {
            var model = InstituicaoAdapter.ToModel (viewModel, true);
            return InstituicaoAdapter.ToViewModel (this._instituicaoRepository.Add (model), true);
        }
        public InstituicaoVM Update (InstituicaoVM viewModel) {
            var model = InstituicaoAdapter.ToModel (viewModel, true);
            return InstituicaoAdapter.ToViewModel (this._instituicaoRepository.Update (model), true);
        }
        public void Disable (long id) {
            this._instituicaoRepository.Disable (id);
        }
        public InstituicaoVM Detail (long id) {
            return InstituicaoAdapter.ToViewModel (this._instituicaoRepository.Get (id), true);
        }
        public List<InstituicaoVM> All () {
            return this._instituicaoRepository.GetAll (true).Select (x => InstituicaoAdapter.ToViewModel (x, true)).ToList ();
        }

        public void AddCategoria (long id, InstituicaoCategoriaVM viewModel) {
            this._instituicaoRepository.AddCategoria (id, InstituicaoCategoriaAdapter.ToModel (viewModel, false));
        }
        public void DeleteCategoria (long id, long idCategoria) {
            this._instituicaoRepository.DeleteCategoria (id, idCategoria);
        }
        public List<InstituicaoCategoriaVM> AllCategoria (long id) {
            return this._instituicaoRepository.GetCategorias (id).Select (x => InstituicaoCategoriaAdapter.ToViewModel (x, false)).ToList ();
        }

        public void AddCurso (long id, InstituicaoCursoVM instituicaoCurso) {
            this._instituicaoRepository.AddCurso (id, InstituicaoAdapter.ToModel (instituicaoCurso, true));
        }
        public void RenewCurso (long id, InstituicaoCursoVM instituicaoCurso) {
            this._instituicaoRepository.RenewCurso (id, InstituicaoAdapter.ToModel (instituicaoCurso, true));
        }
        public void DisableCurso (long id, long idCurso) {
            this._instituicaoRepository.DisableCurso (id, idCurso);
        }
        public InstituicaoCursoVM DetailCurso (long id, long idCurso) {
            return InstituicaoAdapter.ToViewModel (this._instituicaoRepository.GetCurso (id, idCurso), false);
        }
        public List<InstituicaoCursoVM> AllCurso (long id) {
            return this._instituicaoRepository.GetCursos (id).Select (x => InstituicaoAdapter.ToViewModel (x, false)).ToList ();
        }

        public void AddCursoOcorrencia (long id, long idCurso, InstituicaoCursoOcorrenciaVM instituicaoCurso) {
            this._instituicaoRepository.AddCursoOcorrencia (id, idCurso, InstituicaoAdapter.ToModel (instituicaoCurso, true));
        }
        public void DeleteCursoOcorrencia (long id, long idCurso, long idOcorrencia) {
            this._instituicaoRepository.DeleteCursoOcorrencia (id, idCurso, idOcorrencia);
        }
        public InstituicaoCursoOcorrenciaVM DetailCursoOcorrencia (long id, long idCurso, long idOcorrencia) {
            return InstituicaoAdapter.ToViewModel (this._instituicaoRepository.GetCursoOcorrencia (id, idCurso, idOcorrencia), false);
        }
        public List<InstituicaoCursoOcorrenciaVM> AllCursoOcorrencia (long id, long idCurso) {
            return this._instituicaoRepository.GetCursoOcorrencias (id, idCurso).Select (x => InstituicaoAdapter.ToViewModel (x, false)).ToList ();
        }

    }
}