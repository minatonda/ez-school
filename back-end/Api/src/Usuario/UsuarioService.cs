using System.Collections.Generic;
using System.Linq;
using Api.AreaInteresseApi;
using Api.CategoriaProfissionalApi;
using Domain.AreaInteresseDomain;
using Domain.UsuarioDomain;

namespace Api.UsuarioApi {

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

            viewModel.UsuarioInfo.AreaInteresses.ForEach(x => {
                var areaInteresse = AreaInteresseAdapter.ToModel(x, true);
                areaInteresse.UsuarioInfo = model.UsuarioInfo;
                this._areaInteresseRepository.Add(areaInteresse);
            });

            this._usuarioRepository.SaveChanges();
            this._areaInteresseRepository.SaveChanges();
        }

        public void Update(UsuarioVM viewModel) {
            var model = UsuarioAdapter.ToModel(viewModel, true);
            this._usuarioRepository.Update(model);

            var areaInteressesAttached = this._areaInteresseRepository.GetAllByUsuario(viewModel.UsuarioInfo.ID, true);
            var areaInteressesAttachedToRemove = areaInteressesAttached.Where(x => !viewModel.UsuarioInfo.AreaInteresses.Select(y => AreaInteresseAdapter.ToModel(y, true).ID).Contains(x.ID)).ToList();

            areaInteressesAttachedToRemove.ForEach(x => {
                this._areaInteresseRepository.Disable(x.ID);
            });

            viewModel.UsuarioInfo.AreaInteresses.ForEach(x => {
                if (!areaInteressesAttached.Select(y => y.ID).Contains(x.ID)) {
                    var areaInteresse = AreaInteresseAdapter.ToModel(x, true);
                    areaInteresse.UsuarioInfo = model.UsuarioInfo;
                    this._areaInteresseRepository.Add(areaInteresse);
                }
            });

            this._usuarioRepository.SaveChanges();
            this._areaInteresseRepository.SaveChanges();
        }

        public void UpdateAluno(AlunoVM viewModel) {
            var model = AlunoAdapter.ToModel(viewModel, true);
            this._usuarioRepository.UpdateAluno(model);
        }

        public void UpdateProfessor(ProfessorVM viewModel) {
            var model = ProfessorAdapter.ToModel(viewModel, true);
            this._usuarioRepository.UpdateProfessor(model);
        }

        public void Disable(string id) {
            this._usuarioRepository.Disable(id);
        }

        public UsuarioVM Detail(string id) {
            var usuario = UsuarioAdapter.ToViewModel(this._usuarioRepository.Get(id), true);
            usuario.UsuarioInfo = this.DetailUsuarioInfo(id);
            return usuario;
        }

        public UsuarioInfoVM DetailUsuarioInfo(string id) {
            var usuarioInfo = this._usuarioRepository.GetUsuarioInfo(id);
            var listAreaInteresse = this._areaInteresseRepository.GetAllByUsuario(id, true);
            return UsuarioAdapter.ToViewModel(usuarioInfo, listAreaInteresse, true);
        }

        public AlunoVM DetailAluno(string id) {
            return AlunoAdapter.ToViewModel(this._usuarioRepository.GetAluno(id), true);
        }

        public ProfessorVM DetailProfessor(string id) {
            return ProfessorAdapter.ToViewModel(this._usuarioRepository.GetProfessor(id), true);
        }

        public List<UsuarioVM> All() {
            return this._usuarioRepository.GetAll(true).Select(x => UsuarioAdapter.ToViewModel(x, true)).ToList();
        }

        public List<UsuarioInfoVM> GetAllByTermo(string perfil, string termo) {
            return this._usuarioRepository.GetAllByTermo(perfil, termo).Select(x => UsuarioAdapter.ToViewModel(x, null, true)).ToList();
        }

    }
}