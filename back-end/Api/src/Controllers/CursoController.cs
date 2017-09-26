using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.ViewModels;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers {
    [Route ("api/curso")]
    public class CursoController : Controller {

        private CursoRepository _cursoRepository;
        public CursoController (CursoRepository cursoRepository) {
            this._cursoRepository = cursoRepository;
        }
        [HttpGet]
        public List<CursoVM> Get () {
            return this._cursoRepository.GetAll (true).Select (x => CursoAdapter.ToViewModel (x, true)).ToList ();
        }
        [HttpGet ("sht/{id}")]
        public List<ShortVM> GetShort () {
            return this._cursoRepository.GetAll (true).Select (x => CursoAdapter.ToViewModelShort (x)).ToList ();
        }
        [HttpGet ("dtl/{id}")]
        public CursoVM GetDetail (long id) {
            return CursoAdapter.ToViewModel (this._cursoRepository.Get (id), true);
        }
        [HttpPut ("add")]
        public CursoVM Put ([FromBody] CursoVM viewModel) {
            var model = CursoAdapter.ToModel (viewModel, true);
            return CursoAdapter.ToViewModel (this._cursoRepository.Add (model), true);
        }
        [HttpPost ("upd")]
        public CursoVM Post ([FromBody] CursoVM viewModel) {
            var model = CursoAdapter.ToModel (viewModel, true);
            return CursoAdapter.ToViewModel (this._cursoRepository.Update (model), true);
        }
        [HttpDelete ("del/{id}")]
        public void Delete (long id) {
            this._cursoRepository.Delete (id);
        }
    }
}