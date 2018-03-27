using System;
using System.Collections.Generic;
using Api.Common;
using Api.Common.Base;
using Api.CursoApi;
using Domain.Common;
using Domain.CursoDomain;
using Domain.InstituicaoDomain;
using Domain.UsuarioDomain;
using Microsoft.AspNetCore.Mvc;

namespace Api.InstituicaoApi {

    [Route("api/instituicao")]
    public class InstituicaoController : BaseController {

        private InstituicaoService _instituicaoService;

        public InstituicaoController(InstituicaoRepository instituicaoRepository, CursoRepository cursoRepository, UsuarioRepository usuarioRepository, AreaInteresseRepository areaInteresseRepository) : base(usuarioRepository, areaInteresseRepository, instituicaoRepository, cursoRepository) {
            this._instituicaoService = new InstituicaoService(instituicaoRepository, cursoRepository);
        }

        [HttpPut("add")]
        public void Add([FromBody] InstituicaoVM viewModel) {
            this.Authorize(BaseRole.ADD_INSTITUICAO);
            this._instituicaoService.Add(viewModel);
        }

        [HttpPut("{id}/instituicao-colaborador/add")]
        public void AddInstituicaoColaborador(long id, [FromBody] InstituicaoColaboradorVM viewModel) {
            try {
                this.IsAuthorizedInstituicao(id, InstituicaoRole.OWNER_INSTITUICAO);
            } catch (BaseException) {
                this.IsAuthorizedInstituicao(id, InstituicaoRole.ADD_INSTITUICAO_COLABORADOR);
            }
            this._instituicaoService.AddInstituicaoColaborador(id, viewModel);
        }

        [HttpPut("{id}/instituicao-colaborador-perfil/add")]
        public void AddInstituicaoColaboradorPerfil(long id, [FromBody] InstituicaoColaboradorPerfilVM viewModel) {
            try {
                this.IsAuthorizedInstituicao(id, InstituicaoRole.OWNER_INSTITUICAO);
            } catch (BaseException) {
                this.IsAuthorizedInstituicao(id, InstituicaoRole.ADD_INSTITUICAO_COLABORADOR_PERFIL);
            }
            this._instituicaoService.AddInstituicaoColaboradorPerfil(id, viewModel);
        }

        [HttpPut("{id}/instituicao-curso/add")]
        public void AddInstituicaoCurso(long id, [FromBody] InstituicaoCursoVM viewModel) {
            try {
                this.IsAuthorizedInstituicao(id, InstituicaoRole.OWNER_INSTITUICAO);
            } catch (BaseException) {
                this.IsAuthorizedInstituicao(id, InstituicaoRole.ADD_INSTITUICAO_CURSO);
            }
            this._instituicaoService.AddInstituicaoCurso(id, viewModel);
        }

        [HttpPut("{id}/instituicao-curso/{idInstituicaoCurso}/instituicao-curso-ocorrencia/add")]
        public void AddInstituicaoCursoOcorrencia(long id, long idInstituicaoCurso, [FromBody] InstituicaoCursoOcorrenciaVM viewModel) {
            try {
                this.IsAuthorizedInstituicao(id, InstituicaoRole.OWNER_INSTITUICAO);
            } catch (BaseException) {
                this.IsAuthorizedInstituicao(id, InstituicaoRole.ADD_INSTITUICAO_CURSO_OCORRENCIA);
            }
            this._instituicaoService.AddInstituicaoCursoOcorrencia(idInstituicaoCurso, viewModel);
        }

        [HttpPost("update")]
        public void Update([FromBody] InstituicaoVM viewModel) {
            try {
                this.Authorize(BaseRole.EDIT_INSTITUICAO);
            } catch (BaseException) {
                this.IsAuthorizedInstituicao(viewModel.ID, InstituicaoRole.OWNER_INSTITUICAO);
            }
            this._instituicaoService.Update(viewModel);
        }

        [HttpPost("{id}/instituicao-colaborador/update")]
        public void UpdateInstituicaoColaborador(long id, [FromBody] InstituicaoColaboradorVM viewModel) {
            try {
                this.IsAuthorizedInstituicao(id, InstituicaoRole.OWNER_INSTITUICAO);
            } catch (BaseException) {
                this.IsAuthorizedInstituicao(id, InstituicaoRole.EDIT_INSTITUICAO_COLABORADOR);
            }
            this._instituicaoService.UpdateInstituicaoColaborador(id, viewModel);
        }

