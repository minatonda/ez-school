using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Service;
using Api.Data.ViewModels;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers {
    [Route ("api/professor")]
    public class ProfessorController : Controller {

        private ProfessorService _professorService;
        public ProfessorController (ProfessorRepository professorRepository) {
            this._professorService = new ProfessorService(professorRepository);
        }
        [HttpGet]
        public List<ProfessorVM> All () {
            return this._professorService.All();
        }
        [HttpGet ("{id}")]
        public ProfessorVM Detail (string id) {
            return this._professorService.Detail (id);
        }
        [HttpPut ("add")]
        public ProfessorVM Add ([FromBody] ProfessorVM viewModel) {
            return this._professorService.Add(viewModel);
        }
        [HttpPost ("update")]
        public ProfessorVM Update ([FromBody] ProfessorVM viewModel) {
             return this._professorService.Update(viewModel);
        }
        [HttpDelete ("disable")]
        public void Disable ([FromQuery] long id) {
            this._professorService.Disable (id);
        }
    }
}