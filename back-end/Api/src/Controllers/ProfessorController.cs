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
        public List<ProfessorVM> Get () {
            return this._professorService.GetAll();
        }
        [HttpGet ("{id}")]
        public ProfessorVM GetDetail (string id) {
            return this._professorService.GetDetail (id);
        }
        [HttpPut ("add")]
        public ProfessorVM Put ([FromBody] ProfessorVM viewModel) {
            return this._professorService.Add(viewModel);
        }
        [HttpPost ("upd")]
        public ProfessorVM Post ([FromBody] ProfessorVM viewModel) {
             return this._professorService.Update(viewModel);
        }
        [HttpDelete ("del")]
        public void Delete ([FromQuery] long id) {
            this._professorService.Disable (id);
        }
    }
}