using System.Collections.Generic;
using System.Linq;
using Api.CursoApi;
using Domain.CursoDomain;
using Domain.InstituicaoDomain;

namespace Api.InstituicaoApi {

    public class InstituicaoCursoOcorrenciaPeriodoAdapter {

        public static InstituicaoCursoOcorrenciaPeriodoVM ToViewModel(InstituicaoCursoOcorrenciaPeriodo model, bool deep) {
            var vm = new InstituicaoCursoOcorrenciaPeriodoVM();
            vm.ID = model.ID;
            vm.DataInicio = model.DataInicio;
            vm.DataExpiracao = model.DataExpiracao;

            return vm;
        }

        public static InstituicaoCursoOcorrenciaPeriodo ToModel(InstituicaoCursoOcorrenciaPeriodoVM vm, bool deep) {
            var model = new InstituicaoCursoOcorrenciaPeriodo();
            model.ID = vm.ID;
            model.DataInicio = vm.DataInicio;
            model.DataExpiracao = vm.DataExpiracao;

            return model;
        }

    }
}