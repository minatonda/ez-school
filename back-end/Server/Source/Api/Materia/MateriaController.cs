using System.Collections.Generic;
using Api.Common.Base;
using Api.InstituicaoApi;
using Domain.CursoDomain;
using Domain.InstituicaoDomain;
using Domain.MateriaDomain;
using Domain.UsuarioDomain;
using Microsoft.AspNetCore.Mvc;

namespace Api.MateriaApi {

    [Route("api/materia")]
    public class MateriaController : BaseController {

        private MateriaService _materiaService;

        public MateriaController(MateriaRepository materiaRepository, UsuarioRepository usuarioRepository, AreaInteresseRepository areaInteresseRepository, InstituicaoRepository instituicaoRepository, CursoRepository cursoRepository) : base(usuarioRepository, areaInteresseRepository, instituicaoRepository, cursoRepository) {
            this._materiaService = new MateriaService(materiaRepository);
        }

        [HttpPut("add")]
        public void Add([FromBody] MateriaVM viewModel) {
            if (!this.IsAuthorized(BaseRole.ADD_MATERIA)) {
                throw new BaseUnauthorizedException(BaseRole.ADD_MATERIA);
            }
            this._materiaService.Add(viewModel);
        }

        [HttpPost("update")]
        public void Update([FromBody] MateriaVM viewModel) {
            if (!this.IsAuthorized(BaseRole.EDIT_MATERIA)) {
                throw new BaseUnauthorizedException(BaseRole.EDIT_MATERIA);
            }
            this._materiaService.Update(viewModel);
        }

        [HttpDelete("disable")]
        public void Disable([FromQuery] long id) {
            if (!this.IsAuthorized(BaseRole.DISABLE_CATEGORIA_PROFISSIONAL)) {
                throw new BaseUnauthorizedException(BaseRole.DISABLE_CATEGORIA_PROFISSIONAL);
            }
            this._materiaService.Disable(id);
        }

        [HttpGet("detail/{id}")]
        public MateriaVM Detail(long id) {
            return this._materiaService.Detail(id);
        }

        [HttpGet]
        public List<MateriaVM> All() {
            return this._materiaService.All();
        }
    }
}