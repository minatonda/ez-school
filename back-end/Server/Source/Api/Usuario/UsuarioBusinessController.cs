using System;
using System.Collections.Generic;
using Api.Common.Base;
using Api.InstituicaoApi;
using Domain.CursoDomain;
using Domain.InstituicaoDomain;
using Domain.UsuarioDomain;
using Microsoft.AspNetCore.Mvc;

namespace Api.UsuarioApi {

    [Route("api/business/usuario")]
    public class UsuarioBusinessController : BaseController {

        private UsuarioService usuarioService;

        public UsuarioBusinessController(UsuarioRepository usuarioRepository, AreaInteresseRepository areaInteresseRepository, InstituicaoRepository instituicaoRepository, CursoRepository cursoRepository) : base(usuarioRepository, areaInteresseRepository, instituicaoRepository, cursoRepository) {
            this.usuarioService = new UsuarioService(usuarioRepository, areaInteresseRepository);
        }

        [HttpGet("me")]
        public UsuarioInfoVM Me() {
            return this.GetUsuarioInfoAuthenticated();
        }

        [HttpGet("me/authorized-view")]
        public List<string> MeAuthorizedView() {
            return this.usuarioService.GetAllAuthorizedViewByRoles(this.GetAllRolesFromUsuario());
        }

        [HttpGet("me/instituicao")]
        public List<InstituicaoVM> MeInstituicao() {
            return this.GetAllInstituicaoFromUsuario();
        }

        [HttpGet("me/admin")]
        public bool MeAdmin() {
            return this.GetAllRolesFromUsuario().Contains(BaseRole.ADMIN);
        }

    }
}