using System.Collections.Generic;
using Api.Common.Base;
using Domain.CategoriaProfissionalDomain;
using Domain.UsuarioDomain;
using Microsoft.AspNetCore.Mvc;

namespace Api.CategoriaProfissionalApi {

    [Route("api/categoria-profissional")]
    public class CategoriaProfissionalController : BaseController {

        private CategoriaProfissionalService _categoriaProfissionalService;
        public CategoriaProfissionalController(CategoriaProfissionalRepository categoriaProfissionalRepository, UsuarioRepository usuarioRepository) : base(usuarioRepository) {
            this._categoriaProfissionalService = new CategoriaProfissionalService(categoriaProfissionalRepository);
        }

        [HttpPut("add")]
        public void Add([FromBody] CategoriaProfissionalVM viewModel) {
            this._categoriaProfissionalService.Add(viewModel);
        }

        [HttpPost("update")]
        public void Update([FromBody] CategoriaProfissionalVM viewModel) {
            this._categoriaProfissionalService.Update(viewModel);
        }

        [HttpDelete("disable")]
        public void Disable([FromQuery] long id) {
            this._categoriaProfissionalService.Disable(id);
        }

        [HttpGet("{id}")]
        public CategoriaProfissionalVM Detail(long id) {
            return this._categoriaProfissionalService.Detail(id);
        }

        [HttpGet]
        public List<CategoriaProfissionalVM> All() {
            return this._categoriaProfissionalService.All();
        }

    }
}