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
            return AlunoAdapter.ToViewModel(this._usuarioRepository.GetAluno(id), true);
        }
        public AlunoVM UpdateAluno(AlunoVM viewModel) {
            var model = AlunoAdapter.ToModel(viewModel, true);
            this._usuarioRepository.UpdateAluno(model);
            this._usuarioRepository.AddAlunoCategoriaProfissional(model.ID, viewModel.CategoriaProfissional.Select(x => new AreaInteresse() { CategoriaProfissional = CategoriaProfissionalAdapter.ToModel(x, true) }).ToList());
            return AlunoAdapter.ToViewModel(model, true);
        }
        public ProfessorVM DetailProfessor(string id) {
            return ProfessorAdapter.ToViewModel(this._usuarioRepository.GetProfessor(id), true);
        }
        public ProfessorVM UpdateProfessor(ProfessorVM viewModel) {
            var model = ProfessorAdapter.ToModel(viewModel, true);
            return ProfessorAdapter.ToViewModel(this._usuarioRepository.UpdateProfessor(model), true);
        }
    }
}