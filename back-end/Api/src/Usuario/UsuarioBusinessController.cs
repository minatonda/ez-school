using System;
using System.Collections.Generic;
using Api.Common.Base;
using Domain.UsuarioDomain;
using Microsoft.AspNetCore.Mvc;

namespace Api.UsuarioApi {

    [Route("api/business/usuario")]
    public class UsuarioBusinessController : BaseController {

        private UsuarioService usuarioService;

        public UsuarioBusinessController(UsuarioRepository usuarioRepository) : base(usuarioRepository) {
            this.usuarioService = new UsuarioService(usuarioRepository);
        }


        [HttpGet("/me")]
        public UsuarioInfoVM Me() {
            return this.getLogged();
        }

    }
}