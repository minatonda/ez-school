using System.Collections.Generic;
using System.Linq;
using Api.CursoApi;
using Domain.CursoDomain;
using Domain.InstituicaoDomain;

namespace Api.InstituicaoApi {

    public class InstituicaoCursoOcorrenciaNotaAdapter {

        public static InstituicaoCursoOcorrenciaNotaVM ToViewModel(InstituicaoCursoOcorrenciaNota model, bool deep) {
            var vm = new InstituicaoCursoOcorrenciaNotaVM();
            vm.ID = model.ID;
            vm.Valor = model.Valor;
            vm.IDTag = model.IDTag;
            vm.DataLancamento = model.DataLancamento;
            vm.InstituicaoCursoOcorrenciaPeriodoAluno = InstituicaoCursoOcorrenciaPeriodoAlunoAdapter.ToViewModel(model.InstituicaoCursoOcorrenciaPeriodoAluno, false);
            
            return vm;
        }

        public static InstituicaoCursoOcorrenciaNota ToModel(InstituicaoCursoOcorrenciaNotaVM vm, bool deep) {
            var model = new InstituicaoCursoOcorrenciaNota();
            model.ID = vm.ID;
            model.Valor = vm.Valor;
            model.IDTag = vm.IDTag;
            model.DataLancamento = vm.DataLancamento;
            model.InstituicaoCursoOcorrenciaPeriodoAluno = InstituicaoCursoOcorrenciaPeriodoAlunoAdapter.ToModel(vm.InstituicaoCursoOcorrenciaPeriodoAluno, false);

            return model;
        }

    }
}