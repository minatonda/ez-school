using System.Collections.Generic;
using System.Linq;
using Api.CursoApi;
using Domain.CursoDomain;
using Domain.InstituicaoDomain;

namespace Api.InstituicaoApi {

    public class InstituicaoCursoOcorrenciaAusenciaAdapter {

        public static InstituicaoCursoOcorrenciaAusenciaVM ToViewModel(InstituicaoCursoOcorrenciaAusencia model, bool deep) {
            var vm = new InstituicaoCursoOcorrenciaAusenciaVM();
            vm.ID = model.ID;
            vm.DataAusencia = model.DataAusencia;
            vm.InstituicaoCursoOcorrenciaPeriodoAluno = InstituicaoCursoOcorrenciaPeriodoAlunoAdapter.ToViewModel(model.InstituicaoCursoOcorrenciaPeriodoAluno, false);

            return vm;
        }

        public static InstituicaoCursoOcorrenciaAusencia ToModel(InstituicaoCursoOcorrenciaAusenciaVM vm, bool deep) {
            var model = new InstituicaoCursoOcorrenciaAusencia();
            model.ID = vm.ID;
            model.DataAusencia = vm.DataAusencia;
            model.InstituicaoCursoOcorrenciaPeriodoAluno = InstituicaoCursoOcorrenciaPeriodoAlunoAdapter.ToModel(vm.InstituicaoCursoOcorrenciaPeriodoAluno, false);

            return model;
        }

    }
}