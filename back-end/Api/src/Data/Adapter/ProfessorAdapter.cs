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
    public class ProfessorAdapter {

        public static ProfessorVM ToViewModel (Professor model, bool deep) {
            var vm = new ProfessorVM ();
            vm.ID = model.ID;
            vm.Label = model.UsuarioInfo.Nome;

            return vm;
        }

        public static SelectVM ToViewModelShort (Professor model) {
            var vm = new SelectVM ();
            vm.ID = model.ID;
            vm.Label = model.UsuarioInfo.Nome;
            return vm;
        }

        public static Professor ToModel (ProfessorVM vm, bool deep) {
            var model = new Professor (UsuarioAdapter.ToModel(vm.UsuarioInfo,false));
            model.ID = vm.ID;
            return model;
        }

    }
}