using System.Collections.Generic;
using System.Linq;
using Api.CategoriaProfissionalApi;
using Domain.AreaInteresseDomain;
using Domain.UsuarioDomain;

namespace Api.UsuarioApi {

    public class AlunoAdapter {

        public static AlunoVM ToViewModel(Aluno model, bool deep) {
            var vm = new AlunoVM();
            vm.ID = model.ID;
            vm.Label = model.UsuarioInfo.Nome;
            return vm;
        }

        public static Aluno ToModel(AlunoVM vm, bool deep) {
            var model = new Aluno();
            model.ID = vm.ID;
            return model;
        }

    }
}