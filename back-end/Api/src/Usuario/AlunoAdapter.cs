using System.Collections.Generic;
using System.Linq;
using Api.CategoriaProfissionalApi;
using Domain.Models;

namespace Api.UsuarioApi {

    public class AlunoAdapter {

        public static AlunoVM ToViewModel(Aluno model, List<AreaInteresse> areainteresse, bool deep) {
            var vm = new AlunoVM();
            vm.ID = model.ID.ToString();
            vm.Label = model.UsuarioInfo.Nome;
            vm.UsuarioInfo = UsuarioAdapter.ToViewModel(model.UsuarioInfo, false);
            if (areainteresse != null) {
                vm.CategoriaProfissionais = areainteresse.Select(x => CategoriaProfissionalAdapter.ToViewModel(x.CategoriaProfissional, true)).ToList();
            }

            return vm;
        }

        public static Aluno ToModel(AlunoVM vm, bool deep) {
            var model = new Aluno();
            model.ID = vm.ID;
            model.UsuarioInfo = UsuarioAdapter.ToModel(vm.UsuarioInfo, false);
            return model;
        }

    }
}