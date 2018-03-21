using Domain.MateriaDomain;

namespace Api.MateriaApi {

    public class MateriaRelacionamentoAdapter {

        public static MateriaRelacionamentoVM ToViewModel(MateriaRelacionamento model, bool deep) {
            var vm = new MateriaRelacionamentoVM();
            vm.ID = model.ID;

            if (model.MateriaPai != null) {
                vm.MateriaPai = MateriaAdapter.ToViewModel(model.MateriaPai, true);
            }

            return vm;
        }

        public static MateriaRelacionamento ToModel(MateriaRelacionamentoVM vm, bool deep) {
            var model = new MateriaRelacionamento();
            model.ID = vm.ID;

            if (vm.MateriaPai != null) {
                model.MateriaPai = MateriaAdapter.ToModel(vm.MateriaPai, true);
            }

            return model;
        }


    }
}