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

        [HttpGet("{id}")]
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
        public Array AllRoles() {
            return Enum.GetValues(typeof(BaseRole));
        }


    }
}