using Domain.CursoDomain;

namespace Api.CursoApi {

    public class CursoAdapter {

        public static CursoVM ToViewModel(Curso model, bool deep) {
            var vm = new CursoVM();
            vm.ID = model.ID.ToString();
            vm.Label = model.Nome;

            vm.Nome = model.Nome;
            vm.Descricao = model.Descricao;

            return vm;
        }

        public static Curso ToModel(CursoVM vm, bool deep) {
            var model = new Curso();
            if (vm.ID != null) {
                model.ID = long.Parse(vm.ID);
            }

            model.Nome = vm.Nome;
            model.Descricao = vm.Descricao;

            return model;
        }

    }
}