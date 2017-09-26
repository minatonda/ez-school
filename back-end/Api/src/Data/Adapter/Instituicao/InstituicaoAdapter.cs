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
    public class InstituicaoAdapter {

        public static InstituicaoVM ToViewModel (Instituicao model, bool deep) {
            var vm = new InstituicaoVM ();
            vm.ID = model.ID;
            vm.Label = model.Nome;

            vm.Nome = model.Nome;
            vm.CNPJ = model.CNPJ;

            return vm;
        }

        public static ShortVM ToViewModelShort (Instituicao model) {
            var vm = new ShortVM ();
            vm.ID = model.ID;
            vm.Label = model.Nome;
            return vm;
        }

        public static Instituicao ToModel (InstituicaoVM vm, bool deep) {
            var model = new Instituicao ();
            model.ID = vm.ID;

            model.Nome = vm.Nome;
            model.CNPJ = vm.CNPJ;

            return model;
        }

    }
}