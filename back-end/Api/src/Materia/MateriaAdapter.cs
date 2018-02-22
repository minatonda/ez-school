using Api.Common.ViewModels;
using Domain.MateriaDomain;

namespace Api.MateriaApi {
    
    public class MateriaAdapter {

        public static MateriaVM ToViewModel(Materia model, bool deep) {
            var vm = new MateriaVM();
            vm.ID = model.ID.ToString();
            vm.Label = model.Nome;

            vm.Nome = model.Nome;
            vm.Descricao = model.Descricao;

            return vm;
        }

        public static Materia ToModel(MateriaVM vm, bool deep) {
            var model = new Materia();
            if (vm.ID != null) {
                model.ID = long.Parse(vm.ID);
            }

            model.Nome = vm.Nome;
            model.Descricao = vm.Descricao;

            return model;
        }

    }
}