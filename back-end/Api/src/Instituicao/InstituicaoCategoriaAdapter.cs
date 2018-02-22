using Domain.InstituicaoDomain;

namespace Api.InstituicaoApi {
    public class InstituicaoCategoriaAdapter {

        public static InstituicaoCategoriaVM ToViewModel (InstituicaoCategoria model, bool deep) {
            var vm = new InstituicaoCategoriaVM ();
            vm.ID = model.ID.ToString ();

            vm.Label = model.Nome;
            vm.Nome = model.Nome;
            vm.Descricao = model.Descricao;

            return vm;
        }

        public static InstituicaoCategoria ToModel (InstituicaoCategoriaVM vm, bool deep) {
            var model = new InstituicaoCategoria ();
            if (vm.ID != null) {
                model.ID = long.Parse (vm.ID);
            }

            model.Nome = vm.Nome;
            model.Descricao = vm.Descricao;

            return model;
        }

    }
}