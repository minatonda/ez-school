using System.Collections.Generic;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.InstituicaoApi {
    
    [Route("api/instituicao-categoria")]
    public class InstituicaoCategoriaController : Controller {

        private InstituicaoCategoriaService _InstituicaoCategoriaService;
        
        public InstituicaoCategoriaController(InstituicaoCategoriaRepository InstituicaoCategoriaRepository) {
            this._InstituicaoCategoriaService = new InstituicaoCategoriaService(InstituicaoCategoriaRepository);
        }

        [HttpPut("add")]
        public void Add([FromBody] InstituicaoCategoriaVM viewModel) {
            this._InstituicaoCategoriaService.Add(viewModel);
        }

        [HttpPost("update")]
        public void Update([FromBody] InstituicaoCategoriaVM viewModel) {
            this._InstituicaoCategoriaService.Update(viewModel);
        }

        [HttpDelete("disable")]
        public void Disable([FromQuery] long id) {
            this._InstituicaoCategoriaService.Disable(id);
        }

        [HttpGet("{id}")]
        public InstituicaoCategoriaVM Detail(long id) {
            return this._InstituicaoCategoriaService.Detail(id);
        }

        [HttpGet]
        public List<InstituicaoCategoriaVM> All() {
            return this._InstituicaoCategoriaService.All();
        }
    }
}