        [HttpPost("{id}/instituicao-colaborador-perfil/update")]
        public void UpdateInstituicaoColaboradorPerfil(long id, [FromBody] InstituicaoColaboradorPerfilVM viewModel) {
            try {
                this.IsAuthorizedInstituicao(id, InstituicaoRole.OWNER_INSTITUICAO);
            } catch (BaseException) {
                this.IsAuthorizedInstituicao(id, InstituicaoRole.EDIT_INSTITUICAO_COLABORADOR_PERFIL);
            }
            this._instituicaoService.UpdateInstituicaoColaboradorPerfil(id, viewModel);
        }

        [HttpPost("{id}/instituicao-curso/update")]
        public void UpdateInstituicaoCurso(long id, [FromBody] InstituicaoCursoVM viewModel) {
            try {
                this.IsAuthorizedInstituicao(id, InstituicaoRole.OWNER_INSTITUICAO);
            } catch (BaseException) {
                this.IsAuthorizedInstituicao(id, InstituicaoRole.EDIT_INSTITUICAO_CURSO);
            }
            this._instituicaoService.UpdateInstituicaoCurso(id, viewModel);
        }

        [HttpPost("{id}/instituicao-curso/{idInstituicaoCurso}/instituicao-curso-ocorrencia/update")]
        public void UpdateInstituicaoCursoOcorrencia(long id, long idInstituicaoCurso, [FromBody] InstituicaoCursoOcorrenciaVM viewModel) {
            try {
                this.IsAuthorizedInstituicao(id, InstituicaoRole.OWNER_INSTITUICAO);
            } catch (BaseException) {
                this.IsAuthorizedInstituicao(id, InstituicaoRole.EDIT_INSTITUICAO_CURSO_OCORRENCIA);
            }
            this._instituicaoService.UpdateInstituicaoCursoOcorrencia(idInstituicaoCurso, viewModel);
        }

        [HttpDelete("disable")]
        public void Disable([FromQuery] long id) {
            try {
                this.IsAuthorizedInstituicao(id, InstituicaoRole.OWNER_INSTITUICAO);
            } catch (BaseException) {
                this.Authorize(BaseRole.DISABLE_INSTITUICAO);
            }
            this._instituicaoService.Disable(id);
        }

        [HttpDelete("{id}/instituicao-curso/disable")]
        public void DisableInstituicaoCurso(long id, [FromQuery]long idInstituicaoCurso) {
            try {
                this.IsAuthorizedInstituicao(id, InstituicaoRole.OWNER_INSTITUICAO);
            } catch (BaseException) {
                this.IsAuthorizedInstituicao(id, InstituicaoRole.DISABLE_INSTITUICAO_CURSO);
            }
            this._instituicaoService.DisableInstituicaoCurso(idInstituicaoCurso);
        }

        [HttpDelete("{id}/instituicao-colaborador/disable")]
        public void DisableInstituicaoColaborador(long id, [FromQuery]long idInstituicaoColaborador) {
            try {
                this.IsAuthorizedInstituicao(id, InstituicaoRole.OWNER_INSTITUICAO);
            } catch (BaseException) {
                this.IsAuthorizedInstituicao(id, InstituicaoRole.DISABLE_INSTITUICAO_COLABORADOR);
            }
            this._instituicaoService.DisableInstituicaoColaborador(idInstituicaoColaborador);
        }

        [HttpDelete("{id}/instituicao-colaborador-perfil/disable")]
        public void DisableInstituicaoColaboradorPerfil(long id, [FromQuery]long idInstituicaoColaboradorPerfil) {
            try {
                this.IsAuthorizedInstituicao(id, InstituicaoRole.OWNER_INSTITUICAO);
            } catch (BaseException) {
                this.IsAuthorizedInstituicao(id, InstituicaoRole.DISABLE_INSTITUICAO_COLABORADOR_PERFIL);
            }
            this._instituicaoService.DisableInstituicaoColaboradorPerfil(idInstituicaoColaboradorPerfil);
        }

