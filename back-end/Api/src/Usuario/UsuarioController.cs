using System;
using System.Collections.Generic;
using Domain.UsuarioDomain;
using Microsoft.AspNetCore.Mvc;

namespace Api.UsuarioApi {

    [Route("api/usuario")]
    public class UsuarioController : Controller {

        private UsuarioService usuarioService;

        public UsuarioController(UsuarioRepository usuarioRepository) {
            this.usuarioService = new UsuarioService(usuarioRepository);
        }

        [HttpGet]
        public List<UsuarioVM> All() {
            return this.usuarioService.All();
        }

        [HttpGet("{id}/{term?}")]
        public Object Detail(string id, [FromQuery]string termo) {
            switch (id.ToLower()) {
                case ("professor"): {
                        if (termo != null) {
                            return this.usuarioService.GetProfessoresByTermo(termo);
                        } else {
                            return new List<Object>();
                        }
                    }
                case ("aluno"): {
                        if (termo != null) {
                            return this.usuarioService.GetAlunosByTermo(termo);
                        } else {
                            return new List<Object>();
                        }
                    }
                default: {
                        return this.usuarioService.Detail(id);
                    }
            }
        }

        [HttpPut("add")]
        public UsuarioVM Add([FromBody] UsuarioVM viewModel) {
            return this.usuarioService.Add(viewModel);
        }

        [HttpPost("update")]
        public UsuarioVM Update([FromBody] UsuarioVM viewModel) {
            return this.usuarioService.Update(viewModel);
        }

        [HttpDelete("disable")]
        public void Disable(string id) {
            this.usuarioService.Disable(id);
        }

        [HttpGet("{id}/aluno")]
        public AlunoVM DetailAluno(string id) {
            return this.usuarioService.DetailAluno(id);
        }

        [HttpPost("{id}/aluno/update")]
        public AlunoVM UpdateAluno([FromBody] AlunoVM viewModel) {
            return this.usuarioService.UpdateAluno(viewModel);
        }

        [HttpGet("{id}/professor")]
        public ProfessorVM DetailProfessor(string id) {
            return this.usuarioService.DetailProfessor(id);
        }
        
        [HttpPost("{id}/professor/update")]
        public ProfessorVM UpdateProfessor([FromBody] ProfessorVM viewModel) {
            return this.usuarioService.UpdateProfessor(viewModel);
        }

    }
}