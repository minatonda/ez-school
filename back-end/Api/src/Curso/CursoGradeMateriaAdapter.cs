using Api.MateriaApi;
using Domain.CursoDomain;

namespace Api.CursoApi {

    public class CursoGradeMateriaAdapter {

        public static CursoGradeMateriaVM ToViewModel(CursoGradeMateria model, bool deep) {
            var vm = new CursoGradeMateriaVM();
            vm.ID = model.ID;
            vm.Descricao = model.Descricao;
            vm.Materia = MateriaAdapter.ToViewModel(model.Materia, true);

            vm.Label = model.Descricao;

            return vm;
        }

        public static CursoGradeMateria ToModel(CursoGradeMateriaVM vm, bool deep) {
            var model = new CursoGradeMateria();
            model.ID = vm.ID;
            model.Descricao = vm.Descricao;
            model.Materia = MateriaAdapter.ToModel(vm.Materia, true);
            return model;
        }

    }
}