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

        public static ProfessorVM ToViewModel(Professor model, List<AreaInteresse> areainteresse, bool deep) {
            var vm = new ProfessorVM ();
            vm.ID = model.ID;
            vm.Label = model.UsuarioInfo.Nome;
            vm.UsuarioInfo = UsuarioAdapter.ToViewModel(model.UsuarioInfo, false);
            vm.CategoriaProfissionais = areainteresse.Select(x => CategoriaProfissionalAdapter.ToViewModel(x.CategoriaProfissional, true)).ToList();

            return vm;
        }

        public static Professor ToModel(ProfessorVM vm, bool deep) {
            var model = new Professor ();
            model.ID = vm.ID;
            model.UsuarioInfo = UsuarioAdapter.ToModel (vm.UsuarioInfo, false);
            
            return model;
        }

    }
}