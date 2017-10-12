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
        public List<InstituicaoCategoriaVM> Get()
        {
            return this._InstituicaoCategoriaCategoriaService.GetAll();
        }
        [HttpGet("{id}")]
        public InstituicaoCategoriaVM GetDetail(long id)
        {
            return this.GetDetail(id);
        }
        [HttpPut("add")]
        public InstituicaoCategoriaVM Put([FromBody] InstituicaoCategoriaVM viewModel)
        {
            return this._InstituicaoCategoriaCategoriaService.Add(viewModel);
        }
        [HttpPost("upd")]
        public InstituicaoCategoriaVM Post([FromBody] InstituicaoCategoriaVM viewModel)
        {
           return this._InstituicaoCategoriaCategoriaService.Update(viewModel);
        }
        [HttpDelete("del")]
        public void Delete([FromQuery] long id)
        {
            this._InstituicaoCategoriaCategoriaService.Delete(id);
        }
    }
}