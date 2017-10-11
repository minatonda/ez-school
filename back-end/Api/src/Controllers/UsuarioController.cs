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
        private UsuarioService _usuarioRepository;
        public UsuarioController(UsuarioRepository usuarioRepository)
        {
            this._usuarioRepository = new UsuarioService(usuarioRepository);
        }
        [HttpGet]
        public List<UsuarioVM> Get()
        {
            return this._usuarioRepository.GetAll();
        }
        [HttpGet("{id}")]
        public UsuarioVM GetDetail(string id)
        {
            return this._usuarioRepository.GetDetail(id);
        }
        [HttpPut("add")]
        public UsuarioVM Put([FromBody] UsuarioVM viewModel)
        {
            return this._usuarioRepository.Add(viewModel);
        }
        [HttpPost("upd")]
        public UsuarioVM Post([FromBody] UsuarioVM viewModel)
        {
            return this._usuarioRepository.Update(viewModel);
        }
        [HttpDelete("del")]
        public void Delete(string id)
        {
            this._usuarioRepository.Delete(id);
        }
    }
}