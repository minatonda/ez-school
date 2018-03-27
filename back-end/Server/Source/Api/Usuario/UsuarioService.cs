using System.Collections.Generic;
using System.Linq;
using Api.AreaInteresseApi;
using Api.InstituicaoApi;
using Api.CategoriaProfissionalApi;
using Api.Common;
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

        public UsuarioVM Autenticar(UsuarioVM usuario) {
            var attachedUsuario = this._usuarioRepository.Query(x => x.Username == usuario.Username && x.Password == usuario.Password).SingleOrDefault();
            if (attachedUsuario != null) {
                return UsuarioAdapter.ToViewModel(attachedUsuario, false);
            }
            if (attachedUsuario == null && this._usuarioRepository.Query(x => x.Username == usuario.Username).SingleOrDefault() == null) {
                throw new BaseException() {
                    Code = BaseExceptionCode.USER_WRONG_USERNAME,
                    Infos = new List<BaseExceptionFieldInfo>() {
                        new BaseExceptionFieldInfo(){
                            Field=BaseExceptionField.USUARIO_USERNAME,
                            Code=BaseExceptionCode.USER_WRONG_USERNAME
                        }
                    }
                };
            } else {
                throw new BaseException() {
                    Code = BaseExceptionCode.USER_WRONG_PASSWORD,
                    Infos = new List<BaseExceptionFieldInfo>() {
                        new BaseExceptionFieldInfo(){
                            Field=BaseExceptionField.USUARIO_PASSWORD,
                            Code=BaseExceptionCode.USER_WRONG_PASSWORD
                        }
                    }
                };
            }
        }

        public List<string> GetAllAuthorizedViewByRoles(List<string> roles) {
            List<string> views = new List<string>();

            roles.ForEach(x => {


                if (x == BaseRole.ADD_MATERIA || x == BaseRole.ADMIN) {
                    views.Add(ViewAlias.MATERIA_ADD);
                }
                if (x == BaseRole.EDIT_MATERIA || x == BaseRole.ADMIN) {
                    views.Add(ViewAlias.MATERIA_UPD);
                }
                if (x == BaseRole.DETAIL_MATERIA || x == BaseRole.ADMIN) {
                    views.Add(ViewAlias.MATERIA_UPD);
                }
                if (x == BaseRole.LIST_MATERIA || x == BaseRole.ADMIN) {
                    views.Add(ViewAlias.MATERIA);
                }

                if (x == BaseRole.ADD_USUARIO || x == BaseRole.ADMIN) {
                    views.Add(ViewAlias.USUARIO_ADD);
                }
                if (x == BaseRole.EDIT_USUARIO || x == BaseRole.ADMIN) {
                    views.Add(ViewAlias.USUARIO_UPD);
                }
                if (x == BaseRole.DETAIL_USUARIO || x == BaseRole.ADMIN) {
                    views.Add(ViewAlias.USUARIO_UPD);
                }
                if (x == BaseRole.LIST_USUARIO || x == BaseRole.ADMIN) {
                    views.Add(ViewAlias.USUARIO);
                }

                if (x == BaseRole.ADD_CURSO || x == BaseRole.ADMIN) {
                    views.Add(ViewAlias.CURSO_ADD);
                }
                if (x == BaseRole.EDIT_CURSO || x == BaseRole.ADMIN) {
                    views.Add(ViewAlias.CURSO_UPD);
                }
                if (x == BaseRole.DETAIL_CURSO || x == BaseRole.ADMIN) {
                    views.Add(ViewAlias.CURSO_UPD);
                }
                if (x == BaseRole.LIST_CURSO || x == BaseRole.ADMIN) {
                    views.Add(ViewAlias.CURSO);
                }

                if (x == BaseRole.ADD_CATEGORIA_PROFISSIONAL || x == BaseRole.ADMIN) {
                    views.Add(ViewAlias.CATEGORIA_PROFISSIONAL_ADD);
                }
                if (x == BaseRole.EDIT_CATEGORIA_PROFISSIONAL || x == BaseRole.ADMIN) {
                    views.Add(ViewAlias.CATEGORIA_PROFISSIONAL_UPD);
                }
                if (x == BaseRole.DETAIL_CATEGORIA_PROFISSIONAL || x == BaseRole.ADMIN) {
                    views.Add(ViewAlias.CATEGORIA_PROFISSIONAL_UPD);
                }
                if (x == BaseRole.LIST_CATEGORIA_PROFISSIONAL || x == BaseRole.ADMIN) {
                    views.Add(ViewAlias.CATEGORIA_PROFISSIONAL);
                }



                if (x == BaseRole.ADD_INSTITUICAO || x == BaseRole.ADMIN) {
                    views.Add(ViewAlias.INSTITUICAO_ADD);
                }
                if (x == BaseRole.EDIT_INSTITUICAO || x == BaseRole.ADMIN || x == InstituicaoRole.OWNER_INSTITUICAO) {
                    views.Add(ViewAlias.INSTITUICAO_UPD);
                }
                if (x == BaseRole.DETAIL_INSTITUICAO || x == BaseRole.ADMIN) {
                    views.Add(ViewAlias.INSTITUICAO_UPD);
                }
                if (x == BaseRole.LIST_INSTITUICAO || x == BaseRole.ADMIN) {
                    views.Add(ViewAlias.INSTITUICAO);
                }

                if (x == InstituicaoRole.ADD_INSTITUICAO_CURSO || x == BaseRole.ADMIN || x == InstituicaoRole.OWNER_INSTITUICAO) {
                    views.Add(ViewAlias.INSTITUICAO_CURSO_ADD);
                }
                if (x == InstituicaoRole.EDIT_INSTITUICAO_CURSO || x == BaseRole.ADMIN || x == InstituicaoRole.OWNER_INSTITUICAO) {
                    views.Add(ViewAlias.INSTITUICAO_CURSO_UPD);
                }
                if (x == InstituicaoRole.DETAIL_INSTITUICAO_CURSO || x == BaseRole.ADMIN || x == InstituicaoRole.OWNER_INSTITUICAO) {
                    views.Add(ViewAlias.INSTITUICAO_CURSO_UPD);
                }
                if (x == InstituicaoRole.LIST_INSTITUICAO_CURSO || x == BaseRole.ADMIN || x == InstituicaoRole.OWNER_INSTITUICAO) {
                    views.Add(ViewAlias.INSTITUICAO_CURSO);
                }

                if (x == InstituicaoRole.ADD_INSTITUICAO_COLABORADOR || x == BaseRole.ADMIN || x == InstituicaoRole.OWNER_INSTITUICAO) {
                    views.Add(ViewAlias.INSTITUICAO_COLABORADOR_ADD);
                }
                if (x == InstituicaoRole.EDIT_INSTITUICAO_COLABORADOR || x == BaseRole.ADMIN || x == InstituicaoRole.OWNER_INSTITUICAO) {
                    views.Add(ViewAlias.INSTITUICAO_COLABORADOR_UPD);
                }
                if (x == InstituicaoRole.DETAIL_INSTITUICAO_COLABORADOR || x == BaseRole.ADMIN || x == InstituicaoRole.OWNER_INSTITUICAO) {
                    views.Add(ViewAlias.INSTITUICAO_COLABORADOR_UPD);
                }
                if (x == InstituicaoRole.LIST_INSTITUICAO_COLABORADOR || x == BaseRole.ADMIN || x == InstituicaoRole.OWNER_INSTITUICAO) {
                    views.Add(ViewAlias.INSTITUICAO_COLABORADOR);
                }

                if (x == InstituicaoRole.ADD_INSTITUICAO_COLABORADOR_PERFIL || x == BaseRole.ADMIN || x == InstituicaoRole.OWNER_INSTITUICAO) {
                    views.Add(ViewAlias.INSTITUICAO_COLABORADOR_PERFIL_ADD);
                }
                if (x == InstituicaoRole.EDIT_INSTITUICAO_COLABORADOR_PERFIL || x == BaseRole.ADMIN || x == InstituicaoRole.OWNER_INSTITUICAO) {
                    views.Add(ViewAlias.INSTITUICAO_COLABORADOR_PERFIL_UPD);
                }
                if (x == InstituicaoRole.DETAIL_INSTITUICAO_COLABORADOR_PERFIL || x == BaseRole.ADMIN || x == InstituicaoRole.OWNER_INSTITUICAO) {
                    views.Add(ViewAlias.INSTITUICAO_COLABORADOR_PERFIL_UPD);
                }
                if (x == InstituicaoRole.LIST_INSTITUICAO_COLABORADOR_PERFIL || x == BaseRole.ADMIN || x == InstituicaoRole.OWNER_INSTITUICAO) {
                    views.Add(ViewAlias.INSTITUICAO_COLABORADOR_PERFIL);
                }

                if (x == InstituicaoRole.ADD_INSTITUICAO_CURSO_OCORRENCIA || x == BaseRole.ADMIN || x == InstituicaoRole.OWNER_INSTITUICAO) {
                    views.Add(ViewAlias.INSTITUICAO_CURSO_OCORRENCIA_ADD);
                }
                if (x == InstituicaoRole.EDIT_INSTITUICAO_CURSO_OCORRENCIA || x == BaseRole.ADMIN || x == InstituicaoRole.OWNER_INSTITUICAO) {
                    views.Add(ViewAlias.INSTITUICAO_CURSO_OCORRENCIA_UPD);
                }
                if (x == InstituicaoRole.DETAIL_INSTITUICAO_CURSO_OCORRENCIA || x == BaseRole.ADMIN || x == InstituicaoRole.OWNER_INSTITUICAO) {
                    views.Add(ViewAlias.INSTITUICAO_CURSO_OCORRENCIA_UPD);
                }
                if (x == InstituicaoRole.LIST_INSTITUICAO_CURSO_OCORRENCIA || x == BaseRole.ADMIN || x == InstituicaoRole.OWNER_INSTITUICAO) {
                    views.Add(ViewAlias.INSTITUICAO_CURSO_OCORRENCIA);
                }


                if (x == InstituicaoRole.EDIT_INSTITUICAO_CURSO_OCORRENCIA_NOTA) {

                }
                if (x == InstituicaoRole.EDIT_INSTITUICAO_CURSO_OCORRENCIA_FALTA) {

                }

            });

            return views.GroupBy(x => x).Select(x => x.First()).ToList();
        }

    }
}