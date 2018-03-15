using System;
using System.Collections.Generic;
using System.Linq;
using Api.UsuarioApi;
using Domain.InstituicaoDomain;

namespace Api.InstituicaoApi {
    public class InstituicaoColaboradorPerfilAdapter {

        public static InstituicaoColaboradorPerfilVM ToViewModel(InstituicaoColaboradorPerfil model, bool deep) {
            var vm = new InstituicaoColaboradorPerfilVM();
            vm.ID = model.ID;
            vm.Nome = model.Nome;
            if (model.Roles.Length > 0) {
                vm.Roles = model.Roles.Split(',').ToList();
            } else {
                vm.Roles = new List<string>();
            }

            return vm;
        }

        public static InstituicaoColaboradorPerfil ToModel(InstituicaoColaboradorPerfilVM vm, bool deep) {
            var model = new InstituicaoColaboradorPerfil();
            model.ID = vm.ID;
            model.Nome = vm.Nome;
            model.Roles = String.Join(',', vm.Roles);

            return model;
        }

    }
}