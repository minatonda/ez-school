using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dto;
using Domain.Models;

namespace Api.Data.ViewModels {
    public class InstituicaoCursoPeriodoAdapter {

        public static InstituicaoCursoPeriodoVM ToViewModel (InstituicaoCursoPeriodoDto model, bool deep) {
            var vm = new InstituicaoCursoPeriodoVM ();

            vm.ID = model.ID.ToString ();

            vm.Label = model.Inicio + " - " + model.Fim;
            vm.Inicio = model.Inicio;
            vm.Fim = model.Fim;
            vm.Seg = model.Seg;
            vm.Ter = model.Ter;
            vm.Qua = model.Qua;
            vm.Qui = model.Qui;
            vm.Sex = model.Sex;
            vm.Sab = model.Sab;
            vm.Dom = model.Dom;

            return vm;
        }

        public static InstituicaoCursoPeriodoDto ToModel (InstituicaoCursoPeriodoVM vm, bool deep) {
            var model = new InstituicaoCursoPeriodoDto ();
            if (vm.ID != null) {
                model.ID = long.Parse (vm.ID);
            }

            model.Inicio = vm.Inicio;
            model.Fim = vm.Fim;
            model.Seg = vm.Seg;
            model.Ter = vm.Ter;
            model.Qua = vm.Qua;
            model.Qui = vm.Qui;
            model.Sex = vm.Sex;
            model.Sab = vm.Sab;
            model.Dom = vm.Dom;

            return model;
        }

    }
}