using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Api.Data.Service;
using Api.Data.ViewModels;
using Domain.Repositories;

namespace Api.Controllers
{
    [Route("api/categoria-profissional")]
    public class CategoriaProfissionalController : Controller
    {

        private CategoriaProfissionalService _categoriaProfissionalService;
        public CategoriaProfissionalController(CategoriaProfissionalRepository categoriaProfissionalRepository)
        {
            this._categoriaProfissionalService = new CategoriaProfissionalService(categoriaProfissionalRepository);
        }
        [HttpGet]
        public List<CategoriaProfissionalVM> All()
        {
            return this._categoriaProfissionalService.All();
        }
        [HttpGet("{id}")]
        public CategoriaProfissionalVM Detail(long id)
        {
            return this._categoriaProfissionalService.Detail(id);
        }
        [HttpPut("add")]
        public CategoriaProfissionalVM Add([FromBody] CategoriaProfissionalVM viewModel)
        {
            return this._categoriaProfissionalService.Add(viewModel);
        }
        [HttpPost("update")]
        public CategoriaProfissionalVM Update([FromBody] CategoriaProfissionalVM viewModel)
        {
            return this._categoriaProfissionalService.Update(viewModel);
        }
        [HttpDelete("disable")]
        public void Disable([FromQuery] long id)
        {
            this._categoriaProfissionalService.Disable(id);
        }

    }
}