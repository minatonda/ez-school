using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Interface;
using Domain.Repositories;
using Api.Data.ViewModels;

namespace Api.Data.Service {
    public class CategoriaProfissionalService {

        private CategoriaProfissionalRepository _categoriaProfissionalRepository;

        public CategoriaProfissionalService(CategoriaProfissionalRepository categoriaProfissionalRepository) {
            this._categoriaProfissionalRepository = categoriaProfissionalRepository;
        }

        public void Add(CategoriaProfissionalVM viewModel) {
            var model = CategoriaProfissionalAdapter.ToModel(viewModel, true);
            this._categoriaProfissionalRepository.Add(model);
            this._categoriaProfissionalRepository.SaveChanges();
        }

        public void Update(CategoriaProfissionalVM viewModel) {
            var model = CategoriaProfissionalAdapter.ToModel(viewModel, true);
            this._categoriaProfissionalRepository.Update(model);
            this._categoriaProfissionalRepository.SaveChanges();
        }

        public void Disable(long id) {
            this._categoriaProfissionalRepository.Disable(id);
            this._categoriaProfissionalRepository.SaveChanges();
        }

        public CategoriaProfissionalVM Detail(long id) {
            return CategoriaProfissionalAdapter.ToViewModel(this._categoriaProfissionalRepository.Get(id), true);
        }

        public List<CategoriaProfissionalVM> All() {
            return this._categoriaProfissionalRepository.GetAll(true).Select(x => CategoriaProfissionalAdapter.ToViewModel(x, true)).ToList();
        }
    }
}