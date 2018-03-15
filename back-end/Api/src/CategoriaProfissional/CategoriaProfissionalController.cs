using System.Collections.Generic;
using Api.Common.Base;
using Domain.CategoriaProfissionalDomain;
using Domain.CursoDomain;
using Domain.InstituicaoDomain;
using Domain.UsuarioDomain;
using Microsoft.AspNetCore.Mvc;

namespace Api.CategoriaProfissionalApi {

    [Route("api/categoria-profissional")]
    public class CategoriaProfissionalController : BaseController {

        private CategoriaProfissionalService _categoriaProfissionalService;
        public CategoriaProfissionalController(CategoriaProfissionalRepository categoriaProfissionalRepository, UsuarioRepository usuarioRepository, AreaInteresseRepository areaInteresseRepository, InstituicaoRepository instituicaoRepository, CursoRepository cursoRepository) : base(usuarioRepository, areaInteresseRepository, instituicaoRepository, cursoRepository) {
            this._categoriaProfissionalService = new CategoriaProfissionalService(categoriaProfissionalRepository);
        }

        [HttpPut("add")]
        public void Add([FromBody] CategoriaProfissionalVM viewModel) {
            if (!this.IsAuthorized(BaseRole.ADD_CATEGORIA_PROFISSIONAL)) {
                throw new BaseUnauthorizedException(BaseRole.ADD_CATEGORIA_PROFISSIONAL);
            }
            this._categoriaProfissionalService.Add(viewModel);
        }

        [HttpPost("update")]
        public void Update([FromBody] CategoriaProfissionalVM viewModel) {
            if (!this.IsAuthorized(BaseRole.EDIT_CATEGORIA_PROFISSIONAL )) {
                throw new BaseUnauthorizedException(BaseRole.EDIT_CATEGORIA_PROFISSIONAL);
            }
            this._categoriaProfissionalService.Update(viewModel);
        }

        [HttpDelete("disable")]
        public void Disable([FromQuery] long id) {
            if (!this.IsAuthorized(BaseRole.DISABLE_CATEGORIA_PROFISSIONAL)) {
                throw new BaseUnauthorizedException(BaseRole.DISABLE_CATEGORIA_PROFISSIONAL);
            }
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