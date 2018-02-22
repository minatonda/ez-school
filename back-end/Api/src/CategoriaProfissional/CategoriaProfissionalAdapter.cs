using Domain.CategoriaProfissionalDomain;

namespace Api.CategoriaProfissionalApi {
    
    public class CategoriaProfissionalAdapter {

        public static CategoriaProfissionalVM ToViewModel (CategoriaProfissional model, bool deep) {
            var vm = new CategoriaProfissionalVM ();
            vm.ID = model.ID.ToString ();
            vm.Label = model.Nome;

            vm.Nome = model.Nome;
            vm.Descricao = model.Descricao;

            return vm;
        }

        public static CategoriaProfissional ToModel (CategoriaProfissionalVM vm, bool deep) {
            var model = new CategoriaProfissional ();
            if (vm.ID != null) {
                model.ID = long.Parse (vm.ID);
            }

            model.Nome = vm.Nome;
            model.Descricao = vm.Descricao;

            return model;
        }

    }
}