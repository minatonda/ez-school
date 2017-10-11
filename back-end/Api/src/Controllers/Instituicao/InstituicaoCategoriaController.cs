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

        private InstituicaoCategoriaService _InstituicaoCategoriaCategoriaRepository;
        public InstituicaoCategoriaCategoriaController(InstituicaoCategoriaRepository InstituicaoCategoriaRepository)
        {
            this._InstituicaoCategoriaCategoriaRepository = new InstituicaoCategoriaService(InstituicaoCategoriaRepository);
        }
        [HttpGet]
        public List<InstituicaoCategoriaVM> Get()
        {
            return this._InstituicaoCategoriaCategoriaRepository.GetAll();
        }
        [HttpGet("{id}")]
        public InstituicaoCategoriaVM GetDetail(long id)
        {
            return this.GetDetail(id);
        }
        [HttpPut("add")]
        public InstituicaoCategoriaVM Put([FromBody] InstituicaoCategoriaVM viewModel)
        {
            return this._InstituicaoCategoriaCategoriaRepository.Add(viewModel);
        }
        [HttpPost("upd")]
        public InstituicaoCategoriaVM Post([FromBody] InstituicaoCategoriaVM viewModel)
        {
           return this._InstituicaoCategoriaCategoriaRepository.Update(viewModel);
        }
        [HttpDelete("del")]
        public void Delete([FromQuery] long id)
        {
            this._InstituicaoCategoriaCategoriaRepository.Delete(id);
        }
    }
}