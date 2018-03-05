using System;
using System.Collections.Generic;
using System.Linq;
using Api.CategoriaProfissionalApi;
using Api.UsuarioApi;
using Domain.AreaInteresseDomain;

namespace Api.AreaInteresseApi {

    public class AreaInteresseAdapter {

        public static AreaInteresseVM ToViewModel(AreaInteresse model, bool deep) {
            var vm = new AreaInteresseVM();
            vm.ID = model.ID;
            vm.CategoriaProfissional = CategoriaProfissionalAdapter.ToViewModel(model.CategoriaProfissional, true);
            return vm;
        }

        public static AreaInteresse ToModel(AreaInteresseVM vm, bool deep) {
            var model = new AreaInteresse();
            model.ID = vm.ID;
            model.CategoriaProfissional = CategoriaProfissionalAdapter.ToModel(vm.CategoriaProfissional, true);
            return model;
        }

    }
}