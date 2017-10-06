using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.ViewModels;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers {
    [Route ("api/professor")]
    public class ProfessorController : Controller {

        private ProfessorRepository _professorRepository;
        public ProfessorController (ProfessorRepository professorRepository) {
            this._professorRepository = professorRepository;
        }
        [HttpGet]
        public List<ProfessorVM> Get () {
            return this._professorRepository.GetAll (true).Select (x => ProfessorAdapter.ToViewModel (x, true)).ToList ();
        }
        [HttpGet ("sht")]
        public List<ShortVM> GetShort () {
            return this._professorRepository.GetAll (true).Select (x => ProfessorAdapter.ToViewModelShort (x)).ToList ();
        }
        [HttpGet ("{id}")]
        public ProfessorVM GetDetail (long id) {
            return ProfessorAdapter.ToViewModel (this._professorRepository.Get (id), true);
        }
        [HttpPut ("add")]
        public ProfessorVM Put ([FromBody] ProfessorVM viewModel) {
            var model = ProfessorAdapter.ToModel (viewModel, true);
            return ProfessorAdapter.ToViewModel (this._professorRepository.Add (model), true);
        }
        [HttpPost ("upd")]
        public ProfessorVM Post ([FromBody] ProfessorVM viewModel) {
            var model = ProfessorAdapter.ToModel (viewModel, true);
            return ProfessorAdapter.ToViewModel (this._professorRepository.Update (model), true);
        }
        [HttpDelete ("del")]
        public void Delete ([FromQuery] long id) {
            this._professorRepository.Disable (id);
        }
    }
}