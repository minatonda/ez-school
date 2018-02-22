using Domain.Models;

namespace Api.InstituicaoApi {

    public class InstituicaoAdapter {

        public static InstituicaoVM ToViewModel(Instituicao model, bool deep) {
            var vm = new InstituicaoVM();
            vm.ID = model.ID.ToString();
            vm.Label = model.Nome;

            vm.Nome = model.Nome;
            vm.CNPJ = model.CNPJ;

            return vm;
        }

        public static Instituicao ToModel(InstituicaoVM vm, bool deep) {
            var model = new Instituicao();
            if (vm.ID != null) {
                model.ID = long.Parse(vm.ID);
            }

            model.Nome = vm.Nome;
            model.CNPJ = vm.CNPJ;

            return model;
        }

    }
}