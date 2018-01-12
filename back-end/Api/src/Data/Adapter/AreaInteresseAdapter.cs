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
    public class AreaInteresseAdapter {

        public static AreaInteresseVM ToViewModel (AreaInteresse model, bool deep) {
            var vm = new AreaInteresseVM ();
            vm.ID = model.ID.ToString ();
            if (model.CategoriaProfissional != null) {
                vm.CategoriaProfissional = CategoriaProfissionalAdapter.ToViewModel(model.CategoriaProfissional, true);
            }

            return vm;
        }

        public static AreaInteresse ToModel (AreaInteresseVM vm, bool deep) {
            var model = new AreaInteresse ();
            if (vm.ID != null) {
                model.ID = long.Parse (vm.ID);
            }
            if (vm.CategoriaProfissional != null) {
                model.CategoriaProfissional = CategoriaProfissionalAdapter.ToModel(vm.CategoriaProfissional, true);
            }

            return model;
        }

    }
}