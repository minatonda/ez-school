using System.Collections.Generic;
using System.Linq;
using Api.CategoriaProfissionalApi;
using Domain.Models;
using Domain.Repositories;

namespace Api.UsuarioApi {

    public class UsuarioService {

        private UsuarioRepository _usuarioRepository;
        public UsuarioService(UsuarioRepository usuarioRepository) {
            this._usuarioRepository = usuarioRepository;
        }

        public List<UsuarioVM> All() {
            return this._usuarioRepository.GetAll(true).Select(x => UsuarioAdapter.ToViewModel(x, true)).ToList();
        }

        public UsuarioVM Detail(string id) {
            return UsuarioAdapter.ToViewModel(this._usuarioRepository.Get(id), true);
        }

        public UsuarioVM Add(UsuarioVM viewModel) {
            var model = UsuarioAdapter.ToModel(viewModel, true);
            return UsuarioAdapter.ToViewModel(this._usuarioRepository.Add(model), true);
        }

        public UsuarioVM Update(UsuarioVM viewModel) {
            var model = UsuarioAdapter.ToModel(viewModel, true);
            return UsuarioAdapter.ToViewModel(this._usuarioRepository.Update(model), true);
        }

        public void Disable(string id) {
            this._usuarioRepository.Disable(id);
        }

        public AlunoVM DetailAluno(string id) {
            var areainteresses = this._usuarioRepository.GetAlunoAreaInteresse(id);
            return AlunoAdapter.ToViewModel(this._usuarioRepository.GetAluno(id), areainteresses, true);
        }

        public AlunoVM UpdateAluno(AlunoVM viewModel) {
            var model = AlunoAdapter.ToModel(viewModel, true);
            this._usuarioRepository.UpdateAluno(model, viewModel.CategoriaProfissionais.Select(x => new AreaInteresse() { CategoriaProfissional = CategoriaProfissionalAdapter.ToModel(x, true) }).ToList());
            var areainteresse = this._usuarioRepository.GetAlunoAreaInteresse(null);
            return AlunoAdapter.ToViewModel(model, areainteresse, true);
        }

        public List<AlunoVM> GetAlunosByTermo(string termo) {
            return this._usuarioRepository.GetAlunosByTermo(termo).Select(x => AlunoAdapter.ToViewModel(x, null, true)).ToList();
        }

        public ProfessorVM DetailProfessor(string id) {
            var areainteresses = this._usuarioRepository.GetProfessorAreaInteresse(id);
            return ProfessorAdapter.ToViewModel(this._usuarioRepository.GetProfessor(id), areainteresses, true);
        }

        public ProfessorVM UpdateProfessor(ProfessorVM viewModel) {
            var model = ProfessorAdapter.ToModel(viewModel, true);
            this._usuarioRepository.UpdateProfessor(model, viewModel.CategoriaProfissionais.Select(x => new AreaInteresse() { CategoriaProfissional = CategoriaProfissionalAdapter.ToModel(x, true) }).ToList());
            var areainteresse = this._usuarioRepository.GetProfessorAreaInteresse(null);
            return ProfessorAdapter.ToViewModel(model, areainteresse, true);
        }

        public List<ProfessorVM> GetProfessoresByTermo(string termo) {
            return this._usuarioRepository.GetProfessoresByTermo(termo).Select(x => ProfessorAdapter.ToViewModel(x, null, true)).ToList();
        }

    }
}