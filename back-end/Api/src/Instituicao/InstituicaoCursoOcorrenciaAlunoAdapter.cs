using Api.UsuarioApi;
using Domain.InstituicaoDomain;

namespace Api.InstituicaoApi {
    public class InstituicaoCursoOcorrenciaAlunoAdapter {

        public static InstituicaoCursoOcorrenciaAlunoVM ToViewModel(InstituicaoCursoOcorrenciaAluno model, bool deep) {
            var vm = new InstituicaoCursoOcorrenciaAlunoVM();

            vm.ID = model.ID.ToString();

            if (model.Aluno != null) {
                vm.Aluno = AlunoAdapter.ToViewModel(model.Aluno, null, true);
            }

            vm.Label = vm.Aluno.UsuarioInfo.Nome;

            return vm;
        }

        public static InstituicaoCursoOcorrenciaAluno ToModel(InstituicaoCursoOcorrenciaAlunoVM vm, bool deep) {
            var model = new InstituicaoCursoOcorrenciaAluno();
            if (vm.ID != null) {
                model.ID = long.Parse(vm.ID);
            }

            if (vm.Aluno != null) {
                model.Aluno = AlunoAdapter.ToModel(vm.Aluno, true);
            }

            return model;
        }

        public static InstituicaoCursoOcorrenciaAluno ToModel(InstituicaoCursoOcorrenciaVM instituicaoCursoOcorrencia, AlunoVM aluno) {
            var model = new InstituicaoCursoOcorrenciaAluno();
            model.InstituicaoCursoOcorrencia = InstituicaoCursoOcorrenciaAdapter.ToModel(instituicaoCursoOcorrencia, true);
            model.Aluno = AlunoAdapter.ToModel(aluno, true);
            return model;
        }

    }
}