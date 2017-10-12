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
        public List<UsuarioVM> Get()
        {
            return this.usuarioService.GetAll();
        }
        [HttpGet("{id}")]
        public UsuarioVM GetDetail(string id)
        {
            return this.usuarioService.GetDetail(id);
        }
        [HttpPut("add")]
        public UsuarioVM Put([FromBody] UsuarioVM viewModel)
        {
            return this.usuarioService.Add(viewModel);
        }
        [HttpPost("upd")]
        public UsuarioVM Post([FromBody] UsuarioVM viewModel)
        {
            return this.usuarioService.Update(viewModel);
        }
        [HttpDelete("del")]
        public void Delete(string id)
        {
            this.usuarioService.Delete(id);
        }
    }
}