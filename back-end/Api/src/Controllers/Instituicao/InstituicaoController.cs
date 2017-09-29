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
    [Route("api/instituicao")]
    public class InstituicaoController : Controller
    {

        private InstituicaoService _instituicaoRepository;
        public InstituicaoController(InstituicaoRepository instituicaoRepository)
        {
            this._instituicaoRepository = new InstituicaoService(instituicaoRepository);
        }
        [HttpGet]
        public List<InstituicaoVM> Get()
        {
            return this._instituicaoRepository.GetAll();
        }
        [HttpGet("sht")]
        public List<ShortVM> GetShort()
        {
            return this._instituicaoRepository.GetAllShort();
        }
        [HttpGet("{id}")]
        public InstituicaoVM GetDetail(long id)
        {
            return this._instituicaoRepository.GetDetail(id);
        }
        [HttpPut("add")]
        public InstituicaoVM Put([FromBody] InstituicaoVM viewModel)
        {
            return this._instituicaoRepository.Add(viewModel);
        }
        [HttpPost("upd")]
        public InstituicaoVM Post([FromBody] InstituicaoVM viewModel)
        {
            return this._instituicaoRepository.Update(viewModel);
        }
        [HttpDelete("del")]
        public void Delete([FromQuery]long id)
        {
            this._instituicaoRepository.Delete(id);
        }

        [HttpGet("{id}/categorias")]
        public List<InstituicaoCategoriaVM> GetCategorias(long id)
        {
            return this._instituicaoRepository.GetCategorias(id);
        }
        [HttpGet("{id}/categorias/add")]
        public void AddCategorias(long id, [FromBody] InstituicaoCategoriaVM viewModel)
        {
            this._instituicaoRepository.AddCategoria(id, viewModel);
        }
        [HttpGet("{id}/categorias/del")]
        public void DeleteCategorias(long id, [FromQuery] long idCategoria)
        {
            this._instituicaoRepository.DeleteCategoria(id, idCategoria);
        }
    }
}