using System.Collections.Generic;
using Api.Common.Base;
using Domain.MateriaDomain;
using Domain.UsuarioDomain;
using Microsoft.AspNetCore.Mvc;

namespace Api.MateriaApi {

    [Route("api/materia")]
    public class MateriaController : BaseController {

        private MateriaService _materiaService;

        public MateriaController(MateriaRepository materiaRepository, UsuarioRepository usuarioRepository) : base(usuarioRepository) {
            this._materiaService = new MateriaService(materiaRepository);
        }

        [HttpPut("add")]
        public void Add([FromBody] MateriaVM viewModel) {
            this._materiaService.Add(viewModel);
        }

        [HttpPost("update")]
        public void Update([FromBody] MateriaVM viewModel) {
            this._materiaService.Update(viewModel);
        }

        [HttpDelete("disable")]
        public void Disable([FromQuery] long id) {
            this._materiaService.Disable(id);
        }

        [HttpGet("{id}")]
        public MateriaVM Detail(long id) {
            return this._materiaService.Detail(id);
        }

        [HttpGet]
        public List<MateriaVM> All() {
            return this._materiaService.All();
        }
    }
}