using Domain.CursoDomain;

namespace Api.CursoApi {

    public class CursoAdapter {

        public static CursoVM ToViewModel(Curso model, bool deep) {
            var vm = new CursoVM();
            vm.ID = model.ID;
            vm.Nome = model.Nome;
            vm.Descricao = model.Descricao;

            vm.Label = model.Nome;

            return vm;
        }

        public static Curso ToModel(CursoVM vm, bool deep) {
            var model = new Curso();
            model.ID = vm.ID;
            model.Nome = vm.Nome;
            model.Descricao = vm.Descricao;

            return model;
        }

    }
}