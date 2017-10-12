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

        private ProfessorService _professorRepository;
        public ProfessorController (ProfessorRepository professorRepository) {
            this._professorRepository = new ProfessorService(professorRepository);
        }
        [HttpGet]
        public List<ProfessorVM> Get () {
            return this._professorRepository.GetAll();
        }
        [HttpGet ("{id}")]
        public ProfessorVM GetDetail (string id) {
            return this._professorRepository.GetDetail (id);
        }
        [HttpPut ("add")]
        public ProfessorVM Put ([FromBody] ProfessorVM viewModel) {
            return this._professorRepository.Add(viewModel);
        }
        [HttpPost ("upd")]
        public ProfessorVM Post ([FromBody] ProfessorVM viewModel) {
             return this._professorRepository.Update(viewModel);
        }
        [HttpDelete ("del")]
        public void Delete ([FromQuery] long id) {
            this._professorRepository.Disable (id);
        }
    }
}