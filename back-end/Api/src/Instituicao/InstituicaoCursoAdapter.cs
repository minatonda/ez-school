using System.Collections.Generic;
using System.Linq;
using Api.CursoApi;
using Domain.InstituicaoDomain;

namespace Api.InstituicaoApi {
    public class InstituicaoCursoAdapter {

        public static InstituicaoCursoVM ToViewModel(InstituicaoCurso model, bool deep) {
            var vm = new InstituicaoCursoVM();
            vm.ID = model.ID.ToString();

            vm.Label = model.Curso.Nome;
            vm.DataInicio = model.DataInicio;
            vm.DataExpiracao = model.DataExpiracao;

            if (model.Curso != null) {
                vm.Curso = CursoAdapter.ToViewModel(model.Curso, false);
            }
            if (model.CursoGrade != null) {
                vm.CursoGrade = CursoGradeAdapter.ToViewModel(model.CursoGrade, false);
            }

            return vm;
        }

        public static InstituicaoCurso ToModel(InstituicaoCursoVM vm, bool deep) {
            var model = new InstituicaoCurso();
            if (vm.ID != null) {
                model.ID = long.Parse(vm.ID);
            }
            model.DataInicio = vm.DataInicio;
            model.DataExpiracao = vm.DataExpiracao;

            if (vm.Curso != null) {
                model.Curso = CursoAdapter.ToModel(vm.Curso, false);
            }

            if (vm.CursoGrade != null) {
                model.CursoGrade = CursoGradeAdapter.ToModel(vm.CursoGrade, false);
            }

            return model;
        }

        public static List<InstituicaoCursoPeriodo> InstituicaoCursoPeriodoFromVM(InstituicaoCursoVM vm) {
            return vm.Periodos.Select(x => InstituicaoCursoPeriodoAdapter.ToModel(x, true)).ToList();
        }

        public static List<InstituicaoCursoTurma> InstituicaoCursoTurmaFromVM(InstituicaoCursoVM vm) {
            return vm.Turmas.Select(x => InstituicaoCursoTurmaAdapter.ToModel(x, true)).ToList();
        }

    }
}