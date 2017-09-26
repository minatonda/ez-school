using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.ViewModels;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers {
    [Route ("api/materia")]
    public class MateriaController : Controller {

        private MateriaRepository _materiaRepository;
        public MateriaController (MateriaRepository materiaRepository) {
            this._materiaRepository = materiaRepository;
        }
        [HttpGet]
        public List<MateriaVM> Get () {
            return this._materiaRepository.GetAll (true).Select (x => MateriaAdapter.ToViewModel (x, true)).ToList ();
        }
        [HttpGet ("sht/{id}")]
        public List<ShortVM> GetShort () {
            return this._materiaRepository.GetAll (true).Select (x => MateriaAdapter.ToViewModelShort (x)).ToList ();
        }
        [HttpGet ("dtl/{id}")]
        public MateriaVM GetDetail (long id) {
            return MateriaAdapter.ToViewModel (this._materiaRepository.Get (id), true);
        }
        [HttpPut ("add")]
        public MateriaVM Put ([FromBody] MateriaVM viewModel) {
            var model = MateriaAdapter.ToModel (viewModel, true);
            return MateriaAdapter.ToViewModel (this._materiaRepository.Add (model), true);
        }
        [HttpPost ("upd")]
        public MateriaVM Post ([FromBody] MateriaVM viewModel) {
            var model = MateriaAdapter.ToModel (viewModel, true);
            return MateriaAdapter.ToViewModel (this._materiaRepository.Update (model), true);
        }
        [HttpDelete ("del/{id}")]
        public void Delete (long id) {
            this._materiaRepository.Delete (id);
        }
    }
}