using Domain.Models;

namespace Api.MateriaApi {
    
    public class MateriaRelacionamentoAdapter {

        public static MateriaRelacionamentoVM ToViewModel(MateriaRelacionamento model, bool deep) {
            var vm = new MateriaRelacionamentoVM();
            vm.ID = model.ID.ToString();

            if(model.MateriaPai!=null){
                vm.MateriaPai = MateriaAdapter.ToViewModel(model.MateriaPai,true);
            }

            return vm;
        }

        public static MateriaRelacionamento ToModel(MateriaRelacionamentoVM vm, bool deep) {
            var model = new MateriaRelacionamento();
            if (vm.ID != null) {
                model.ID = long.Parse(vm.ID);
            }
            if (vm.MateriaPai != null) {
                model.MateriaPai = MateriaAdapter.ToModel(vm.MateriaPai, true);
            }

            return model;
        }
      

    }
}