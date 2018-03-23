using System;
using System.Collections.Generic;
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
            if (!this.IsAuthorized(BaseRole.ADD_USUARIO)) {
                throw new BaseUnauthorizedException(BaseRole.ADD_USUARIO);
            }
            this.usuarioService.Add(viewModel);
        }

        [HttpPost("update")]
        public void Update([FromBody] UsuarioVM viewModel) {
            if (!this.IsAuthorized(BaseRole.EDIT_USUARIO)) {
                throw new BaseUnauthorizedException(BaseRole.EDIT_USUARIO);
            }
            this.usuarioService.Update(viewModel);
        }

        [HttpDelete("disable")]
        public void Disable(string id) {
            if (!this.IsAuthorized(BaseRole.DISABLE_USUARIO)) {
                throw new BaseUnauthorizedException(BaseRole.DISABLE_USUARIO);
            }
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
        public List<BaseRole> AllRoles() {
            return new List<BaseRole>(){
                BaseRole.ADMIN,
                BaseRole.ADD_MATERIA,
                BaseRole.EDIT_MATERIA,
                BaseRole.DISABLE_MATERIA,
                BaseRole.DETAIL_MATERIA,
                BaseRole.LIST_MATERIA,
                BaseRole.ADD_USUARIO,
                BaseRole.EDIT_USUARIO,
                BaseRole.DISABLE_USUARIO,
                BaseRole.DETAIL_USUARIO,
                BaseRole.LIST_USUARIO,
                BaseRole.ADD_CURSO,
                BaseRole.EDIT_CURSO,
                BaseRole.DISABLE_CURSO,
                BaseRole.DETAIL_CURSO,
                BaseRole.LIST_CURSO,
                BaseRole.ADD_CATEGORIA_PROFISSIONAL,
                BaseRole.EDIT_CATEGORIA_PROFISSIONAL,
                BaseRole.DISABLE_CATEGORIA_PROFISSIONAL,
                BaseRole.DETAIL_CATEGORIA_PROFISSIONAL,
                BaseRole.LIST_CATEGORIA_PROFISSIONAL,
                BaseRole.ADD_INSTITUICAO,
                BaseRole.EDIT_INSTITUICAO,
                BaseRole.DISABLE_INSTITUICAO,
                BaseRole.DETAIL_INSTITUICAO,
                BaseRole.LIST_INSTITUICAO,
            };
        }


    }
}