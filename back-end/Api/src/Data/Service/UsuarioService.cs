using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Interface;
using Domain.Repositories;
using Api.Data.ViewModels;

namespace Api.Data.Service
{
    public class UsuarioService
    {
        private UsuarioRepository _usuarioRepository;
        public UsuarioService(UsuarioRepository usuarioRepository)
        {
            this._usuarioRepository = usuarioRepository;
        }
        public List<UsuarioVM> GetAll()
        {
            return this._usuarioRepository.GetAll(true).Select(x => UsuarioAdapter.ToViewModel(x, true)).ToList();
        }
        public List<ShortVM> GetAllShort()
        {
            return this._usuarioRepository.GetAll(true).Select(x => UsuarioAdapter.ToViewModelShort(x)).ToList();
        }
        public UsuarioVM GetDetail(string id)
        {
            return UsuarioAdapter.ToViewModel(this._usuarioRepository.Get(id), true);
        }
        public UsuarioVM Add(UsuarioVM viewModel)
        {
            var model = UsuarioAdapter.ToModel(viewModel, true);
            return UsuarioAdapter.ToViewModel(this._usuarioRepository.Add(model), true);
        }
        public UsuarioVM Update(UsuarioVM viewModel)
        {
            var model = UsuarioAdapter.ToModel(viewModel, true);
            return UsuarioAdapter.ToViewModel(this._usuarioRepository.Update(model), true);
        }
        public void Delete(string id)
        {
            this._usuarioRepository.Delete(id);
        }

    }
}