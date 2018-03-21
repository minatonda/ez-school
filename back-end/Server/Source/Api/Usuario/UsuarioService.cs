using System.Collections.Generic;
using System.Linq;
using Api.AreaInteresseApi;
using Api.CategoriaProfissionalApi;
using Api.Common.Base;
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

        public List<UsuarioInfoVM> GetAllByTermo(string termo, bool onlyAluno, bool onlyProfessor) {
            return this._usuarioRepository.GetAllByTermo(termo, onlyAluno, onlyProfessor).Select(x => UsuarioAdapter.ToViewModel(x, true)).ToList();
        }

        public List<string> GetAllAuthorizedViewByRoles(List<string> roles) {
            List<string> views = new List<string>();

            roles.ForEach(x => {

                if (x == ((int)BaseRole.ADD_MATERIA).ToString()) {
                    views.Add(ViewAlias.MATERIA_ADD);
                }
                if (x == ((int)BaseRole.EDIT_MATERIA).ToString()) {
                    views.Add(ViewAlias.MATERIA_UPD);
                }
                if (x == ((int)BaseRole.DETAIL_MATERIA).ToString()) {
                    views.Add(ViewAlias.MATERIA_UPD);
                }
                if (x == ((int)BaseRole.LIST_MATERIA).ToString()) {
                    views.Add(ViewAlias.MATERIA);
                }

                if (x == ((int)BaseRole.ADD_USUARIO).ToString()) {
                    views.Add(ViewAlias.USUARIO_ADD);
                }
                if (x == ((int)BaseRole.EDIT_USUARIO).ToString()) {
                    views.Add(ViewAlias.USUARIO_UPD);
                }
                if (x == ((int)BaseRole.DETAIL_USUARIO).ToString()) {
                    views.Add(ViewAlias.USUARIO_UPD);
                }
                if (x == ((int)BaseRole.LIST_USUARIO).ToString()) {
                    views.Add(ViewAlias.USUARIO);
                }

                if (x == ((int)BaseRole.ADD_CURSO).ToString()) {
                    views.Add(ViewAlias.CURSO_ADD);
                }
                if (x == ((int)BaseRole.EDIT_CURSO).ToString()) {
                    views.Add(ViewAlias.CURSO_UPD);
                }
                if (x == ((int)BaseRole.DETAIL_CURSO).ToString()) {
                    views.Add(ViewAlias.CURSO_UPD);
                }
                if (x == ((int)BaseRole.LIST_CURSO).ToString()) {
                    views.Add(ViewAlias.CURSO);
                }

                if (x == ((int)BaseRole.ADD_CATEGORIA_PROFISSIONAL).ToString()) {
                    views.Add(ViewAlias.CATEGORIA_PROFISSIONAL_ADD);
                }
                if (x == ((int)BaseRole.EDIT_CATEGORIA_PROFISSIONAL).ToString()) {
                    views.Add(ViewAlias.CATEGORIA_PROFISSIONAL_UPD);
                }
                if (x == ((int)BaseRole.DETAIL_CATEGORIA_PROFISSIONAL).ToString()) {
                    views.Add(ViewAlias.CATEGORIA_PROFISSIONAL_UPD);
                }
                if (x == ((int)BaseRole.LIST_CATEGORIA_PROFISSIONAL).ToString()) {
                    views.Add(ViewAlias.CATEGORIA_PROFISSIONAL);
                }

                if (x == ((int)BaseRole.ADD_INSTITUICAO).ToString()) {
                    views.Add(ViewAlias.INSTITUICAO_ADD);
                }
                if (x == ((int)BaseRole.EDIT_INSTITUICAO).ToString()) {
                    views.Add(ViewAlias.INSTITUICAO_UPD);
                }
                if (x == ((int)BaseRole.DETAIL_INSTITUICAO).ToString()) {
                    views.Add(ViewAlias.INSTITUICAO_UPD);
                }
                if (x == ((int)BaseRole.LIST_INSTITUICAO).ToString()) {
                    views.Add(ViewAlias.INSTITUICAO);
                }

                if (x == ((int)BaseRole.ADD_INSTITUICAO_CURSO).ToString()) {
                    views.Add(ViewAlias.INSTITUICAO_CURSO_ADD);
                }
                if (x == ((int)BaseRole.EDIT_INSTITUICAO_CURSO).ToString()) {
                    views.Add(ViewAlias.INSTITUICAO_CURSO_UPD);
                }
                if (x == ((int)BaseRole.DETAIL_INSTITUICAO_CURSO).ToString()) {
                    views.Add(ViewAlias.INSTITUICAO_CURSO_UPD);
                }
                if (x == ((int)BaseRole.LIST_INSTITUICAO_CURSO).ToString()) {
                    views.Add(ViewAlias.INSTITUICAO_CURSO);
                }

                if (x == ((int)BaseRole.ADD_INSTITUICAO_CURSO_OCORRENCIA).ToString()) {
                    views.Add(ViewAlias.INSTITUICAO_CURSO_OCORRENCIA_ADD);
                }
                if (x == ((int)BaseRole.EDIT_INSTITUICAO_CURSO_OCORRENCIA).ToString()) {
                    views.Add(ViewAlias.INSTITUICAO_CURSO_OCORRENCIA_UPD);
                }
                if (x == ((int)BaseRole.DETAIL_INSTITUICAO_CURSO_OCORRENCIA).ToString()) {
                    views.Add(ViewAlias.INSTITUICAO_CURSO_OCORRENCIA_UPD);
                }
                if (x == ((int)BaseRole.LIST_INSTITUICAO_CURSO_OCORRENCIA).ToString()) {
                    views.Add(ViewAlias.INSTITUICAO_CURSO_OCORRENCIA);
                }

                if (x == ((int)BaseRole.EDIT_INSTITUICAO_CURSO_OCORRENCIA_NOTA).ToString()) {

                }
                if (x == ((int)BaseRole.EDIT_INSTITUICAO_CURSO_OCORRENCIA_FALTA).ToString()) {

                }

            });

            return views;
        }

    }
}