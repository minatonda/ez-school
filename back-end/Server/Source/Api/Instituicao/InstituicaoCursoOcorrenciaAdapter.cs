using System.Collections.Generic;
using System.Linq;
using Api.UsuarioApi;
using Domain.InstituicaoDomain;

namespace Api.InstituicaoApi {

    public class InstituicaoCursoOcorrenciaAdapter {

        public static InstituicaoCursoOcorrenciaVM ToViewModel(InstituicaoCursoOcorrencia model, bool deep) {
            var vm = new InstituicaoCursoOcorrenciaVM();
            vm.ID = model.ID;
            vm.DataInicio = model.DataInicio;
            vm.DataExpiracao = model.DataExpiracao;

            if (model.Coordenador != null) {
                vm.Coordenador = UsuarioAdapter.ToViewModel(model.Coordenador, null, false);
            }

            return vm;
        }

        public static InstituicaoCursoOcorrencia ToModel(InstituicaoCursoOcorrenciaVM vm, bool deep) {
            var model = new InstituicaoCursoOcorrencia();
            model.ID = vm.ID;
            model.DataInicio = vm.DataInicio;
            model.DataExpiracao = vm.DataExpiracao;

            if (vm.Coordenador != null) {
                model.Coordenador = UsuarioAdapter.ToModel(vm.Coordenador, false);
            }

            return model;
        }

        public static List<InstituicaoCursoOcorrenciaPeriodo> InstituicaoCursoOcorrenciaPeriodosFrom(InstituicaoCursoOcorrenciaVM vm) {
            return vm.InstituicaoCursoOcorrenciaPeriodos.Select(x => InstituicaoCursoOcorrenciaPeriodoAdapter.ToModel(x, true)).ToList();
        }

    }
}