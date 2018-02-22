using System.Collections.Generic;
using System.Linq;
using Api.CategoriaProfissionalApi;
using Domain.AreaInteresseDomain;
using Domain.UsuarioDomain;

namespace Api.UsuarioApi {

    public class ProfessorAdapter {

        public static ProfessorVM ToViewModel(Professor model, List<AreaInteresse> areainteresse, bool deep) {
            var vm = new ProfessorVM();
            vm.ID = model.ID;
            vm.Label = model.UsuarioInfo.Nome;
            vm.UsuarioInfo = UsuarioAdapter.ToViewModel(model.UsuarioInfo, false);
            if (areainteresse != null) {
                vm.CategoriaProfissionais = areainteresse.Select(x => CategoriaProfissionalAdapter.ToViewModel(x.CategoriaProfissional, true)).ToList();
            }

            return vm;
        }

        public static Professor ToModel(ProfessorVM vm, bool deep) {
            var model = new Professor();
            model.ID = vm.ID;
            model.UsuarioInfo = UsuarioAdapter.ToModel(vm.UsuarioInfo, false);

            return model;
        }

    }
}