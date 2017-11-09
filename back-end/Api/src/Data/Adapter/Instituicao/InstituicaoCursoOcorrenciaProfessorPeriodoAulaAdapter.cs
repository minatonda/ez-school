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

        public static InstituicaoCursoOcorrenciaProfessorPeriodoAulaVM ToViewModel(InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula model, bool deep) {
            var vm = new InstituicaoCursoOcorrenciaProfessorPeriodoAulaVM();
            vm.ID = model.ID.ToString();
            vm.Dia = model.Dia;
            vm.Inicio = model.Inicio;
            vm.Fim = model.Fim;

            vm.Label += (model.Inicio + " - " + model.Fim);
            switch (model.Dia) {
                case (DayOfWeek.Sunday): {
                        vm.Label = "Domingo - ";
                        break;
                    }
                case (DayOfWeek.Monday): {
                        vm.Label = "Segunda - ";
                        break;
                    }
                case (DayOfWeek.Tuesday): {
                        vm.Label = "Terça - ";
                        break;
                    }
                case (DayOfWeek.Wednesday): {
                        vm.Label = "Quarta - ";
                        break;
                    }
                case (DayOfWeek.Thursday): {
                        vm.Label = "Quinta - ";
                        break;
                    }
                case (DayOfWeek.Friday): {
                        vm.Label = "Sexta - ";
                        break;
                    }
                case (DayOfWeek.Saturday): {
                        vm.Label = "Sábado - ";
                        break;
                    }
            }
            return vm;
        }

        public static InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula ToModel(InstituicaoCursoOcorrenciaProfessorPeriodoAulaVM vm, bool deep) {
            var model = new InstituicaoCursoOcorrenciaPeriodoProfessorPeriodoAula();
            
            if (vm.ID != null) {
                model.ID = long.Parse(vm.ID);
            }
            
            model.Dia = vm.Dia;
            model.Inicio = vm.Inicio;
            model.Fim = vm.Fim;
            
            return model;
        }

    }
}