        [HttpDelete("{id}/instituicao-curso/{idInstituicaoCurso}/instituicao-curso-ocorrencia/disable")]
        public void DisableInstituicaoCursoOcorrencia(long id, long idInstituicaoCurso, [FromQuery]long idInstituicaoCursoOcorrencia) {
            try {
                this.IsAuthorizedInstituicao(id, InstituicaoRole.OWNER_INSTITUICAO);
            } catch (BaseException) {
                this.IsAuthorizedInstituicao(id, InstituicaoRole.DISABLE_INSTITUICAO_CURSO_OCORRENCIA);
            }
            this._instituicaoService.DisableInstituicaoCursoOcorrencia(idInstituicaoCursoOcorrencia);
        }

        [HttpGet("detail/{id}")]
        public InstituicaoVM Detail(long id) {
            return this._instituicaoService.Detail(id);
        }

        [HttpGet("detail/{id}/instituicao-colaborador/detail/{idInstituicaoColaborador}")]
        public InstituicaoColaboradorVM DetailInstituicaoColaborador(long id, long idInstituicaoColaborador) {
            return this._instituicaoService.DetailInstituicaoColaborador(idInstituicaoColaborador);
        }

        [HttpGet("detail/{id}/instituicao-colaborador-perfil/detail/{idInstituicaoColaboradorPerfil}")]
        public InstituicaoColaboradorPerfilVM DetailInstituicaoColaboradorPerfil(long id, long idInstituicaoColaboradorPerfil) {
            return this._instituicaoService.DetailInstituicaoColaboradorPerfil(idInstituicaoColaboradorPerfil);
        }

        [HttpGet("detail/{id}/instituicao-curso/detail/{idInstituicaoCurso}")]
        public InstituicaoCursoVM DetailInstituicaoCurso(long id, long idInstituicaoCurso) {
            return this._instituicaoService.DetailInstituicaoCurso(idInstituicaoCurso);
        }

        [HttpGet("detail/{id}/instituicao-curso/detail/{idInstituicaoCurso}/instituicao-curso-ocorrencia/detail/{idInstituicaoCursoOcorrencia}")]
        public InstituicaoCursoOcorrenciaVM DetailInstituicaoCursoOcorrencia(long id, long idInstituicaoCurso, long idInstituicaoCursoOcorrencia) {
            return this._instituicaoService.DetailInstituicaoCursoOcorrencia(idInstituicaoCursoOcorrencia);
        }

        [HttpGet]
        public List<InstituicaoVM> All() {
            return this._instituicaoService.All();
        }

        [HttpGet("detail/{id}/instituicao-colaborador")]
        public List<InstituicaoColaboradorVM> AllInstituicaoColaborador(long id) {
            return this._instituicaoService.AllInstituicaoColaborador(id);
        }

        [HttpGet("detail/{id}/instituicao-colaborador-perfil")]
        public List<InstituicaoColaboradorPerfilVM> AllInstituicaoColaboradorPerfil(long id) {
            return this._instituicaoService.AllInstituicaoColaboradorPerfil(id);
        }

        [HttpGet("detail/{id}/instituicao-curso")]
        public List<InstituicaoCursoVM> AllInstituicaoCurso(long id) {
            return this._instituicaoService.AllInstituicaoCurso(id);
        }

        [HttpGet("detail/{id}/instituicao-curso/detail/{idInstituicaoCurso}/instituicao-curso-ocorrencia")]
        public List<InstituicaoCursoOcorrenciaVM> AllInstituicaoCursoOcorrencia(long id, long idInstituicaoCurso) {
            return this._instituicaoService.AllInstituicaoCursoOcorrencia(idInstituicaoCurso);
        }

        [HttpGet("detail/{id}/instituicao-curso/detail/{idInstituicaoCurso}/instituicao-curso-periodo")]
        public List<InstituicaoCursoPeriodoVM> AllInstituicaoCursoPeriodo(long id, long idInstituicaoCurso) {
            return this._instituicaoService.AllInstituicaoCursoPeriodo(idInstituicaoCurso);
        }

