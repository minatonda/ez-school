using System;
using System.Collections.Generic;
using Api.Common.Base;
using Domain.UsuarioDomain;
using Microsoft.AspNetCore.Mvc;

namespace Api.UsuarioApi {

    [Route("api/business/usuario")]
    public class UsuarioBusinessController : BaseController {

        private UsuarioService usuarioService;

        public UsuarioBusinessController(UsuarioRepository usuarioRepository, AreaInteresseRepository areaInteresseRepository) : base(usuarioRepository, areaInteresseRepository) {
            this.usuarioService = new UsuarioService(usuarioRepository, areaInteresseRepository);
        }


        [HttpGet("/me")]
        public UsuarioInfoVM Me() {
            return this.getLogged();
        }

        [HttpGet("all-by-term")]
        public List<UsuarioInfoVM> AllByTerm([FromQuery] string perfil, [FromQuery]string termo) {
            return this.usuarioService.GetAllByTermo(perfil, termo);
        }

    }
}