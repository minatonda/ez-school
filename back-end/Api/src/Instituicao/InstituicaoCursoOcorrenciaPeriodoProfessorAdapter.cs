using System.Collections.Generic;
using Api.CursoApi;
using Api.UsuarioApi;
using Domain.AreaInteresseDomain;
using Domain.InstituicaoDomain;

namespace Api.InstituicaoApi {

    public class InstituicaoCursoOcorrenciaPeriodoProfessorAdapter {

        public static InstituicaoCursoOcorrenciaPeriodoProfessorVM ToViewModel(InstituicaoCursoOcorrenciaPeriodoProfessor model, bool deep) {
            var vm = new InstituicaoCursoOcorrenciaPeriodoProfessorVM();
            vm.ID = model.ID;

            if (model.Professor != null) {
                vm.Professor = UsuarioAdapter.ToViewModel(model.Professor, new List<AreaInteresse>(), true);
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

            vm.Label = model.Professor.Nome;

            return vm;
        }

        public static InstituicaoCursoOcorrenciaPeriodoProfessor ToModel(InstituicaoCursoOcorrenciaPeriodoProfessorVM vm, bool deep) {
            var model = new InstituicaoCursoOcorrenciaPeriodoProfessor();
            model.ID = vm.ID;

            if (vm.Professor != null) {
                model.Professor = UsuarioAdapter.ToModel(vm.Professor, true);
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