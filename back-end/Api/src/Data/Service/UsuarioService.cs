using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Interface;
using Domain.Repositories;
using Api.Data.ViewModels;

namespace Api.Data.Service {
    public class UsuarioService {
        private UsuarioRepository _usuarioRepository;
        private AreaInteresseRepository _areaInteresseRepository;

        public UsuarioService(UsuarioRepository usuarioRepository, AreaInteresseRepository areaInteresseRepository) {
            this._usuarioRepository = usuarioRepository;
            this._areaInteresseRepository = areaInteresseRepository;
        }


        public void Add(UsuarioVM viewModel) {
            var model = UsuarioAdapter.ToModel(viewModel, true);
            this._usuarioRepository.Add(model);
        }

        public void Update(UsuarioVM viewModel) {
            var model = UsuarioAdapter.ToModel(viewModel, true);
            this._usuarioRepository.Update(model);
        }

        public void UpdateAluno(AlunoVM viewModel) {
            var model = AlunoAdapter.ToModel(viewModel, true);
            var attached = this._usuarioRepository.GetAluno(model.ID);
            var _attachedAreaInteresses = this._areaInteresseRepository.GetAllByAluno(model.ID, true);
            this._usuarioRepository.UpdateAluno(model);
        }

        public void UpdateProfessor(ProfessorVM viewModel) {
            var model = ProfessorAdapter.ToModel(viewModel, true);
            var attached = this._usuarioRepository.GetAluno(model.ID);
            var _attachedAreaInteresses = this._areaInteresseRepository.GetAllByProfessor(model.ID, true);
            this._usuarioRepository.UpdateProfessor(model);
        }
        public void Disable(string id) {
            this._usuarioRepository.Disable(id);
        }

        public UsuarioVM Detail(string id) {
            return UsuarioAdapter.ToViewModel(this._usuarioRepository.Get(id), true);
        }

        public AlunoVM DetailAluno(string id) {
            var areainteresses = this._areaInteresseRepository.GetAllByAluno(id, true);
            return AlunoAdapter.ToViewModel(this._usuarioRepository.GetAluno(id), areainteresses, true);
        }

        public ProfessorVM DetailProfessor(string id) {
            var areainteresses = this._areaInteresseRepository.GetAllByProfessor(id, true);
            return ProfessorAdapter.ToViewModel(this._usuarioRepository.GetProfessor(id), areainteresses, true);
        }

        public List<UsuarioVM> All() {
            return this._usuarioRepository.GetAll(true).Select(x => UsuarioAdapter.ToViewModel(x, true)).ToList();
        }

        public List<AlunoVM> GetAllAlunosByTermo(string termo) {
            return this._usuarioRepository.GetAllAlunosByTermo(termo, true).Select(x => AlunoAdapter.ToViewModel(x, null, true)).ToList();
        }

        public List<ProfessorVM> GetAllProfessoresByTermo(string termo) {
            return this._usuarioRepository.GetAllProfessoresByTermo(termo, true).Select(x => ProfessorAdapter.ToViewModel(x, null, true)).ToList();
        }

    }
}