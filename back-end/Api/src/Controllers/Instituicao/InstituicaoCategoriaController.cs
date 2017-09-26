using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.ViewModels;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers {
    [Route ("api/instituicao-categoria")]
    public class InstituicaoCategoriaController : Controller {

        private InstituicaoRepository _instituicaoCategoriaRepository;
        public InstituicaoCategoriaController (InstituicaoRepository instituicaoCategoriaRepository) {
            this._instituicaoCategoriaRepository = instituicaoCategoriaRepository;
        }
        [HttpGet]
        public List<InstituicaoVM> Get () {
            return this._instituicaoCategoriaRepository.GetAll (true).Select (x => InstituicaoAdapter.ToViewModel (x, true)).ToList ();
        }
        [HttpGet ("sht/{id}")]
        public List<ShortVM> GetShort () {
            return this._instituicaoCategoriaRepository.GetAll (true).Select (x => InstituicaoAdapter.ToViewModelShort (x)).ToList ();
        }
        [HttpGet ("dtl/{id}")]
        public InstituicaoVM GetDetail (long id) {
            return InstituicaoAdapter.ToViewModel (this._instituicaoCategoriaRepository.Get (id), true);
        }
        [HttpPut ("add")]
        public InstituicaoVM Put ([FromBody] InstituicaoVM viewModel) {
            var model = InstituicaoAdapter.ToModel (viewModel, true);
            return InstituicaoAdapter.ToViewModel (this._instituicaoCategoriaRepository.Add (model), true);
        }
        [HttpPost ("upd")]
        public InstituicaoVM Post ([FromBody] InstituicaoVM viewModel) {
            var model = InstituicaoAdapter.ToModel (viewModel, true);
            return InstituicaoAdapter.ToViewModel (this._instituicaoCategoriaRepository.Update (model), true);
        }
        [HttpDelete ("del/{id}")]
        public void Delete (long id) {
            this._instituicaoCategoriaRepository.Delete (id);
        }
    }
}