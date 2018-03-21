using Domain.InstituicaoDomain;

namespace Api.InstituicaoApi {

    public class InstituicaoAdapter {

        public static InstituicaoVM ToViewModel(Instituicao model, bool deep) {
            var vm = new InstituicaoVM();
            vm.ID = model.ID;
            vm.Nome = model.Nome;
            vm.CNPJ = model.CNPJ;

            vm.Label = model.Nome;

            return vm;
        }

        public static Instituicao ToModel(InstituicaoVM vm, bool deep) {
            var model = new Instituicao();
            model.ID = vm.ID;
            model.Nome = vm.Nome;
            model.CNPJ = vm.CNPJ;

            return model;
        }

    }
}