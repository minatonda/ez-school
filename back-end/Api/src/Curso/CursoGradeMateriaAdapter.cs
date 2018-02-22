using Api.MateriaApi;
using Domain.CursoDomain;

namespace Api.CursoApi {

    public class CursoGradeMateriaAdapter {

        public static CursoGradeMateriaVM ToViewModel(CursoGradeMateria model, bool deep) {
            var vm = new CursoGradeMateriaVM();

            vm.ID = model.ID.ToString();
            vm.Label = model.Descricao;

            vm.Descricao = model.Descricao;
            vm.Materia = MateriaAdapter.ToViewModel(model.Materia, true);

            return vm;
        }

        public static CursoGradeMateria ToModel(CursoGradeMateriaVM vm, bool deep) {
            var model = new CursoGradeMateria();
            if (vm.ID != null) {
                model.ID = long.Parse(vm.ID);
            }
            model.Descricao = vm.Descricao;
            model.Materia = MateriaAdapter.ToModel(vm.Materia, true);
            return model;
        }

    }
}