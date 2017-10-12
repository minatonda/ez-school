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
    public class AlunoAdapter {

        public static AlunoVM ToViewModel (Aluno model, bool deep) {
            var vm = new AlunoVM ();
            vm.ID = model.ID.ToString ();
            vm.Label = model.UsuarioInfo.Nome;
            vm.UsuarioInfo = UsuarioAdapter.ToViewModel (model.UsuarioInfo, false);

            return vm;
        }

        public static SelectVM ToViewModelShort (Aluno model) {
            var vm = new SelectVM ();
            vm.ID = model.ID;
            vm.Label = model.UsuarioInfo.Nome;
            return vm;
        }

        public static Aluno ToModel (AlunoVM vm, bool deep) {
            var model = new Aluno ();
            model.ID = vm.ID;
            model.UsuarioInfo = UsuarioAdapter.ToModel (vm.UsuarioInfo, false);
            return model;
        }

    }
}