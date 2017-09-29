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
    public class InstituicaoService
    {
        private InstituicaoRepository _instituicaoRepository;
        public InstituicaoService(InstituicaoRepository instituicaoRepository)
        {
            this._instituicaoRepository = instituicaoRepository;
        }
        public List<InstituicaoVM> GetAll()
        {
            return this._instituicaoRepository.GetAll(true).Select(x => InstituicaoAdapter.ToViewModel(x, true)).ToList();
        }
        public List<ShortVM> GetAllShort()
        {
            return this._instituicaoRepository.GetAll(true).Select(x => InstituicaoAdapter.ToViewModelShort(x)).ToList();
        }
        public InstituicaoVM GetDetail(long id)
        {
            return InstituicaoAdapter.ToViewModel(this._instituicaoRepository.Get(id), true);
        }
        public InstituicaoVM Add(InstituicaoVM viewModel)
        {
            var model = InstituicaoAdapter.ToModel(viewModel, true);
            return InstituicaoAdapter.ToViewModel(this._instituicaoRepository.Add(model), true);
        }
        public InstituicaoVM Update(InstituicaoVM viewModel)
        {
            var model = InstituicaoAdapter.ToModel(viewModel, true);
            return InstituicaoAdapter.ToViewModel(this._instituicaoRepository.Update(model), true);
        }
        public void Delete(long id)
        {
            this._instituicaoRepository.Delete(id);
        }
        public List<InstituicaoCategoriaVM> GetCategorias(long id)
        {
            return this._instituicaoRepository.GetCategorias(id).Select(x => InstituicaoCategoriaAdapter.ToViewModel(x, false)).ToList();
        }
        public void AddCategoria(long id, InstituicaoCategoriaVM viewModel)
        {
            this._instituicaoRepository.AddCategoria(id, InstituicaoCategoriaAdapter.ToModel(viewModel, false));
        }
        public void DeleteCategoria(long id, long idCategoria)
        {
            this._instituicaoRepository.DeleteCategoria(id, idCategoria);
        }

    }
}