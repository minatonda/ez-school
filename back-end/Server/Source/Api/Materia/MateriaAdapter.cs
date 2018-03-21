using Api.Common.Base;
using Domain.MateriaDomain;

namespace Api.MateriaApi {
    
    public class MateriaAdapter {

        public static MateriaVM ToViewModel(Materia model, bool deep) {
            var vm = new MateriaVM();
            vm.ID = model.ID;
            vm.Label = model.Nome;

            vm.Nome = model.Nome;
            vm.Descricao = model.Descricao;

            return vm;
        }

        public static Materia ToModel(MateriaVM vm, bool deep) {
            var model = new Materia();
            model.ID = vm.ID;

            model.Nome = vm.Nome;
            model.Descricao = vm.Descricao;

            return model;
        }

    }
}