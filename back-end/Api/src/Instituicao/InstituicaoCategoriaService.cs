using System.Collections.Generic;
using System.Linq;
using Domain.Repositories;

namespace Api.InstituicaoApi {
    
    public class InstituicaoCategoriaService {

        private InstituicaoCategoriaRepository _instituicaoCategoriaRepository;

        public InstituicaoCategoriaService(InstituicaoCategoriaRepository instituicaoCategoriaRepository) {
            this._instituicaoCategoriaRepository = instituicaoCategoriaRepository;
        }

        public void Add(InstituicaoCategoriaVM viewModel) {
            var model = InstituicaoCategoriaAdapter.ToModel(viewModel, true);
            this._instituicaoCategoriaRepository.Add(model);
            this._instituicaoCategoriaRepository.SaveChanges();
        }

        public void Update(InstituicaoCategoriaVM viewModel) {
            var model = InstituicaoCategoriaAdapter.ToModel(viewModel, true);
            this._instituicaoCategoriaRepository.Update(model);

            this._instituicaoCategoriaRepository.SaveChanges();

        }

        public void Disable(long id) {
            this._instituicaoCategoriaRepository.Disable(id);
            this._instituicaoCategoriaRepository.SaveChanges();
        }

        public InstituicaoCategoriaVM Detail(long id) {
            return InstituicaoCategoriaAdapter.ToViewModel(this._instituicaoCategoriaRepository.Get(id), true);
        }

        public List<InstituicaoCategoriaVM> All() {
            return this._instituicaoCategoriaRepository.GetAll(true).Select(x => InstituicaoCategoriaAdapter.ToViewModel(x, true)).ToList();
        }
    }
}