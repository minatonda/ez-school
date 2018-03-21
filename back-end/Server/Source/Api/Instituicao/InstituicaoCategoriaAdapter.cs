using Domain.InstituicaoDomain;

namespace Api.InstituicaoApi {
    public class InstituicaoCategoriaAdapter {

        public static InstituicaoCategoriaVM ToViewModel(InstituicaoCategoria model, bool deep) {
            var vm = new InstituicaoCategoriaVM();
            vm.ID = model.ID;
            vm.Nome = model.Nome;
            vm.Descricao = model.Descricao;

            vm.Label = model.Nome;

            return vm;
        }

        public static InstituicaoCategoria ToModel(InstituicaoCategoriaVM vm, bool deep) {
            var model = new InstituicaoCategoria();
            model.ID = vm.ID;
            model.Nome = vm.Nome;
            model.Descricao = vm.Descricao;

            return model;
        }

    }
}