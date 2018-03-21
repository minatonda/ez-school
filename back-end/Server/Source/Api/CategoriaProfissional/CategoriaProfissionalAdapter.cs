using Domain.CategoriaProfissionalDomain;

namespace Api.CategoriaProfissionalApi {

    public class CategoriaProfissionalAdapter {

        public static CategoriaProfissionalVM ToViewModel(CategoriaProfissional model, bool deep) {
            var vm = new CategoriaProfissionalVM();
            vm.ID = model.ID;
            vm.Nome = model.Nome;
            vm.Descricao = model.Descricao;

            vm.Label = model.Nome;

            return vm;
        }

        public static CategoriaProfissional ToModel(CategoriaProfissionalVM vm, bool deep) {
            var model = new CategoriaProfissional();
            model.ID = vm.ID;
            model.Nome = vm.Nome;
            model.Descricao = vm.Descricao;

            return model;
        }

    }
}