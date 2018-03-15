using System;
using System.Collections.Generic;
using System.Linq;
using Api.AreaInteresseApi;
using Api.CategoriaProfissionalApi;
using Domain.AreaInteresseDomain;
using Domain.UsuarioDomain;

namespace Api.UsuarioApi {

    public class UsuarioAdapter {

        public static UsuarioInfoVM ToViewModel(UsuarioInfo model, List<AreaInteresse> areainteresse, bool deep) {
            var vm = UsuarioAdapter.ToViewModel(model, true);

            if (areainteresse != null) {
                vm.AreaInteresses = areainteresse.Select(x => AreaInteresseAdapter.ToViewModel(x, true)).ToList();
            }

            return vm;
        }

        public static UsuarioInfoVM ToViewModel(UsuarioInfo model, bool deep) {
            var vm = new UsuarioInfoVM();
            vm.ID = model.ID;
            vm.Nome = model.Nome;
            vm.Label = model.Nome;
            vm.CPF = model.CPF;
            vm.RG = model.RG;
            vm.DataNascimento = model.DataNascimento;
            if (model.Roles.Length > 0) {
                vm.Roles = model.Roles.Split(',').ToList();
            } else {
                vm.Roles = new List<string>();
            }
            return vm;
        }

        public static UsuarioInfoVM ToViewModel(UsuarioInfo model, Aluno aluno, Professor professor, bool deep) {
            var vm = UsuarioAdapter.ToViewModel(model, true);
            if (aluno != null) {
                vm.Aluno = AlunoAdapter.ToViewModel(aluno, true);
            }
            if (professor != null) {
                vm.Professor = ProfessorAdapter.ToViewModel(professor, true);
            }
            return vm;
        }

        public static UsuarioInfo ToModel(UsuarioInfoVM vm, bool deep) {
            var model = new UsuarioInfo();
            model.ID = vm.ID;
            model.Nome = vm.Nome;
            model.CPF = vm.CPF;
            model.RG = vm.RG;
            model.DataNascimento = vm.DataNascimento;
            model.Roles = String.Join(",", vm.Roles);
            return model;
        }

        public static UsuarioVM ToViewModel(Usuario model, bool deep) {
            var vm = new UsuarioVM();
            vm.ID = model.ID;
            vm.Username = model.Username;
            vm.Password = model.Password;

            if (model.UsuarioInfo != null && deep) {
                vm.UsuarioInfo = UsuarioAdapter.ToViewModel(model.UsuarioInfo, false);
            }

            return vm;
        }

        public static Usuario ToModel(UsuarioVM vm, bool deep) {
            var model = new Usuario();
            model.ID = vm.ID;
            model.Username = vm.Username;
            model.Password = vm.Password;

            if (vm.UsuarioInfo != null && deep) {
                model.UsuarioInfo = UsuarioAdapter.ToModel(vm.UsuarioInfo, false);
            }

            return model;
        }

    }
}