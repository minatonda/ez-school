using Api.MateriaApi;
using Domain.CursoDomain;

namespace Api.CursoApi {

    public class CursoGradeMateriaAdapter {

        public static CursoGradeMateriaVM ToViewModel(CursoGradeMateria model, bool deep) {
            var vm = new CursoGradeMateriaVM();
            vm.ID = model.ID;
            vm.Descricao = model.Descricao;
            vm.Tags = model.Tags;
            vm.NomeExibicao = model.NomeExibicao;
            vm.NumeroAulas = model.NumeroAulas;
            vm.Grupo = model.Grupo;

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
            model.Tags = vm.Tags;
            model.NomeExibicao = vm.NomeExibicao;
            model.NumeroAulas = vm.NumeroAulas;
            model.Grupo = vm.Grupo;

            if (vm.Materia != null) {
                model.Materia = MateriaAdapter.ToModel(vm.Materia, false);
            }

            return model;
        }

    }
}