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
    public class InstituicaoCategoriaService
    {
        private InstituicaoCategoriaRepository _instituicaoCategoriaRepository;
        public InstituicaoCategoriaService(InstituicaoCategoriaRepository instituicaoCategoriaRepository)
        {
            this._instituicaoCategoriaRepository = instituicaoCategoriaRepository;
        }
        public List<InstituicaoCategoriaVM> All()
        {
            return this._instituicaoCategoriaRepository.GetAll(true).Select(x => InstituicaoCategoriaAdapter.ToViewModel(x, true)).ToList();
        }
        public InstituicaoCategoriaVM Detail(long id)
        {
            return InstituicaoCategoriaAdapter.ToViewModel(this._instituicaoCategoriaRepository.Get(id), true);
        }
        public InstituicaoCategoriaVM Add(InstituicaoCategoriaVM viewModel)
        {
            var model = InstituicaoCategoriaAdapter.ToModel(viewModel, true);
            return InstituicaoCategoriaAdapter.ToViewModel(this._instituicaoCategoriaRepository.Add(model), true);
        }
        public InstituicaoCategoriaVM Update(InstituicaoCategoriaVM viewModel)
        {
            var model = InstituicaoCategoriaAdapter.ToModel(viewModel, true);
            return InstituicaoCategoriaAdapter.ToViewModel(this._instituicaoCategoriaRepository.Update(model), true);
        }
        public void Disable(long id)
        {
            this._instituicaoCategoriaRepository.Disable(id);
        }

    }
}