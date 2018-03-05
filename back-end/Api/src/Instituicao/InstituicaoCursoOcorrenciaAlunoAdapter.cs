using System.Collections.Generic;
using Api.UsuarioApi;
using Domain.AreaInteresseDomain;
using Domain.InstituicaoDomain;

namespace Api.InstituicaoApi {
    public class InstituicaoCursoOcorrenciaAlunoAdapter {

        public static InstituicaoCursoOcorrenciaAlunoVM ToViewModel(InstituicaoCursoOcorrenciaAluno model, bool deep) {
            var vm = new InstituicaoCursoOcorrenciaAlunoVM();
            vm.ID = model.ID;

            if (model.Aluno != null) {
                vm.Aluno = UsuarioAdapter.ToViewModel(model.Aluno, new List<AreaInteresse>(), true);
            }

            vm.Label = vm.Aluno.Nome;

            return vm;
        }

        public static InstituicaoCursoOcorrenciaAluno ToModel(InstituicaoCursoOcorrenciaAlunoVM vm, bool deep) {
            var model = new InstituicaoCursoOcorrenciaAluno();
            model.ID = vm.ID;

            if (vm.Aluno != null) {
                model.Aluno = UsuarioAdapter.ToModel(vm.Aluno, true);
            }

            return model;
        }

        public static InstituicaoCursoOcorrenciaAluno ToModel(InstituicaoCursoOcorrenciaVM instituicaoCursoOcorrencia, UsuarioInfoVM aluno) {
            var model = new InstituicaoCursoOcorrenciaAluno();
            model.InstituicaoCursoOcorrencia = InstituicaoCursoOcorrenciaAdapter.ToModel(instituicaoCursoOcorrencia, true);
            model.Aluno = UsuarioAdapter.ToModel(aluno, true);
            return model;
        }

    }
}