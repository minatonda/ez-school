using System;
using System.Collections.Generic;
using System.Linq;
using Api.UsuarioApi;
using Domain.InstituicaoDomain;

namespace Api.InstituicaoApi {
    public class InstituicaoColaboradorAdapter {

        public static InstituicaoColaboradorVM ToViewModel(InstituicaoColaborador model, bool deep) {
            var vm = new InstituicaoColaboradorVM();
            vm.ID = model.ID;
            vm.Usuario = UsuarioAdapter.ToViewModel(model.Usuario, true);
            if (model.Perfis.Length > 0) {
                vm.Perfis = model.Perfis.Split(',').ToList();
            } else {
                vm.Perfis = new List<string>();
            }

            return vm;
        }

        public static InstituicaoColaborador ToModel(InstituicaoColaboradorVM vm, bool deep) {
            var model = new InstituicaoColaborador();
            model.ID = vm.ID;
            model.Usuario = UsuarioAdapter.ToModel(vm.Usuario, true);
            model.Perfis = String.Join(',', vm.Perfis);

            return model;
        }

    }
}