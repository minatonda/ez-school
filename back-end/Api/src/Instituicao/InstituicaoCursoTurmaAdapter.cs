using Domain.InstituicaoDomain;

namespace Api.InstituicaoApi {
    public class InstituicaoCursoTurmaAdapter {

        public static InstituicaoCursoTurmaVM ToViewModel (InstituicaoCursoTurma model, bool deep) {
            var vm = new InstituicaoCursoTurmaVM ();

            vm.ID = model.ID.ToString ();
            vm.Nome = model.Nome;
            vm.Descricao = model.Descricao;

            vm.Label = vm.Nome;

            return vm;
        }

        public static InstituicaoCursoTurma ToModel (InstituicaoCursoTurmaVM vm, bool deep) {
            var model = new InstituicaoCursoTurma ();
            if (vm.ID != null) {
                model.ID = long.Parse (vm.ID);
            }

            model.Nome = vm.Nome;
            model.Descricao = vm.Descricao;

            return model;
        }

    }
}