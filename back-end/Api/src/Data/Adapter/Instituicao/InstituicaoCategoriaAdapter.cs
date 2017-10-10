using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Interface;

namespace Api.Data.ViewModels {
    public class InstituicaoCategoriaAdapter {

        public static InstituicaoCategoriaVM ToViewModel (InstituicaoCategoria model, bool deep) {
            var vm = new InstituicaoCategoriaVM ();
            vm.ID = model.ID.ToString();
            vm.Label = model.Nome;

            vm.Nome = model.Nome;
            vm.Descricao = model.Descricao;

            return vm;
        }

        public static SelectVM ToViewModelShort (InstituicaoCategoria model) {
            var vm = new SelectVM ();
            vm.ID = model.ID.ToString();
            vm.Label = model.Nome;
            return vm;
        }

        public static InstituicaoCategoria ToModel (InstituicaoCategoriaVM vm, bool deep) {
            var model = new InstituicaoCategoria ();
                        if (vm.ID != null) {
                model.ID = long.Parse (vm.ID);
            }

            model.Nome = vm.Nome;
            model.Descricao = vm.Descricao;

            return model;
        }

    }
}