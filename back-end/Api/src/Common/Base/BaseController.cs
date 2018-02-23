using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web;
using Api.UsuarioApi;
using Domain.UsuarioDomain;
using Microsoft.AspNetCore.Mvc;

namespace Api.Common.Base {
    public class BaseController : Controller {

        public string _title = "";

        private UsuarioService usuarioService;

        public BaseController(UsuarioRepository usuarioRepository) {
            this.usuarioService = new UsuarioService(usuarioRepository);
        }

        public UsuarioInfoVM getLogged() {
            var usuarioId = this.User.FindFirst(x => x.Type == "usuario-info-id").Value;
            return this.usuarioService.Detail(usuarioId).UsuarioInfo;
        }

    }
}