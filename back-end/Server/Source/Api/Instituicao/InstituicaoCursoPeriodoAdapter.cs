using System;
using Domain.InstituicaoDomain;

namespace Api.InstituicaoApi {
    public class InstituicaoCursoPeriodoAdapter {

        public static InstituicaoCursoPeriodoVM ToViewModel(InstituicaoCursoPeriodo model, bool deep) {
            var vm = new InstituicaoCursoPeriodoVM();
            vm.ID = model.ID;
            vm.Inicio = model.Inicio;
            vm.Fim = model.Fim;
            vm.PausaInicio = model.PausaInicio;
            vm.PausaFim = model.PausaFim;
            vm.Quebras = model.Quebras;

            if (model.Dom) {
                vm.DiaSemana.Add(DayOfWeek.Sunday);
            }
            if (model.Seg) {
                vm.DiaSemana.Add(DayOfWeek.Monday);
            }
            if (model.Ter) {
                vm.DiaSemana.Add(DayOfWeek.Tuesday);
            }
            if (model.Qua) {
                vm.DiaSemana.Add(DayOfWeek.Wednesday);
            }
            if (model.Qui) {
                vm.DiaSemana.Add(DayOfWeek.Thursday);
            }
            if (model.Sex) {
                vm.DiaSemana.Add(DayOfWeek.Friday);
            }
            if (model.Sab) {
                vm.DiaSemana.Add(DayOfWeek.Saturday);
            }

            vm.Label = model.Inicio + " - " + model.Fim;

            return vm;
        }

        public static InstituicaoCursoPeriodo ToModel(InstituicaoCursoPeriodoVM vm, bool deep) {
            var model = new InstituicaoCursoPeriodo();
            model.ID = vm.ID;
            model.Inicio = vm.Inicio;
            model.Fim = vm.Fim;
            model.PausaInicio = vm.PausaInicio;
            model.PausaFim = vm.PausaFim;
            model.Quebras = vm.Quebras;

            model.Dom = vm.DiaSemana.Contains(DayOfWeek.Sunday);
            model.Seg = vm.DiaSemana.Contains(DayOfWeek.Monday);
            model.Ter = vm.DiaSemana.Contains(DayOfWeek.Tuesday);
            model.Qua = vm.DiaSemana.Contains(DayOfWeek.Wednesday);
            model.Qui = vm.DiaSemana.Contains(DayOfWeek.Thursday);
            model.Sex = vm.DiaSemana.Contains(DayOfWeek.Friday);
            model.Sab = vm.DiaSemana.Contains(DayOfWeek.Saturday);

            return model;
        }

    }
}