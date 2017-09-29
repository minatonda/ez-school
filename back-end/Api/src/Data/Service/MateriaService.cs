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
    public class MateriaService
    {

        private MateriaRepository _materiaRepository;

        public MateriaService(MateriaRepository materiaRepository)
        {
            this._materiaRepository = materiaRepository;
        }
        public List<MateriaVM> GetAll()
        {
            return this._materiaRepository.GetAll(true).Select(x => MateriaAdapter.ToViewModel(x, true)).ToList();
        }
        public List<ShortVM> GetAllShort()
        {
            return this._materiaRepository.GetAll(true).Select(x => MateriaAdapter.ToViewModelShort(x)).ToList();
        }
        public MateriaVM GetDetail(long id)
        {
            return MateriaAdapter.ToViewModel(this._materiaRepository.Get(id), true);
        }
        public MateriaVM Add(MateriaVM viewModel)
        {
            var model = MateriaAdapter.ToModel(viewModel, true);
            return MateriaAdapter.ToViewModel(this._materiaRepository.Add(model), true);
        }
        public MateriaVM Update(MateriaVM viewModel)
        {
            var model = MateriaAdapter.ToModel(viewModel, true);
            return MateriaAdapter.ToViewModel(this._materiaRepository.Update(model), true);
        }
        public void Delete(long id)
        {
            this._materiaRepository.Delete(id);
        }

    }
}