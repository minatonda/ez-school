using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.ViewModels;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers {
    [Route ("api/aluno")]
    public class AlunoController : Controller {

        private AlunoRepository _alunoRepository;
        public AlunoController (AlunoRepository alunoRepository) {
            this._alunoRepository = alunoRepository;
        }
        [HttpGet]
        public List<AlunoVM> Get () {
            return this._alunoRepository.GetAll (true).Select (x => AlunoAdapter.ToViewModel (x, true)).ToList ();
        }
        [HttpGet ("sht")]
        public List<SelectVM> GetShort () {
            return this._alunoRepository.GetAll (true).Select (x => AlunoAdapter.ToViewModelShort (x)).ToList ();
        }
        [HttpGet ("{id}")]
        public AlunoVM GetDetail (long id) {
            return AlunoAdapter.ToViewModel (this._alunoRepository.Get (id), true);
        }
        [HttpPut ("add")]
        public AlunoVM Put ([FromBody] AlunoVM viewModel) {
            var model = AlunoAdapter.ToModel (viewModel, true);
            return AlunoAdapter.ToViewModel (this._alunoRepository.Add (model), true);
        }
        [HttpPost ("upd")]
        public AlunoVM Post ([FromBody] AlunoVM viewModel) {
            var model = AlunoAdapter.ToModel (viewModel, true);
            return AlunoAdapter.ToViewModel (this._alunoRepository.Update (model), true);
        }
        [HttpDelete ("del")]
        public void Delete ([FromQuery] long id) {
            this._alunoRepository.Disable (id);
        }
    }
}