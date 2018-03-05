using System;
using System.Collections.Generic;
using Api.Common.Base;
using Domain.UsuarioDomain;
using Microsoft.AspNetCore.Mvc;

namespace Api.UsuarioApi {

    [Route("api/usuario")]
    public class UsuarioController : BaseController {

        private UsuarioService usuarioService;

        public UsuarioController(UsuarioRepository usuarioRepository, AreaInteresseRepository areaInteresseRepository) : base(usuarioRepository, areaInteresseRepository) {
            this.usuarioService = new UsuarioService(usuarioRepository, areaInteresseRepository);
        }

        [HttpPut("add")]
        public void Add([FromBody] UsuarioVM viewModel) {
            this.usuarioService.Add(viewModel);
        }

        [HttpPost("update")]
        public void Update([FromBody] UsuarioVM viewModel) {
            this.usuarioService.Update(viewModel);
        }

        [HttpPost("{id}/aluno/update")]
        public void UpdateAluno([FromBody] AlunoVM viewModel) {
            this.usuarioService.UpdateAluno(viewModel);
        }

        [HttpPost("{id}/professor/update")]
        public void UpdateProfessor([FromBody] ProfessorVM viewModel) {
            this.usuarioService.UpdateProfessor(viewModel);
        }

        [HttpDelete("disable")]
        public void Disable(string id) {
            this.usuarioService.Disable(id);
        }

        [HttpGet("{id}")]
        public UsuarioVM Detail(string id, [FromQuery]string termo) {
            var x = this.getLogged();
            return this.usuarioService.Detail(id);
        }

        [HttpGet("{id}/aluno")]
        public AlunoVM DetailAluno(string id) {
            return this.usuarioService.DetailAluno(id);
        }

        [HttpGet("{id}/professor")]
        public ProfessorVM DetailProfessor(string id) {
            return this.usuarioService.DetailProfessor(id);
        }

        [HttpGet]
        public List<UsuarioVM> All() {
            return this.usuarioService.All();
        }


    }
}