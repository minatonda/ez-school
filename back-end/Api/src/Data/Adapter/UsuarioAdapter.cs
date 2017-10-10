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
    public class UsuarioAdapter {

        public static UsuarioInfoVM ToViewModel (UsuarioInfo model, bool deep) {
            var vm = new UsuarioInfoVM ();
            vm.ID = model.ID;
            vm.Nome = model.Nome;
            vm.CPF = model.CPF;
            vm.RG = model.RG;
            vm.DataNascimento = model.DataNascimento;
            return vm;
        }

        public static UsuarioInfo ToModel (UsuarioInfoVM vm, bool deep) {
            var model = new UsuarioInfo ();
            model.ID = vm.ID;
            model.Nome = vm.Nome;
            model.CPF = vm.CPF;
            model.RG = vm.RG;
            model.DataNascimento = vm.DataNascimento;
            return model;
        }

        public static UsuarioVM ToViewModel (Usuario model, bool deep) {
            var vm = new UsuarioVM ();
            vm.ID = model.ID;
            vm.Username = model.Username;
            vm.Password = model.Password;
            if (model.UsuarioInfo != null && deep) {
                vm.UsuarioInfo = UsuarioAdapter.ToViewModel (model.UsuarioInfo, false);
            }
            return vm;
        }

        public static Usuario ToModel (UsuarioVM vm, bool deep) {
            var model = new Usuario ();
            model.ID = vm.ID;
            model.Username = vm.Username;
            model.Password = vm.Password;
            if (vm.UsuarioInfo != null && deep) {
                model.UsuarioInfo = UsuarioAdapter.ToModel (vm.UsuarioInfo, false);
            }
            return model;
        }
        
        public static SelectVM ToViewModelShort (Usuario model) {
            var vm = new SelectVM ();
            vm.Label = model.Username;
            return vm;
        }

    }
}