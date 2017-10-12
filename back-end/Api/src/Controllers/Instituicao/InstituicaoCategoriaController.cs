using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Service;
using Api.Data.ViewModels;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/instituicao-categoria")]
    public class InstituicaoCategoriaCategoriaController : Controller
    {

        private InstituicaoCategoriaService _InstituicaoCategoriaCategoriaService;
        public InstituicaoCategoriaCategoriaController(InstituicaoCategoriaRepository InstituicaoCategoriaRepository)
        {
            this._InstituicaoCategoriaCategoriaService = new InstituicaoCategoriaService(InstituicaoCategoriaRepository);
        }
        [HttpGet]
        public List<InstituicaoCategoriaVM> All()
        {
            return this._InstituicaoCategoriaCategoriaService.All();
        }
        [HttpGet("{id}")]
        public InstituicaoCategoriaVM Detail(long id)
        {
            return this.Detail(id);
        }
        [HttpPut("add")]
        public InstituicaoCategoriaVM Add([FromBody] InstituicaoCategoriaVM viewModel)
        {
            return this._InstituicaoCategoriaCategoriaService.Add(viewModel);
        }
        [HttpPost("update")]
        public InstituicaoCategoriaVM Update([FromBody] InstituicaoCategoriaVM viewModel)
        {
           return this._InstituicaoCategoriaCategoriaService.Update(viewModel);
        }
        [HttpDelete("disable")]
        public void Disable([FromQuery] long id)
        {
            this._InstituicaoCategoriaCategoriaService.Disable(id);
        }
    }
}