using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Api.Data.Service;
using Api.Data.ViewModels;
using Domain.Repositories;
using System;

namespace Api.Controllers {
    [Route("api/usuario")]
    public class UsuarioController : Controller {

        private UsuarioService usuarioService;

        public UsuarioController(UsuarioRepository usuarioRepository, AreaInteresseRepository areaInteresseRepository) {
            this.usuarioService = new UsuarioService(usuarioRepository, areaInteresseRepository);
        }

        [HttpGet]
        public List<UsuarioVM> All() {
            return this.usuarioService.All();
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

        [HttpGet("{id}/{term?}")]
        public Object Detail(string id, [FromQuery]string termo) {
            switch (id.ToLower()) {
                case ("professor"): {
                        if (termo != null) {
                            return this.usuarioService.GetAllProfessoresByTermo(termo);
                        } else {
                            return new List<Object>();
                        }
                    }
                case ("aluno"): {
                        if (termo != null) {
                            return this.usuarioService.GetAllAlunosByTermo(termo);
                        } else {
                            return new List<Object>();
                        }
                    }
                default: {
                        return this.usuarioService.Detail(id);
                    }
            }
        }

        [HttpGet("{id}/aluno")]
        public AlunoVM DetailAluno(string id) {
            return this.usuarioService.DetailAluno(id);
        }

        [HttpGet("{id}/professor")]
        public ProfessorVM DetailProfessor(string id) {
            return this.usuarioService.DetailProfessor(id);
        }

    }
}