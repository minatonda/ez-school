using Api.CursoApi;
using Api.UsuarioApi;
using Domain.Models;

namespace Api.InstituicaoApi {

    public class InstituicaoCursoOcorrenciaPeriodoProfessorAdapter {

        public static InstituicaoCursoOcorrenciaPeriodoProfessorVM ToViewModel(InstituicaoCursoOcorrenciaPeriodoProfessor model, bool deep) {
            var vm = new InstituicaoCursoOcorrenciaPeriodoProfessorVM();

            vm.ID = model.ID.ToString();
            vm.Label = model.Professor.UsuarioInfo.Nome;

            if (model.Professor != null) {
                vm.Professor = ProfessorAdapter.ToViewModel(model.Professor, null, true);
            }

            if (model.CursoGradeMateria != null) {
                vm.CursoGradeMateria = CursoGradeMateriaAdapter.ToViewModel(model.CursoGradeMateria, true);
            }

            if (model.InstituicaoCursoPeriodo != null) {
                vm.InstituicaoCursoPeriodo = InstituicaoCursoPeriodoAdapter.ToViewModel(model.InstituicaoCursoPeriodo, true);
            }

            if (model.InstituicaoCursoTurma != null) {
                vm.InstituicaoCursoTurma = InstituicaoCursoTurmaAdapter.ToViewModel(model.InstituicaoCursoTurma, true);
            }

            return vm;
        }

        public static InstituicaoCursoOcorrenciaPeriodoProfessor ToModel(InstituicaoCursoOcorrenciaPeriodoProfessorVM vm, bool deep) {
            var model = new InstituicaoCursoOcorrenciaPeriodoProfessor();
            if (vm.ID != null) {
                model.ID = long.Parse(vm.ID);
            }

            if (vm.Professor != null) {
                model.Professor = ProfessorAdapter.ToModel(vm.Professor, true);
            }

            if (vm.CursoGradeMateria != null) {
                model.CursoGradeMateria = CursoGradeMateriaAdapter.ToModel(vm.CursoGradeMateria, true);
            }

            if (vm.InstituicaoCursoPeriodo != null) {
                model.InstituicaoCursoPeriodo = InstituicaoCursoPeriodoAdapter.ToModel(vm.InstituicaoCursoPeriodo, true);
            }

            if (vm.InstituicaoCursoTurma != null) {
                model.InstituicaoCursoTurma = InstituicaoCursoTurmaAdapter.ToModel(vm.InstituicaoCursoTurma, true);
            }

            return model;
        }

    }
}