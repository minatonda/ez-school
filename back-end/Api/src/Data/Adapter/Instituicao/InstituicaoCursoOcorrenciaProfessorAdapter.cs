using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Api.Data.ViewModels {
    public class InstituicaoCursoOcorrenciaProfessorAdapter {

        public static InstituicaoCursoOcorrenciaProfessorVM ToViewModel(InstituicaoCursoOcorrenciaProfessor model, List<InstituicaoCursoOcorrenciaProfessorPeriodoAula> instituicaoCursoOcorrenciaProfessorPeriodoAulas, bool deep) {
            var vm = new InstituicaoCursoOcorrenciaProfessorVM();

            vm.ID = model.ID.ToString();

            if (model.Turma != null) {
                vm.Turma = InstituicaoCursoTurmaAdapter.ToViewModel(model.Turma, false);
            }

            if (model.Professor != null) {
                vm.Professor = ProfessorAdapter.ToViewModel(model.Professor, null, true);
            }

            if (model.Materia != null) {
                vm.Materia = CursoGradeMateriaAdapter.ToViewModel(model.Materia, false);
            }

            if (model.Periodo != null) {
                vm.Periodo = InstituicaoCursoPeriodoAdapter.ToViewModel(model.Periodo, false);
            }

            vm.Label = vm.Professor.UsuarioInfo.Nome;

            return vm;
        }

        public static InstituicaoCursoOcorrenciaProfessor ToModel(InstituicaoCursoOcorrenciaProfessorVM vm, bool deep) {
            var model = new InstituicaoCursoOcorrenciaProfessor();

            if (vm.ID != null) {
                model.ID = long.Parse(vm.ID);
            }

            if (vm.Turma != null) {
                model.Turma = InstituicaoCursoTurmaAdapter.ToModel(vm.Turma, false);
            }

            if (vm.Professor != null) {
                model.Professor = ProfessorAdapter.ToModel(vm.Professor, true);
            }

            if (vm.Materia != null) {
                model.Materia = CursoGradeMateriaAdapter.ToModel(vm.Materia, false);
            }

            if (vm.Periodo != null) {
                model.Periodo = InstituicaoCursoPeriodoAdapter.ToModel(vm.Periodo, false);
            }

            return model;
        }

        public static List<InstituicaoCursoOcorrenciaProfessorPeriodoAula> InstituicaoCursoOcorrenciaProfessorPeriodoAulasFrom(InstituicaoCursoOcorrenciaProfessorVM vm) {
            return vm.PeriodosAula.Select(x => InstituicaoCursoOcorrenciaProfessorPeriodoAulaAdapter.ToModel(x, true)).ToList();
        }

    }
}