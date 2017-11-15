using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Api.Data.ViewModels {
    public class InstituicaoCursoOcorrenciaPeriodoProfessorAdapter {

        public static InstituicaoCursoOcorrenciaProfessorVM ToViewModel(InstituicaoCursoOcorrenciaPeriodoProfessor model, List<InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula> instituicaoCursoOcorrenciaProfessorPeriodoAulas, bool deep) {
            var vm = new InstituicaoCursoOcorrenciaProfessorVM();

            vm.ID = model.ID.ToString();

            if (model.InstituicaoCursoTurma != null) {
                vm.Turma = InstituicaoCursoTurmaAdapter.ToViewModel(model.InstituicaoCursoTurma, false);
            }

            if (model.Professor != null) {
                vm.Professor = ProfessorAdapter.ToViewModel(model.Professor, null, true);
            }

            if (model.CursoGradeMateria != null) {
                vm.Materia = CursoGradeMateriaAdapter.ToViewModel(model.CursoGradeMateria, false);
            }

            if (model.InstituicaoCursoPeriodo != null) {
                vm.Periodo = InstituicaoCursoPeriodoAdapter.ToViewModel(model.InstituicaoCursoPeriodo, false);
            }

            vm.Label = vm.Professor.UsuarioInfo.Nome;

            return vm;
        }

        public static InstituicaoCursoOcorrenciaPeriodoProfessor ToModel(InstituicaoCursoOcorrenciaProfessorVM vm, bool deep) {
            var model = new InstituicaoCursoOcorrenciaPeriodoProfessor();

            if (vm.ID != null) {
                model.ID = long.Parse(vm.ID);
            }

            if (vm.Turma != null) {
                model.InstituicaoCursoTurma = InstituicaoCursoTurmaAdapter.ToModel(vm.Turma, false);
            }

            if (vm.Professor != null) {
                model.Professor = ProfessorAdapter.ToModel(vm.Professor, true);
            }

            if (vm.Materia != null) {
                model.CursoGradeMateria = CursoGradeMateriaAdapter.ToModel(vm.Materia, false);
            }

            if (vm.Periodo != null) {
                model.InstituicaoCursoPeriodo = InstituicaoCursoPeriodoAdapter.ToModel(vm.Periodo, false);
            }

            return model;
        }

        public static List<InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula> InstituicaoCursoOcorrenciaProfessorPeriodoAulasFrom(InstituicaoCursoOcorrenciaProfessorVM vm) {
            return vm.PeriodosAula.Select(x => InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaAdapter.ToModel(x, true)).ToList();
        }

    }
}