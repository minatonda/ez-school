using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Api.Data.Service;
using Api.Data.ViewModels;
using Domain.Repositories;

namespace Api.Controllers
{
    [Route("api/usuario")]
    public class UsuarioController : Controller
    {
        private UsuarioService usuarioService;
        public UsuarioController(UsuarioRepository usuarioRepository)
        {
            this.usuarioService = new UsuarioService(usuarioRepository);
        }
        [HttpGet]
        public List<UsuarioVM> All()
        {
            return this.usuarioService.All();
        }
        [HttpGet("{id}")]
        public UsuarioVM Detail(string id)
        {
            return this.usuarioService.Detail(id);
        }
        [HttpPut("add")]
        public UsuarioVM Add([FromBody] UsuarioVM viewModel)
        {
            return this.usuarioService.Add(viewModel);
        }
        [HttpPost("update")]
        public UsuarioVM Update([FromBody] UsuarioVM viewModel)
        {
            return this.usuarioService.Update(viewModel);
        }
        [HttpDelete("disable")]
        public void Disable(string id)
        {
            this.usuarioService.Disable(id);
        }
    }
}