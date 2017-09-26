using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.ViewModels;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers {
    [Route ("api/instituicao")]
    public class InstituicaoController : Controller {

        private InstituicaoRepository _instituicaoRepository;
        public InstituicaoController (InstituicaoRepository instituicaoRepository) {
            this._instituicaoRepository = instituicaoRepository;
        }
        [HttpGet]
        public List<InstituicaoVM> Get () {
            return this._instituicaoRepository.GetAll (true).Select (x => InstituicaoAdapter.ToViewModel (x, true)).ToList ();
        }
        [HttpGet ("sht/{id}")]
        public List<ShortVM> GetShort () {
            return this._instituicaoRepository.GetAll (true).Select (x => InstituicaoAdapter.ToViewModelShort (x)).ToList ();
        }
        [HttpGet ("dtl/{id}")]
        public InstituicaoVM GetDetail (long id) {
            return InstituicaoAdapter.ToViewModel (this._instituicaoRepository.Get (id), true);
        }
        [HttpPut ("add")]
        public InstituicaoVM Put ([FromBody] InstituicaoVM viewModel) {
            var model = InstituicaoAdapter.ToModel (viewModel, true);
            return InstituicaoAdapter.ToViewModel (this._instituicaoRepository.Add (model), true);
        }
        [HttpPost ("upd")]
        public InstituicaoVM Post ([FromBody] InstituicaoVM viewModel) {
            var model = InstituicaoAdapter.ToModel (viewModel, true);
            return InstituicaoAdapter.ToViewModel (this._instituicaoRepository.Update (model), true);
        }
        [HttpDelete ("del/{id}")]
        public void Delete (long id) {
            this._instituicaoRepository.Delete (id);
        }
    }
}