        [HttpGet("detail/{id}/instituicao-curso/detail/{idInstituicaoCurso}/instituicao-curso-turma")]
        public List<InstituicaoCursoTurmaVM> AllInstituicaoCursoTurma(long id, long idInstituicaoCurso) {
            return this._instituicaoService.AllInstituicaoCursoTurma(idInstituicaoCurso);
        }

        [HttpGet("detail/{id}/instituicao-curso/detail/{idInstituicaoCurso}/curso-grade-materia")]
        public List<CursoGradeMateriaVM> AllCursoGradeMateria(long id, long idInstituicaoCurso) {
            return this._instituicaoService.AllCursoGradeMateria(idInstituicaoCurso);
        }

        [HttpGet("roles")]
        public List<TreeViewVM<string>> AllRoles() {

            var roles = new List<TreeViewVM<string>>(){
                new TreeViewVM<string>(){
                    ID = InstituicaoRole.OWNER_INSTITUICAO,
                    ChildrenRequisite = false,
                    Children = new List<TreeViewVM<string>>(){
                        new TreeViewVM<string>(){
                            ID = InstituicaoRole.ADD_INSTITUICAO_CURSO,
                        },
                        new TreeViewVM<string>(){
                            ID = InstituicaoRole.EDIT_INSTITUICAO_CURSO,
                        },
                        new TreeViewVM<string>(){
                            ID = InstituicaoRole.DISABLE_INSTITUICAO_CURSO,
                        },
                        new TreeViewVM<string>(){
                            ID = InstituicaoRole.DETAIL_INSTITUICAO_CURSO,
                        },
                        new TreeViewVM<string>(){
                            ID = InstituicaoRole.LIST_INSTITUICAO_CURSO,
                            ChildrenRequisite = true,
                            Children = new List<TreeViewVM<string>>(){
                                new TreeViewVM<string>(){
                                    ID = InstituicaoRole.ADD_INSTITUICAO_CURSO_OCORRENCIA,
                                },
                                new TreeViewVM<string>(){
                                    ID = InstituicaoRole.EDIT_INSTITUICAO_CURSO_OCORRENCIA,
                                },
                                new TreeViewVM<string>(){
                                    ID = InstituicaoRole.DISABLE_INSTITUICAO_CURSO_OCORRENCIA,
                                },
                                new TreeViewVM<string>(){
                                    ID = InstituicaoRole.DETAIL_INSTITUICAO_CURSO_OCORRENCIA,
                                },
                                new TreeViewVM<string>(){
                                    ID = InstituicaoRole.LIST_INSTITUICAO_CURSO_OCORRENCIA,
                                }
                            }
                        },

                        new TreeViewVM<string>(){
                            ID = InstituicaoRole.ADD_INSTITUICAO_COLABORADOR,
                        },
                        new TreeViewVM<string>(){
                            ID = InstituicaoRole.EDIT_INSTITUICAO_COLABORADOR,
                        },
                        new TreeViewVM<string>(){
                            ID = InstituicaoRole.DISABLE_INSTITUICAO_COLABORADOR,
                        },
                        new TreeViewVM<string>(){
                            ID = InstituicaoRole.DETAIL_INSTITUICAO_COLABORADOR,
                        },
                        new TreeViewVM<string>(){
                            ID = InstituicaoRole.LIST_INSTITUICAO_COLABORADOR,
                        },

                        new TreeViewVM<string>(){
                            ID = InstituicaoRole.ADD_INSTITUICAO_COLABORADOR_PERFIL,
                        },
                        new TreeViewVM<string>(){
                            ID = InstituicaoRole.EDIT_INSTITUICAO_COLABORADOR_PERFIL,
                        },
                        new TreeViewVM<string>(){
                            ID = InstituicaoRole.DISABLE_INSTITUICAO_COLABORADOR_PERFIL,
                        },
                        new TreeViewVM<string>(){
                            ID = InstituicaoRole.DETAIL_INSTITUICAO_COLABORADOR_PERFIL,
                        },
                        new TreeViewVM<string>(){
                            ID = InstituicaoRole.LIST_INSTITUICAO_COLABORADOR_PERFIL,
                        }
                    }
                },

            };

            return roles;
        }


    }
}