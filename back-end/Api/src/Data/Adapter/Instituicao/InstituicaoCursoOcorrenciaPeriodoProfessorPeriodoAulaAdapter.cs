using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Api.Data.ViewModels {
    public class InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaAdapter {

        public static InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaVM ToViewModel(InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula model, List<InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula> instituicaoCursoOcorrenciaProfessorPeriodoAulas, bool deep) {
            var vm = new InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaVM();

            vm.ID = model.ID.ToString();
            vm.Inicio = model.Inicio;
            vm.Fim = model.Fim;
            vm.Dia = model.Dia;

            return vm;
        }

        public static InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula ToModel(InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaVM vm, bool deep) {
            var model = new InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula();

            if (vm.ID != null) {
                model.ID = long.Parse(vm.ID);
            }
            model.Inicio = vm.Inicio;
            model.Fim = vm.Fim;
            model.Dia = vm.Dia;

            return model;
        }

        public static InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula ToModel(InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaVM vm, InstituicaoCursoOcorrenciaPeriodoProfessorVM instituicaoCursoOcorrenciaPeriodoProfessor, bool deep) {
            var model = InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAulaAdapter.ToModel(vm, true);
            model.InstituicaoCursoOcorrenciaPeriodoProfessor = InstituicaoCursoOcorrenciaPeriodoProfessorAdapter.ToModel(instituicaoCursoOcorrenciaPeriodoProfessor, true);

            return model;
        }

    }
}