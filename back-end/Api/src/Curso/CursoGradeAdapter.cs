using Domain.CursoDomain;

namespace Api.CursoApi {

    public class CursoGradeAdapter {

        public static CursoGradeVM ToViewModel(CursoGrade model, bool deep) {
            var vm = new CursoGradeVM();
            vm.ID = model.ID;

            vm.Label = model.Descricao;
            vm.Descricao = model.Descricao;
            vm.DataCriacao = model.DataCriacao;

            return vm;
        }

        public static CursoGrade ToModel(CursoGradeVM vm, bool deep) {
            var model = new CursoGrade();
            model.ID = vm.ID;
            model.Descricao = vm.Descricao;
            model.DataCriacao = vm.DataCriacao;
            return model;
        }

    }
}