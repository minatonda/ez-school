using Api.MateriaApi;
using Domain.CursoDomain;

namespace Api.CursoApi {

    public class CursoGradeMateriaAdapter {

        public static CursoGradeMateriaVM ToViewModel(CursoGradeMateria model, bool deep) {
            var vm = new CursoGradeMateriaVM();
            vm.ID = model.ID;
            vm.Descricao = model.Descricao;
            if (model.Materia != null) {
                vm.Materia = MateriaAdapter.ToViewModel(model.Materia, false);
            }

            vm.Label = model.Descricao;

            return vm;
        }

        public static CursoGradeMateria ToModel(CursoGradeMateriaVM vm, bool deep) {
            var model = new CursoGradeMateria();
            model.ID = vm.ID;
            model.Descricao = vm.Descricao;
            if (vm.Materia != null) {
                model.Materia = MateriaAdapter.ToModel(vm.Materia, false);
            }
            
            return model;
        }

    }
}