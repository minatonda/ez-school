using System.Collections.Generic;
using System.Linq;
using Api.CategoriaProfissionalApi;
using Domain.AreaInteresseDomain;
using Domain.UsuarioDomain;

namespace Api.UsuarioApi {

    public class ProfessorAdapter {

        public static ProfessorVM ToViewModel(Professor model, bool deep) {
            var vm = new ProfessorVM();
            vm.ID = model.ID;
            vm.Label = model.UsuarioInfo.Nome;

            return vm;
        }

        public static Professor ToModel(ProfessorVM vm, bool deep) {
            var model = new Professor();
            model.ID = vm.ID;

            return model;
        }

    }
}