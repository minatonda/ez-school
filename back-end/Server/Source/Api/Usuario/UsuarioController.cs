using System;
using System.Collections.Generic;
using Api.Common;
using Api.Common.Base;
using Domain.CursoDomain;
using Domain.InstituicaoDomain;
using Domain.UsuarioDomain;
using Microsoft.AspNetCore.Mvc;

namespace Api.UsuarioApi {

    [Route("api/usuario")]
    public class UsuarioController : BaseController {

        private UsuarioService usuarioService;

        public UsuarioController(UsuarioRepository usuarioRepository, AreaInteresseRepository areaInteresseRepository, InstituicaoRepository instituicaoRepository, CursoRepository cursoRepository) : base(usuarioRepository, areaInteresseRepository, instituicaoRepository, cursoRepository) {
            this.usuarioService = new UsuarioService(usuarioRepository, areaInteresseRepository);
        }

        [HttpPut("add")]
        public void Add([FromBody] UsuarioVM viewModel) {
            this.Authorize(BaseRole.ADD_USUARIO);
            this.usuarioService.ValidateUsuario(viewModel, true);
            this.usuarioService.Add(viewModel);
        }

        [HttpPost("update")]
        public void Update([FromBody] UsuarioVM viewModel) {
            if (viewModel.ID != this.GetUsuarioInfoAuthenticated().ID) {
                this.Authorize(BaseRole.EDIT_USUARIO);
            }
            this.usuarioService.ValidateUsuario(viewModel);
            this.usuarioService.Update(viewModel);
        }

        [HttpDelete("disable")]
        public void Disable(string id) {
            this.Authorize(BaseRole.DISABLE_USUARIO);
            this.usuarioService.Disable(id);
        }

        [HttpGet("detail/{id}")]
        public UsuarioVM Detail(string id, [FromQuery]string termo) {
            var x = this.GetUsuarioInfoAuthenticated();
            return this.usuarioService.Detail(id);
        }

        [HttpGet]
        public List<UsuarioVM> All() {
            return this.usuarioService.All();
        }

        [HttpGet("by-termo")]
        public List<UsuarioInfoVM> AllByTermo([FromQuery] string termo, [FromQuery] bool onlyAluno = false, [FromQuery] bool onlyProfessor = false) {
            if (termo == null) {
                termo = "";
            }
            return this.usuarioService.GetAllByTermo(termo, onlyAluno, onlyProfessor);
        }

        [HttpGet("roles")]
        public List<TreeViewVM<string>> AllRoles() {

            var roles = new List<TreeViewVM<string>>(){
                new TreeViewVM<string>(){
                    ID = BaseRole.ADMIN,
                    ChildrenRequisite = false,
                    Children = new List<TreeViewVM<string>>(){

                        new TreeViewVM<string>(){
                            ID = BaseRole.ADD_INSTITUICAO,
                        },
                        new TreeViewVM<string>(){
                            ID = BaseRole.EDIT_INSTITUICAO,
                        },
                        new TreeViewVM<string>(){
                            ID = BaseRole.DISABLE_INSTITUICAO,
                        },
                        new TreeViewVM<string>(){
                            ID = BaseRole.DETAIL_INSTITUICAO,
                        },
                        new TreeViewVM<string>(){
                            ID = BaseRole.LIST_INSTITUICAO,
                        },

                        new TreeViewVM<string>(){
                            ID = BaseRole.ADD_CURSO,
                        },
                        new TreeViewVM<string>(){
                            ID = BaseRole.EDIT_CURSO,
                        },
                        new TreeViewVM<string>(){
                            ID = BaseRole.DISABLE_CURSO,
                        },
                        new TreeViewVM<string>(){
                            ID = BaseRole.DETAIL_CURSO,
                        },
                        new TreeViewVM<string>(){
                            ID = BaseRole.LIST_CURSO,
                        },

                        new TreeViewVM<string>(){
                            ID = BaseRole.ADD_MATERIA,
                        },
                        new TreeViewVM<string>(){
                            ID = BaseRole.EDIT_MATERIA,
                        },
                        new TreeViewVM<string>(){
                            ID = BaseRole.DISABLE_MATERIA,
                        },
                        new TreeViewVM<string>(){
                            ID = BaseRole.DETAIL_MATERIA,
                        },
                        new TreeViewVM<string>(){
                            ID = BaseRole.LIST_MATERIA,
                        },


                        new TreeViewVM<string>(){
                            ID = BaseRole.ADD_CATEGORIA_PROFISSIONAL,
                        },
                        new TreeViewVM<string>(){
                            ID = BaseRole.EDIT_CATEGORIA_PROFISSIONAL,
                        },
                        new TreeViewVM<string>(){
                            ID = BaseRole.DISABLE_CATEGORIA_PROFISSIONAL,
                        },
                        new TreeViewVM<string>(){
                            ID = BaseRole.DETAIL_CATEGORIA_PROFISSIONAL,
                        },
                        new TreeViewVM<string>(){
                            ID = BaseRole.LIST_CATEGORIA_PROFISSIONAL,
                        },

                        new TreeViewVM<string>(){
                            ID = BaseRole.ADD_USUARIO,
                        },
                        new TreeViewVM<string>(){
                            ID = BaseRole.EDIT_USUARIO,
                        },
                        new TreeViewVM<string>(){
                            ID = BaseRole.DISABLE_USUARIO,
                        },
                        new TreeViewVM<string>(){
                            ID = BaseRole.DETAIL_USUARIO,
                        },
                        new TreeViewVM<string>(){
                            ID = BaseRole.LIST_USUARIO,
                        }
                    }
                },

            };

            return roles;
        }

    }
}