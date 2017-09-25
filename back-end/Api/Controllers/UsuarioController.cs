using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.ViewModels;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers {
    [Route ("api/usuario")]
    public class UsuarioController : Controller {

        private UsuarioRepository _usuarioRepository;
        public UsuarioController (UsuarioRepository usuarioRepository) {
            this._usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public List<UsuarioVM> Get () {
            return this._usuarioRepository.GetAll (true).Select (x => UsuarioAdapter.ToViewModel (x, true)).ToList ();
        }

        [HttpGet ("sht/{id}")]
        public List<ShortVM> GetShort () {
            return this._usuarioRepository.GetAll (true).Select (x => UsuarioAdapter.ToViewModelShort (x)).ToList ();
        }

        [HttpGet ("dtl/{id}")]
        public UsuarioVM GetDetail (string id) {
            return UsuarioAdapter.ToViewModel (this._usuarioRepository.Get (id), true);
        }

        [HttpPut ("add")]
        public UsuarioVM Put ([FromBody] UsuarioVM viewModel) {
            var model = UsuarioAdapter.ToModel (viewModel, true);
            return UsuarioAdapter.ToViewModel (this._usuarioRepository.Add (model), true);
        }

        [HttpPost ("upd")]
        public UsuarioVM Post ([FromBody] UsuarioVM viewModel) {
            var model = UsuarioAdapter.ToModel (viewModel, true);
            return UsuarioAdapter.ToViewModel (this._usuarioRepository.Update (model), true);
        }

        [HttpDelete ("del/{id}")]
        public void Delete (string id) {
            this._usuarioRepository.Delete (id);
        }
    }
}